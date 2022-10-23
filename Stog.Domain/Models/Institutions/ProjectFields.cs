using Stog.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Institutions
{
    public class ProjectFields : BaseEntity
    {
        ///// <summary>
        ///// Ctor
        ///// </summary>
        //public ProjectFields()
        //{
        //    Project = new Project();
        //    Field = new Field();
        //}
        /// <summary>
        /// Identifier of the Project 
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// Object of the Project
        /// </summary>
        public virtual Project Project { get; set; }   
        /// <summary>
        /// Identifier of the Field
        /// </summary>
        public Guid FieldId { get; set; }
        /// <summary>
        /// Object of the Field
        /// </summary>s
        public Field Field { get; set; }
    }
}
