using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.User
{
    /// <summary>
    /// The model to be used for the Log In / Authorization 
    /// </summary>
    public class LogInModel
    {
        public LogInModel()
        {
            Username = String.Empty;
            Password = String.Empty;
        }
        /// <summary>
        /// Username property to keep the value of username 
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Password Value to be used for authentication 
        /// </summary>
        public string Password { get; set; }
    }
}
