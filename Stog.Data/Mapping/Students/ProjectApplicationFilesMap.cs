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
    /// Mappings for the ProjectApplicationFiles entity
    /// </summary>
    public class ProjectApplicationFilesMap : BaseEntityTypeConfiguration<ProjectApplicationFiles>
    {
        public override int Order => 1;
        /// <summary>
        /// Configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ProjectApplicationFiles> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.HasOne(mapping => mapping.ProjectApplication)
                .WithMany()
                .HasForeignKey(mapping => mapping.ProjectApplicationId);
            builder.HasOne(mapping => mapping.File)
                .WithMany()
                .HasForeignKey(mapping => mapping.FileId);
            base.Configure(builder);
        }
        /// <summary>
        /// Post configure
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<ProjectApplicationFiles> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
