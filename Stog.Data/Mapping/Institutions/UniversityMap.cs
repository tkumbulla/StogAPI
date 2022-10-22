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
    /// Mappings for the University entity.
    /// </summary>
    public class UniversityMap : BaseEntityTypeConfiguration<University>
    {
        /// <summary>
        /// configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<University> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.HasOne(mapping => mapping.FinancialInstitution)
                .WithMany()
                .HasForeignKey(mapping => mapping.FinancialInstitutionId);
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
        protected override void PostConfigure(EntityTypeBuilder<University> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
