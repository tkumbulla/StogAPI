using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.Students
{
    public class Student
    {
        /// <summary>
        /// ctor
        /// </summary>
        public Student()
        {
            SSN = String.Empty;
            StudentCard = new Common.File();
        }
        /// <summary>
        /// The social security number of the student
        /// </summary>
        public string SSN { get; set; }
        /// <summary>
        /// The file that corresponds to the student id card
        /// </summary>
        public Guid StudentCardId { get; set; }
        /// <summary>
        /// The file that corresponds to the student id card
        /// </summary>
        public virtual Common.File StudentCard { get;set; }
    }
}
