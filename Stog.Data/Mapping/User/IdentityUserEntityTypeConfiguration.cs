﻿using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Data.Mapping.User
{
    /// <summary>
    /// Configuration for the identity user entity
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public abstract class IdentityUserEntityTypeConfiguration<TEntity> : IMappingConfiguration,
        IEntityTypeConfiguration<TEntity>
        where TEntity : IdentityUser
    {
        public virtual int Order { get; set; } = 1;

        #region Utilities

        /// <summary>
        /// Developers can override this method in custom partial classes in order to add some custom configuration code.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        protected virtual void PostConfigure(EntityTypeBuilder<TEntity> builder)
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Configures the entity.
        /// </summary>
        /// <param name="builder">The builder to be used to configure the entity.</param>
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            //add custom configuration
            PostConfigure(builder);
        }

        /// <summary>
        /// Apply this mapping configuration.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for the database context.</param>
        public virtual void ApplyConfiguration(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(this);
        }

        #endregion
    }
}
