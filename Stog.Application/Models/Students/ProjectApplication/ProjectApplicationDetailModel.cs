using Stog.Domain.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.Students.ProjectApplication
{
    /// <summary>
    /// Details for a single item on the list of user applications
    /// </summary>
    public class ProjectApplicationDetailModel
    {
        /// <summary>
        /// The identifier of the application
        /// </summary>
        public Guid ApplicationId { get; set; }
        /// <summary>
        /// The identifier of the project the application corresponds to
        /// </summary>
        public Guid ProjectId { get; set; }
        ///// <summary>
        ///// Additional notes regarding to application
        ///// </summary>
        //public string? AdditionalNotes { get; set; }
        ///// <summary>
        ///// The id of the file that is submited from the student as a proposal for the project
        ///// </summary>
        //public Guid ProposalFileId { get; set; }
        ///// <summary>
        ///// The virtual path of the proposal file
        ///// </summary>
        //public string ProposalFileVirtualPath { get; set; }
        ///// <summary>
        ///// The content type of the proposal file
        ///// </summary>
        //public string ProposalFileContentType { get; set; }
        ///// <summary>
        ///// 
        ///// </summary>
        //public string ProposalFileName { get; set; }
        /// <summary>
        /// The status of the application
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        ///   Title of the project
        /// </summary>
        public string? ProjectTitle { get; set; }

        ///// <summary>
        /////   Description of the project
        ///// </summary>
        //public string? ProjectDescription { get; set; }
        /// <summary>
        /// Shows the institution that the project corresponds to
        /// </summary>
        public Guid FinancialInstitutionId { get; set; }
        /// <summary>
        /// Shows the name of the institution the project corresponds to
        /// </summary>
        public string? FinancialInstitutionName { get; set; }
        /// <summary>
        /// The date the application was submitted
        /// </summary>
        public string? ApplicationDate { get; set; }
        /// <summary>
        /// The budget of the project
        /// </summary>
        public decimal Budget { get; set; }
    }
}
