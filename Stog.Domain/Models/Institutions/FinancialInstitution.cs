using Stog.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Institutions
{
    public class FinancialInstitution : AuditableEntity
    {

        /// <summary>
        /// Creates a new instance of the <see cref="\FinancialInstitution"/> object.
        /// </summary>
        public FinancialInstitution()
        {
            Name = string.Empty;
            Description = string.Empty;
            Email = string.Empty;
            PhoneNumber = string.Empty;
        }
        /// <summary>
        ///   Name of the Institution
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        ///   Description of the Institution
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Institution Email 
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Phone number of the Institution
        /// </summary>
        public string PhoneNumber { get; set; }
        /// <summary>
        /// The url of the institution's official website
        /// </summary>
        public string WebsiteUrl { get; set; }
        /// <summary>
        /// The nuis of the institituion
        /// </summary>
        public string Nuis { get; set; }
    }
}
