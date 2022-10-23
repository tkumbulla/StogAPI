using FluentValidation;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.User
{
    public class CreateUserModel
    {
        /// <summary>
        /// ctor
        /// </summary>
        public CreateUserModel()
        {
            FirstName = String.Empty;
            LastName = String.Empty;
            Email = String.Empty;
            Password = String.Empty;
            SSN = String.Empty;
        }
        /// <summary>
        /// Property to keep the user's first name 
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// Property to keep the user's last name
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// Property to keep user email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Property to keep the user's password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Indicates that the user is an active user of the system
        /// </summary>
        public bool IsActive { get; set; }
        /// <summary>
        /// The social security number of the student
        /// </summary>
        public string SSN { get; set; }
        /// <summary>
        /// Validation
        /// </summary>
        public class Validator : AbstractValidator<CreateUserModel>
        {
            public Validator()
            {
                //simple rules:
                RuleFor(model => model.Password).NotNull().WithMessage("Ju lutem vendosni fjalëkalimin. Kjo është një fushë e detyrueshme.");
            }
        }
    }
}
