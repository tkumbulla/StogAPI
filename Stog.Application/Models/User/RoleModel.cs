using Stog.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.User
{
    /// <summary>
    /// Class used to display role properties.
    /// </summary>
    public class RoleModel
    {
        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public RoleModel()
        {
            Id = new Guid();
            Name = String.Empty;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="role"></param>
        public RoleModel(ApplicationRole role)
        {
            Id = role.Id;
            Name = role.Name;   
        }
        /// <summary>
        /// Gets or sets the identifier of the role
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// Gets or sets the name of the role
        /// </summary>
        public string Name { get; set; }
    }
}
