﻿using FluentValidation;
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
            Username = String.Empty;
            Email = String.Empty;
            Password = String.Empty;
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
        /// Property to keep the user's username
        /// </summary>
        public string Username { get; set; }
        /// <summary>
        /// Property to keep user email
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Property to keep the user's password
        /// </summary>
        public string Password { get; set; }
        /// <summary>
        /// Foreign key used to assign to a user a role 
        /// </summary>
        public Guid RoleId { get; set; }
        /// <summary>
        /// Identifier of the current logged in user that is creating the new user
        /// </summary>
        public Guid CreatedById { get; set; }
        /// <summary>
        /// Indicates that the user is an active user of the system
        /// </summary>
        public bool IsActive { get; set; }
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