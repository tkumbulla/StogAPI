using Stog.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Institutions
{
    public class University : AuditableEntity
    {
        /// <summary>
        /// ctor
        /// </summary>
       public University()
        {
            Name = String.Empty;
            FinancialInstitution = new FinancialInstitution();
        }
        /// <summary>
        /// Name of the university
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Indtitution that this university corresponds to
        /// </summary>
        public Guid FinancialInstitutionId { get; set; }
        /// <summary>
        /// Indtitution that this university corresponds to
        /// </summary>
        public virtual FinancialInstitution FinancialInstitution { get; set; }

    }
}
