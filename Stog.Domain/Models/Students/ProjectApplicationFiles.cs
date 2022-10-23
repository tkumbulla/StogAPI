using Stog.Domain.Models.Generics;
using Stog.Domain.Models.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Students
{
    public class ProjectApplicationFiles : BaseEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ProjectApplicationFiles()
        {
            //ProjectApplication = new ProjectApplication();
            //File = new Common.File();
        }
        /// <summary>
        /// Identifier of the Project Application 
        /// </summary>
        public Guid ProjectApplicationId { get; set; }
        /// <summary>
        /// Object of the Project Application
        /// </summary>
        public virtual ProjectApplication ProjectApplication { get; set; }
        /// <summary>
        /// Identifier of the File
        /// </summary>
        public Guid FileId { get; set; }
        /// <summary>
        /// Object of the File
        /// </summary>s
        public Common.File File { get; set; }
    }
}
