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
        /// <summary>
        /// The status of the application
        /// </summary>
        public Status Status { get; set; }
        /// <summary>
        ///   Title of the project
        /// </summary>
        public string? ProjectTitle { get; set; }
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
