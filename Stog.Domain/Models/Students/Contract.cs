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
    public class Contract : AuditableEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Contract()
        {
            FinancialInstitution = new FinancialInstitution();
            Student = new Student();
            File = new Common.File();
        }
        /// <summary>
        /// The id of the institution this contract is signed with
        /// </summary>
        public Guid FinancialInstitutionId { get; set; }
        /// <summary>
        /// The id of the student that has signed the contract
        /// </summary>
        public Guid StudentId { get; set; }
        /// <summary>
        /// The id of the contract original signed file
        /// </summary>
        public Guid FileId { get; set; }
        /// <summary>
        /// The start date of the contract
        /// </summary>
        public DateTime StartDate { get; set; }
        /// <summary>
        /// The end date of the products
        /// </summary>
        public DateTime EndDate { get; set; }
        /// <summary>
        /// Student that has signed the contract
        /// </summary>
        public virtual Student Student { get; set; }
        /// <summary>
        /// The obj of the institution this contract is signed with
        /// </summary>
        public virtual FinancialInstitution FinancialInstitution { get; set; }
        /// <summary>
        /// The object of the contract original signed file
        /// </summary>
        public virtual Common.File File { get; set; }
    }
}
