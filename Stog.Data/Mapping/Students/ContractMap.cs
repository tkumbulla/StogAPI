using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stog.Data.Mapping.Generics;
using Stog.Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Data.Mapping.Students
{
    /// <summary>
    /// Mappings for the Contract entity
    /// </summary>
    public class ContractMap : BaseEntityTypeConfiguration<Contract>
    {
        public override int Order => 1;
        /// <summary>
        /// Configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Contract> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.HasOne(mapping => mapping.Student)
                .WithMany()
                .HasForeignKey(mapping => mapping.StudentId);
            builder.HasOne(mapping => mapping.FinancialInstitution)
                .WithMany()
                .HasForeignKey(mapping => mapping.FinancialInstitutionId);
            builder.HasOne(mapping => mapping.File)
                .WithMany()
                .HasForeignKey(mapping => mapping.FileId);
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
        protected override void PostConfigure(EntityTypeBuilder<Contract> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
