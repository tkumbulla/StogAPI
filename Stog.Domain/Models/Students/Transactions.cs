using Stog.Domain.Models.Generics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Students
{
    public class Transactions : AuditableEntity
    {
        /// <summary>
        /// Ctor
        /// </summary>
        public Transactions() { 
            Description = string.Empty;
        }
        /// <summary>
        /// Description of the transaction
        /// </summary>
        public string Description { get; set; }  
        /// <summary>
        ///  Amount of the transaction
        /// </summary>
        public decimal Amount { get; set; }
        /// <summary>
        /// Shows if it is incoming transaction or outgoing
        /// </summary>
        public bool IsIncoming { get; set; }
        /// <summary>
        /// Shows the date when the transaction is done
        /// </summary>
        public DateTime TransactionDate { get; set; } 


    }
}
