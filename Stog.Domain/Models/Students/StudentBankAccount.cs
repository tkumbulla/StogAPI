using Stog.Domain.Models.Institutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Students
{
    public class StudentBankAccount
    {
        public StudentBankAccount()
        {
            IBAN = String.Empty;
            FinancialInstitution = new FinancialInstitution();
        }
        /// <summary>
        /// The iban of the student
        /// </summary>
        public string IBAN { get; set; }
        /// <summary>
        /// The institution which this account belongs to
        /// </summary>
        public Guid FinanacialInstitutionId { get; set; }
        /// <summary>
        /// The institution which this account belongs to
        /// </summary>
        public virtual FinancialInstitution FinancialInstitution { get; set; }
    }
}
