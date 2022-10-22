using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Stog.Data.Mapping.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Data.Mapping.Common
{
    /// <summary>
    /// Mappings for File entity
    /// </summary>
    public class FileMap : BaseEntityTypeConfiguration<Stog.Domain.Models.Common.File>
    {
        public override int Order => 1;
        /// <summary>
        /// Configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<Stog.Domain.Models.Common.File> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.Property(mapping => mapping.Name).IsRequired();
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
        protected override void PostConfigure(EntityTypeBuilder<Stog.Domain.Models.Common.File> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
