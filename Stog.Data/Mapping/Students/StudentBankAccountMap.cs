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
    /// Mappings for StudentBankAccount entity.
    /// </summary>
    public class StudentBankAccountMap : BaseEntityTypeConfiguration<StudentBankAccount>
    {
        public override int Order => 1;
        /// <summary>
        /// Configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<StudentBankAccount> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.Property(mapping => mapping.IBAN).IsRequired();
            builder.HasOne(mapping => mapping.FinancialInstitution)
                .WithMany()
                .HasForeignKey(mapping => mapping.FinanacialInstitutionId).IsRequired();
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
        protected override void PostConfigure(EntityTypeBuilder<StudentBankAccount> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
