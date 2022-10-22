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
    /// Mappings for the Program entity.
    /// </summary>
    public class ProgramMap : BaseEntityTypeConfiguration<Program>
    {
        /// <summary>
        /// configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Program> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.HasOne(mapping => mapping.University)
                .WithMany()
                .HasForeignKey(mapping => mapping.UniversityId);
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
        protected override void PostConfigure(EntityTypeBuilder<Program> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
