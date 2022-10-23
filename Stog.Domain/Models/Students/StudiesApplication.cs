using Stog.Domain.Models.Enumerations;
using Stog.Domain.Models.Generics;
using Stog.Domain.Models.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Students
{
    public class StudiesApplication : AuditableEntity
    {
        ///// <summary>
        ///// Ctor
        ///// </summary>
        //public StudiesApplication()
        //{
        //    Program = new Program();
        //    Student = new Student();
        //    AdditionalNotes = String.Empty;
        //}
        /// <summary>
        /// The id of the program student is applying for  
        /// </summary>
        public Guid ProgramId { get; set; }
        /// <summary>
        /// Additional notes regarding to application
        /// </summary>
        public string AdditionalNotes { get; set; }
        /// <summary>
        /// The id of the student that is applying
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// The status of the application
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        /// Student that is applying
        /// </summary>
        public virtual Student Student { get; set; }
        /// <summary>
        /// Program that the student is aplying for
        /// </summary>
        public virtual Program Program { get; set; }

    }
}
