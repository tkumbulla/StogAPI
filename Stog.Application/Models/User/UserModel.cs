using Stog.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.User
{
    /// <summary>
    /// Model needed to display the needed information for users
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Parameterless constructor
        /// </summary>
        public UserModel() 
        {
            Id = new Guid();
            FirstName = string.Empty;
            LastName = string.Empty;
            UserName = string.Empty;
            CreatedOnUtc = DateTime.UtcNow;
            UpdatedOnUtc = DateTime.UtcNow;
            IsActive = false;
            Email = string.Empty;
            Password = string.Empty;
            Role = string.Empty;
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
            DisplayUserName = string.Empty;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="user"></param>
        public UserModel(ApplicationUser user)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            UserName = user.UserName;
            CreatedOnUtc = user.CreatedOnUtc;
            UpdatedOnUtc = user.UpdatedOnUtc;
            IsActive = user.IsActive;
            Email = user.Email;
            Password = user.PasswordHash;
            Role = string.Empty;
            CreatedBy = string.Empty;
            UpdatedBy = string.Empty;
            DisplayUserName = user.UserName;
        }
        /// <summary>
        /// User id
        /// </summary>
        public Guid? Id { get; set; }
        /// <summary>
        ///  First name of the user
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        ///  Last name of the user
        /// </summary>

        public string LastName { get; set; }
        /// <summary>
        ///  Full name of the user
        /// </summary>
        public string FullName => FirstName + " " + LastName;
        /// <summary>
        /// User's username (used for logging in)
        /// </summary>
        public string UserName { get; set; }
        /// <summary>
        /// User's email adress
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Gets or sets the UTC date time when the entity was first created.
        /// </summary>
        public DateTime CreatedOnUtc { get; set; }

        /// <summary>
        /// Gets or sets the UTC date time when the entity was last updated.
        /// </summary>
        public DateTime? UpdatedOnUtc { get; set; }
        /// <summary>
        /// Shows if the entity is active or not
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// Shows the user's password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Unique identifier of the user's role
        /// </summary>
        public Guid? Roleid { get; set; }

        /// <summary>
        /// Name of the role
        /// </summary>
        public string Role { get; set; }
        /// <summary>
        /// The identifier of the user that created the user.
        /// </summary>
        public Guid CreatedById { get; set; }
        /// <summary>
        /// The full name of the user that created the user.
        /// </summary>
        public string CreatedBy { get; set; }
        /// <summary>
        /// The identifier of the user that last updated the user.
        /// </summary>
        public Guid? UpdatedById { get; set; }
        /// <summary>
        /// The full name of the user that last updated the user.
        /// </summary>
        public string UpdatedBy { get; set; }
        /// <summary>
        /// Display username
        /// </summary>
        public string DisplayUserName { get; set; }
        /// <summary>
        /// True if the record is deleted
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
