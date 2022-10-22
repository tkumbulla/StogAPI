using Stog.Domain.Models.Enumerations;
using Stog.Domain.Models.Generics;
using Stog.Domain.Models.Institutions;

namespace Stog.Domain.Models.Students
{
    public class ProjectApplication :AuditableEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public ProjectApplication()
        {
            Project = new Project();
            Student = new Student(); 
            AdditionalNotes = String.Empty;
            ProposalFile = new Common.File();
        }
        /// <summary>
        /// The id of the project student is applying for  
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// Additional notes regarding to application
        /// </summary>
        public string AdditionalNotes { get; set; }
        /// <summary>
        /// The id of the student that is applying
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// The id of the file that is submited from the student as a proposal for the project
        /// </summary>
        public Guid ProposalFileId { get; set; }
        /// <summary>
        /// The status of the application
        /// </summary>
        public Status Status { get; set; }  
        /// <summary>
        /// Student that is applying
        /// </summary>
        public virtual Student Student { get; set; }  
        /// <summary>
        /// Project that the student is aplying for
        /// </summary>
        public virtual Project Project { get; set; }
        /// <summary>
        /// The object of the file that is submited from the student as a proposal for the project
        /// </summary>
        public virtual Common.File ProposalFile { get; set; }

    }
}
