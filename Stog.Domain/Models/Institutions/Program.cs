using Stog.Domain.Models.Enumerations;
using Stog.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Institutions
{
    public class Program : AuditableEntity
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Program()
        {
            Name = String.Empty;
            University = new University();
        }
        /// <summary>
        /// Name of the program
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// University that this program corresponds to
        /// </summary>
        public Guid UniversityId { get; set; }
        /// <summary>
        /// Type of the coverage full or half
        /// </summary>
        public Coverage Coverage { get; set; } 
        /// <summary>
        /// The total amount covered by the financial institution
        /// </summary>
        public decimal AmountCovered { get; set; }
        /// <summary>
        /// University that this program corresponds to
        /// </summary>
        public virtual University University { get; set; }
        
    }
}
