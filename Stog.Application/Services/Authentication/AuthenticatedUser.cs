using Stog.Application.Models;
using Stog.Domain.Models.User;
using System;
using System.Collections.Generic;

namespace Stog.Application.Services.Authentication
{

    /// <summary>
    /// Represents an authenticated user.
    /// </summary>
    public class AuthenticatedUser : BaseModel
    {
        public AuthenticatedUser()
        {
            Name = string.Empty;
            Username = string.Empty;
            Password = string.Empty;
            UserRoles = new List<UserRole>();
        }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the role.
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; }

        /// <summary>
        /// Gets or sets code list detail id.
        /// </summary>
        public Guid? CodeListDetailId { get; set; }

        /// <summary>
        /// Gets or sets code list detail name.
        /// </summary>
        public string? CodeListDetailValue { get; set; }
        public Guid? StudentId { get; set; }
    }
}
