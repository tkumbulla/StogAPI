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
    /// Mappings for StudiesApplication entity.
    /// </summary>
    public class StudiesApplicationMap : BaseEntityTypeConfiguration<StudiesApplication>
    {
        public override int Order => 1;
        /// <summary>
        /// Configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<StudiesApplication> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.Property(mapping => mapping.Status).IsRequired();
            builder.HasOne(mapping => mapping.Program)
                .WithMany()
                .HasForeignKey(mapping => mapping.ProgramId);
            builder.HasOne(mapping => mapping.Student)
                .WithMany()
                .HasForeignKey(mapping => mapping.StudentId);
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
        protected override void PostConfigure(EntityTypeBuilder<StudiesApplication> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
