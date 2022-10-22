using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.Extensions.Configuration;
using Stog.Data.Mapping.Generics;
using Stog.Domain.Models.Generics;
using Stog.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace Stog.Data.Context
{
    /// <summary>
    /// Defines the structure of the DBContext class. All configurations for database context go here.
    /// </summary>
    public class DBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, IdentityUserClaim<Guid>,
    UserRole, IdentityUserLogin<Guid>,
    IdentityRoleClaim<Guid>, IdentityUserToken<Guid>>, IDbContext
    {
        private readonly IAuthenticationService _authenticationService;
        #region Ctor
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        /// <param name="authenticationService"></param>
        public DBContext(Microsoft.EntityFrameworkCore.DbContextOptions<DBContext> options, IAuthenticationService authenticationService)
        : base(options)
        {
            _authenticationService = authenticationService;
        }

        #endregion

        #region SaveChanges Overrides

        /// <inheritdoc cref="IdentityDbContext"/>
        public override int SaveChanges()
        {
            UpdateAuditAndDeleteStatus();
            return base.SaveChanges();
        }

        /// <inheritdoc cref="IdentityDbContext"/>
        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateAuditAndDeleteStatus();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        /// <inheritdoc cref="IdentityDbContext"/>
        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditAndDeleteStatus();
            return await base.SaveChangesAsync(cancellationToken);
        }

        /// <inheritdoc cref="IdentityDbContext"/>
        public override async Task<int> SaveChangesAsync(bool acceptAllChangesOnSuccess, CancellationToken cancellationToken = new CancellationToken())
        {
            UpdateAuditAndDeleteStatus();
            return await base.SaveChangesAsync(acceptAllChangesOnSuccess, cancellationToken);
        }

        /// <summary>
        /// Updates audit data, and prevents the user from hard deleting rows from the database.
        /// </summary>
        private void UpdateAuditAndDeleteStatus()
        {
            //   var authenticatedUser = await _authenticationService.GetAuthenticatedUserOrGuestAsync();

            // loop through the changes
            foreach (var entry in ChangeTracker.Entries())
            {
                // loop through deletable records
                // note: we put this before auditable records because we change the 'state' to modified
                if (entry.Entity is ISoftDeletable deletable)
                {
                    // prevent deletion
                    if (entry.State == EntityState.Deleted)
                    {
                        entry.State = EntityState.Modified;
                        deletable.IsDeleted = true;
                    }
                }

                // pattern match auditable records
                if (entry.Entity is IAuditable auditable)
                {
                    // if added, set the create date, and skip the UpdatedOn field
                    if (entry.State == EntityState.Added)
                    {
                        auditable.CreatedOnUtc = DateTime.UtcNow;
                        //auditable.CreatedById = new Guid("17c20463-72eb-401a-8505-d1e33cd8af47");
                    }
                    // if modified, set the date
                    else
                    {
                        Entry(auditable).Property(p => p.CreatedById).IsModified = false;
                        Entry(auditable).Property(p => p.CreatedOnUtc).IsModified = false;
                        //auditable.UpdatedOnUtc = DateTime.UtcNow;
                        //auditable.UpdatedById = new Guid("17c20463-72eb-401a-8505-d1e33cd8af47");
                    }
                }
            }
        }

        #endregion

        #region Utilities

        /// <inheritdoc cref="DbContext"/>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //dynamically load all entity and query type configurations
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var typeConfigurations = assemblies.SelectMany(a => a.GetTypes()).Where(type =>
                (type.BaseType?.IsGenericType ?? false)
                && type.BaseType.GetGenericTypeDefinition() == typeof(BaseEntityTypeConfiguration<>));

            var mapList = new List<IMappingConfiguration?>();
            foreach (var typeConfiguration in typeConfigurations)
            {
                var configuration = Activator.CreateInstance(typeConfiguration) as IMappingConfiguration;
                mapList.Add(configuration);
            }
            foreach (var map in mapList.OrderBy(l => l?.Order))
            {
                map?.ApplyConfiguration(modelBuilder);
            }

            // make all decimals with a set precision
            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(decimal)
                            || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18, 2)");
            }

            // make all enums strings
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType.BaseType == typeof(Enum))
                    {
                        var type = typeof(EnumToStringConverter<>).MakeGenericType(property.ClrType);
                        var converter = Activator.CreateInstance(type, new ConverterMappingHints()) as ValueConverter;

                        property.SetValueConverter(converter);
                    }
                }
            }

            // make all ISoftDeletables hidden from query results
            //foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            //{
            //    modelBuilder.Entity(entityType.ClrType, entityBuilder =>
            //    {
            //        //Global Filters
            //        var lambdaExp = ApplyEntityFilterTo(entityType.ClrType);
            //        if (lambdaExp != null)
            //            entityBuilder.HasQueryFilter(lambdaExp);
            //    });
            //}

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<UserRole>(userRole =>
            {
                userRole.HasKey(ur => new { ur.UserId, ur.RoleId });

                userRole.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);

                userRole.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired()
        .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<ApplicationUser>(applicationUser =>
            {
                applicationUser.HasKey(au => au.Id);
                applicationUser.HasOne(au => au.CreatedBy)
                .WithOne()
                .HasForeignKey<ApplicationUser>(au => au.CreatedById)
        .OnDelete(DeleteBehavior.Cascade);

                applicationUser.HasOne(au => au.UpdatedBy)
               .WithOne()
               .HasForeignKey<ApplicationUser>(au => au.UpdatedById)
        .OnDelete(DeleteBehavior.Restrict);
            });
        }
        protected virtual LambdaExpression? ApplyEntityFilterTo(System.Type entityClrType)
        {
            if (typeof(ISoftDeletable).IsAssignableFrom(entityClrType))
            {
                var parameter = Expression.Parameter(entityClrType, "entity");
                var member = Expression.Property(parameter, nameof(ISoftDeletable.IsDeleted));
                var body = Expression.Equal(member, Expression.Constant(false));
                return Expression.Lambda(body, parameter);
            }

            return null;
        }

        #endregion


        #region Methods

        /// <summary>
        /// Creates a DbSet that can be used to query and save instances of entity.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <returns>A set for the given entity type.</returns>
        public new virtual DbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        /// <summary>
        /// Detach an entity from the context.
        /// </summary>
        /// <typeparam name="TEntity">Entity type.</typeparam>
        /// <param name="entity">Entity.</param>
        public virtual void Detach<TEntity>(TEntity entity) where TEntity : BaseEntity
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            var entityEntry = Entry(entity);
            if (entityEntry == null)
                return;

            //set the entity is not being tracked by the context
            entityEntry.State = EntityState.Detached;
        }

        #endregion

    }
}
