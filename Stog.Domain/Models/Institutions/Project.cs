using Stog.Domain.Models.Generics;

namespace Stog.Domain.Models.Institutions
{
    public class Project : AuditableEntity
    {

        /// <summary>
        /// Creates a new instance of the <see cref="\Project"/> object.
        /// </summary>
        public Project()
        {
            Title = string.Empty;
            Description = string.Empty;
            FinancialInstitution = new FinancialInstitution();
            RequirementFile = new Common.File();
        }
        /// <summary>
        ///   Title of the project
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        ///   Description of the project
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// The opening date of the applications for this project
        /// </summary>
        public DateOnly StartDate { get; set; }
        /// <summary>
        /// Closing date of the applications
        /// </summary>
        public DateOnly EndDate { get; set; }
        /// <summary>
        /// Estimated complition time of the project from the starting date
        /// </summary>
        public int EstimatedTimePeriodMonths { get; set; }
        /// <summary>
        /// Shows the institution that this project corresponds to
        /// </summary>
        public Guid FinancialInstitutionId { get; set; }
        /// <summary>
        /// Total budget offered by the bank for this project
        /// </summary>
        public decimal Budget { get; set; }
        /// <summary>
        /// Shows the requirement documentation of the project uploaded by the istitution 
        /// </summary>
        public Guid RequirementFileId { get; set; }

        #region Referential
        /// <summary>
        /// Shows the institution that this project corresponds to
        /// </summary>
        public virtual FinancialInstitution FinancialInstitution { get; set; }
        /// <summary>
        /// Shows the requirement documentation of the project uploaded by the istitution 
        /// </summary>
        public virtual Common.File RequirementFile { get; set; } 
        #endregion
    }
}
