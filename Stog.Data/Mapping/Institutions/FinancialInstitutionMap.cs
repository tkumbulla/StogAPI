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
    /// Mappings for the FinancialInstitution entity.
    /// </summary>
    public class FinancialInstitutionMap : BaseEntityTypeConfiguration<FinancialInstitution>
    {
        public override int Order => 1;
        /// <summary>
        /// Configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<FinancialInstitution> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.Property(mapping => mapping.Name).IsRequired();
            builder.Property(mapping => mapping.Nuis).IsRequired();
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
        /// Post configure
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<FinancialInstitution> builder)
        {
            base.PostConfigure(builder);    
        }
    }
}
