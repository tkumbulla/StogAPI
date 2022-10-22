using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.User
{
    /// <summary>
    /// Model for changing user password
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// ctor
        /// </summary>
        public ChangePasswordModel()
        {
            UserId = new Guid();
            OldPassword = string.Empty;
            NewPassword = string.Empty; 
            ConfirmPassword = string.Empty; 
        }
        /// <summary>
        /// Gets or sets the identifier of the user
        /// </summary>
        public Guid UserId { get; set; }
        /// <summary>
        /// Gets or sets the old password of the user
        /// </summary>
        public string OldPassword { get; set; }
        /// <summary>
        /// Gets or sets the new password for the user
        /// </summary>
        public string NewPassword { get; set; }
        /// <summary>
        /// Gets or sets the confirmation of the new password for the user
        /// </summary>
        public string ConfirmPassword { get; set; }
        /// <summary>
        /// Validator for the ChangePasswordModel class
        /// </summary>
        public class Validator : AbstractValidator<ChangePasswordModel>
        {
            /// <summary>
            /// ctor
            /// </summary>
            public Validator()
            {
                //simple rules:
                RuleFor(model => model.OldPassword).NotNull().WithMessage("Please enter your old password. This is a required field.");
                RuleFor(model => model.NewPassword).NotNull().WithMessage("Please enter your new password. This is a required field.");
                RuleFor(model => model.ConfirmPassword).NotNull().WithMessage("Please enter your password confirmation. This is a required field.");
                RuleFor(model => model.NewPassword).Equal(model => model.ConfirmPassword).When(model => !String.IsNullOrWhiteSpace(model.NewPassword)).WithMessage("New password and its confirmation do not match.");
            }
        }
    }
}
