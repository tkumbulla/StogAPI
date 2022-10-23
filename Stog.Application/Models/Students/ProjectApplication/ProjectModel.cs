using Stog.Domain.Models.Enumerations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.Students.ProjectApplication
{
    public class ProjectModel
    {
        /// <summary>
        /// The identifier of the project 
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// The id of the file that is submited from the institution as a requirement list for the project
        /// </summary>
        public Guid RequirementFileId { get; set; }
        /// <summary>
        /// The virtual path of the requirement file
        /// </summary>
        public string? RequirementFileVirtualPath { get; set; }
        /// <summary>
        /// The content type of the requirement file
        /// </summary>
        public string? RequirementFileContentType { get; set; }
        /// <summary>
        /// The name of the requirement file
        /// </summary>
        public string? RequirementFileName { get; set; }
        /// <summary>
        /// The status of the application
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        ///   Title of the project
        /// </summary>
        public string? ProjectTitle { get; set; }

        /// <summary>
        ///   Description of the project
        /// </summary>
        public string? ProjectDescription { get; set; }
        /// <summary>
        /// Shows the institution that the project corresponds to
        /// </summary>
        public Guid FinancialInstitutionId { get; set; }
        /// <summary>
        /// Shows the name of the institution the project corresponds to
        /// </summary>
        public string? FinancialInstitutionName { get; set; }
        /// <summary>
        /// Start date
        /// </summary>
        public string? StartDate { get; set; }
        /// <summary>
        /// End date
        /// </summary>
        public string? EndDate { get; set; }
        /// <summary>
        /// The budget of the project
        /// </summary>
        public decimal Budget { get; set; }
    }
}
