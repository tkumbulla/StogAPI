using Stog.Domain.Models.Generics;
using Stog.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Common
{
    public class File : AuditableEntity
    {
        public File()
        {
            Name = string.Empty;
            Description = string.Empty;
            ContentType = string.Empty;
            PhysicalPath = string.Empty;
            VirtualPath = string.Empty;
        }

        /// <summary>
        /// Gets or sets the name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the virtual path
        /// </summary>
        public string VirtualPath { get; set; }

        /// <summary>
        /// Gets or sets the content type
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Gets or sets the physical path
        /// </summary>
        public string PhysicalPath { get; set; }
    }
}
