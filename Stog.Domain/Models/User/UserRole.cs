using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Domain.Models.User
{
    public class UserRole : IdentityUserRole<Guid>
    {
        public UserRole()
        {
            User = new ApplicationUser();
            Role = new ApplicationRole();
        }
        public virtual ApplicationUser User { get; set; }
        public virtual ApplicationRole Role { get; set; }
    }
}
