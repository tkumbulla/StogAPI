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
    /// Mappings for the ProjectFields entity.
    /// </summary>
    public class ProjectFieldsMap : BaseEntityTypeConfiguration<ProjectFields>
    {
        /// <summary>
        /// configure mappings
        /// </summary>
        /// <param name="builder"></param>
        public override void Configure(EntityTypeBuilder<ProjectFields> builder)
        {
            builder.HasKey(mapping => mapping.Id);
            builder.HasOne(mapping => mapping.Field)
                .WithMany()
                .HasForeignKey(mapping => mapping.FieldId);
            builder.HasOne(mapping => mapping.Project)
                .WithMany()
                .HasForeignKey(mapping => mapping.ProjectId);
            base.Configure(builder);
        }
        /// <summary>
        /// post configuring mappings
        /// </summary>
        /// <param name="builder"></param>
        protected override void PostConfigure(EntityTypeBuilder<ProjectFields> builder)
        {
            base.PostConfigure(builder);
        }
    }
}
