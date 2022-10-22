using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.User
{
    /// <summary>
    /// Represents the UserRole entity
    /// </summary>
    public class UserRole : IdentityUserRole<Guid>
    {
        /// <summary>
        /// ctor
        /// </summary>
        public UserRole()
        {
            User = new ApplicationUser();
            Role = new ApplicationRole();
        }
        /// <summary>
        /// Gets or sets the user
        /// </summary>
        public virtual ApplicationUser User { get; set; }
        /// <summary>
        /// Gets or sets the role
        /// </summary>
        public virtual ApplicationRole Role { get; set; }
    }
}
