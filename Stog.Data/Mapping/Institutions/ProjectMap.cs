using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stog.Data.Mapping.Generics;
using Stog.Domain.Models.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Data.Mapping.Institutions
{
    /// <summary>
    /// Mappings for Project entity
    /// </summary>
    public class ProjectMap : BaseEntityTypeConfiguration<Project>
    {
        /// <summary>
        /// configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.Property(mapping => mapping.Title).IsRequired();
            builder.HasOne(mapping => mapping.FinancialInstitution)
                .WithMany(mapping => mapping.Projects)
                .HasForeignKey(mapping => mapping.FinancialInstitutionId)
                .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict)
                .IsRequired();
            builder.HasOne(mapping => mapping.RequirementFile)
                .WithMany()
                .HasForeignKey(mapping => mapping.RequirementFileId)
                 .OnDelete(Microsoft.EntityFrameworkCore.DeleteBehavior.Restrict)
                .IsRequired();
            #region Auditable-Mappings

            builder.HasOne(mapping => mapping.CreatedBy)
                .WithMany()
                .HasForeignKey(mapping => mapping.CreatedById);

            builder.HasOne(mapping => mapping.UpdatedBy)
                .WithMany()
                .HasForeignKey(mapping => mapping.UpdatedById)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion
            base.Configure(builder);
        }
        /// <summary>
        /// post configuring mappings
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<Project> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
