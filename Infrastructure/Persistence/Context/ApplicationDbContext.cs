using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using BeerService.Application.Common.Dependencies.Services;
using BeerService.Domain.Common;
using BeerService.Infrastructure.Identity.Model;
using System.Transactions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BeerService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Reflection;

namespace BeerService.Infrastructure.Persistence.Context;

// In this particular architecture, AppDbContext only contains
// translation logic required for proper loading and saving of
// domain entities. Query-specific customizations and utility
// logic has been relegated higher, to repositories.

/// <summary>
/// DB context implementation for Entity Framework.
/// </summary>
public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    // add-migration migrationName -OutputDir "Persistence/Context/Migrations"

    public DbSet<Beer> Beers => Set<Beer>();
    public DbSet<BeerRating> BeerRatings => Set<BeerRating>();
    public DbSet<BeerType> BeerTypes => Set<BeerType>();

    private readonly ICurrentUserService _currentUser;
    private readonly IDateTime _dateTime;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ICurrentUserService currentUser, IDateTime dateTime) : base(options)
    {
        _currentUser = currentUser;
        _dateTime = dateTime;

        // The default cascade delete, combined with the default Immediate cascade timing setting,
        // can cause problems if we intend to manually revert the state of EntityEntries.
        // This is because deletion state change cascades to navigation properties, even owned types,
        // and reverting the state change on the root entity doesn't revert the cascaded changes.
        // This is particularly problematic for owned types, where it seems EF incorrectly
        // does a cascade and write nulls to the DB, even when the owned type is non-nullable,
        // causing errors when saving entities.
        // Here I effectively disabled the cascade for then-reverted (soft-deleted) entities.
        // But in a production system the cascade behavior has to be properly designed.
        ChangeTracker.CascadeDeleteTiming = CascadeTiming.OnSaveChanges;
    }

    // Added for LinqPad.
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        _currentUser = null!;
        _dateTime = null!;
    }

    public override int SaveChanges()
        => SaveChanges(acceptAllChangesOnSuccess: true);

    public override int SaveChanges(bool acceptAllChangesOnSuccess)
    {
        ApplyMyEntityOverrides();
        return base.SaveChanges(acceptAllChangesOnSuccess);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        => SaveChangesAsync(acceptAllChangesOnSuccess: true, cancellationToken);

    public override Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = default)
    {
        ApplyMyEntityOverrides();
        return base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        ConfigureSoftDeleteFilter(builder);
        base.OnModelCreating(builder);
    }

    /// <summary>
    /// Set global filter on all soft-deletable entities to exclude the ones which are 'deleted'.
    /// </summary>
    private static void ConfigureSoftDeleteFilter(ModelBuilder builder)
    {
        foreach (var softDeletableTypeBuilder in builder.Model.GetEntityTypes()
            .Where(x => typeof(ISoftDeletable).IsAssignableFrom(x.ClrType)))
        {
            var parameter = Expression.Parameter(softDeletableTypeBuilder.ClrType, "p");

            softDeletableTypeBuilder.SetQueryFilter(
                Expression.Lambda(
                    Expression.Equal(
                        Expression.Property(parameter, nameof(ISoftDeletable.DeletedAt)),
                        Expression.Constant(null)),
                    parameter)
            );
        }
    }

    /// <summary>
    /// Automatically stores metadata when entities are added, modified, or deleted.
    /// </summary>
    private void ApplyMyEntityOverrides()
    {
        foreach (var entry in ChangeTracker.Entries<IAudited>())
        {
            switch (entry.State)
            {
                case EntityState.Added:
                    entry.Property(nameof(IAudited.CreatedBy)).CurrentValue = _currentUser.UserId;
                    entry.Property(nameof(IAudited.CreatedAt)).CurrentValue = _dateTime.Now;
                    break;
                case EntityState.Modified:
                    entry.Property(nameof(IAudited.LastModifiedBy)).CurrentValue = _currentUser.UserId;
                    entry.Property(nameof(IAudited.LastModifiedAt)).CurrentValue = _dateTime.Now;
                    break;
            }
        }

        foreach (var entry in ChangeTracker.Entries<ISoftDeletable>())
        {
            switch (entry.State)
            {
                case EntityState.Deleted:
                    entry.State = EntityState.Unchanged; // Override removal. Better than Modified, because that flags ALL properties for update.
                    entry.Property(nameof(ISoftDeletable.DeletedBy)).CurrentValue = _currentUser.UserId;
                    entry.Property(nameof(ISoftDeletable.DeletedAt)).CurrentValue = _dateTime.Now;
                    break;
            }
        }
    }
}
