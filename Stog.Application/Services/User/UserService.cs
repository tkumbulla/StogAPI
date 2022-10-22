using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Stog.Application.Interfaces.User;
using Stog.Application.Models.User;
using Stog.Application.Results;
using Stog.Domain.Interfaces;
using Stog.Domain.Models.Students;
using Stog.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Services.User
{
    /// <summary>
    /// Service that will handle all operations regarding user management.
    /// </summary>
    public class UserService : IUserService
    {
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly RoleManager<ApplicationRole> _roleManager;

        private readonly IHttpContextAccessor _httpContext;

        private readonly IRepository<Student> _studentRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="UserService"/> class.
        /// </summary>
        /// <param name="userManager">.</param>
        /// <param name="roleManager">.</param>
        /// <param name="httpContext">The httpContext<see cref="IHttpContextAccessor"/>.</param>
        public UserService(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IHttpContextAccessor httpContext, IRepository<Student> studentRepository)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _httpContext = httpContext;
            _studentRepository = studentRepository;
        }

        /// <summary>
        /// Get list of filtered users
        /// </summary>
        /// <param name="firstName"></param>
        /// <param name="lastName"></param>
        /// <param name="email"></param>
        /// <param name="roleId"></param>
        /// <param name="isActive"></param>
        /// <param name="isDeleted"></param>
        /// <returns></returns>
        public async Task<HashSet<UserModel>> GetUsers()
        {
            HashSet<UserModel> model = new HashSet<UserModel>();
                var list = _userManager.Users
                .Where(x => x.IsDeleted == false);
                foreach (var user in list)
                {
                    UserModel userModel = new UserModel();
                    var userRoles = await _userManager.GetRolesAsync(user);
                    var roleData = userRoles.FirstOrDefault() != null ? await _roleManager.FindByNameAsync(userRoles.FirstOrDefault()) : null;
                    userModel.Id = user.Id;
                    userModel.UserName = user.DisplayUserName == null ? user.UserName : user.DisplayUserName;
                    userModel.FirstName = user.FirstName;
                    userModel.LastName = user.LastName;
                    userModel.IsActive = user.IsActive;
                    userModel.Email = user.Email;
                    userModel.CreatedOnUtc = user.CreatedOnUtc;
                    userModel.Roleid = roleData != null ? roleData.Id : null;
                    userModel.Role = roleData != null ? roleData.Name : string.Empty;
                    userModel.CreatedById = user.CreatedById;
                    userModel.UpdatedById = user.UpdatedById;
                    userModel.CreatedBy = _userManager.Users.First(x => x.Id == x.CreatedById).FullName;
                    userModel.UpdatedOnUtc = user.UpdatedOnUtc;
                    userModel.IsDeleted = user.IsDeleted;
                    model.Add(userModel);
                }
            return model;
        }

        /// <summary>
        /// Get all roles from the database.
        /// </summary>
        /// <returns>.</returns>
        public IQueryable<RoleModel> GetRoles()
        {
            var roles = _roleManager.Roles.Select(x => new RoleModel
            {
                Id = x.Id,
                Name = x.Name,
            });

            return roles;
        }

        /// <summary>
        /// Get logged-in user details.
        /// </summary>
        /// <returns>.</returns>
        public async Task<UserModel> GetCurrentUserDetailsAsync()
        {
            var email = string.Empty;
            if(_httpContext.HttpContext != null)
            {
                if (_httpContext.HttpContext.User.Identity is ClaimsIdentity identity)
                {
                    email = identity.FindFirst(ClaimTypes.Name)?.Value;
                    var currentUser = _userManager.Users.FirstOrDefault(x => x.UserName == email);
                    if(currentUser == null) throw new ArgumentNullException("No users are logged in");
                    var currentUserDetails = new UserModel
                    {
                        Id = currentUser.Id,
                        FirstName = currentUser.FirstName,
                        LastName = currentUser.LastName,
                        Email = currentUser.Email,
                        CreatedOnUtc = currentUser.CreatedOnUtc,
                        IsActive = currentUser.IsActive,
                        Role = await GetUserRoleAsync(currentUser),
                        UserName = currentUser.UserName
                    };
                    return currentUserDetails;
                }
                else
                    throw new ArgumentNullException("No users are logged in");
            }
            else throw new ArgumentNullException("No users are logged in");
        }

        /// <summary>
        /// Get the specified user's role.
        /// </summary>
        /// <param name="user">.</param>
        /// <returns>Role name .</returns>
        public async Task<string> GetUserRoleAsync(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return roles.FirstOrDefault() ?? string.Empty;
        }

        /// <summary>
        /// Get user by id.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>User View Model.</returns>
        public UserModel GetUserById(Guid id)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new ArgumentNullException("Could not find user.");
            }
            else
            {
                var student = _studentRepository.TableNoTracking.Include(x => x.StudentCard).FirstOrDefault(x => x.Id == user.StudentId);
                if(student == null) throw new ArgumentNullException("Could not find student.");
                var userDetails = new UserModel(user);
                userDetails.SSN = student.SSN;
                userDetails.PhysicalPath = student.StudentCard?.PhysicalPath;
                userDetails.VirtualPath = student.StudentCard?.VirtualPath;
                userDetails.FileName = student.StudentCard?.Name;
                userDetails.ContentType = student.StudentCard?.ContentType;
                return userDetails;
            }
        }

        /// <summary>
        /// Get user by id asynchronously.
        /// </summary>
        /// <param name="id">The id<see cref="Guid"/>.</param>
        /// <returns>The <see cref="Task{UserModel}"/>.</returns>
        public async Task<UserModel> GetUserByIdAsync(Guid id)
        {
            var user = await _userManager.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null)
            {
                throw new ArgumentNullException("Could not find user.");
            }
            else
            {
                var userDetails = new UserModel(user);
                return userDetails;
            }
        }

        /// <summary>
        /// Get logged-in user id.
        /// </summary>
        /// <returns>.</returns>
        public Guid GetCurrentUserId()
        {
            var email = string.Empty;
            if(_httpContext.HttpContext == null) throw new NullReferenceException("Internal server error");
            if (_httpContext.HttpContext.User.Identity is ClaimsIdentity identity)
            {
                email = identity.FindFirst(ClaimTypes.Name)?.Value;
                var currentUser = _userManager.Users.FirstOrDefault(x => x.UserName == email);
                return currentUser == null ? new Guid() : currentUser.Id;
            }
            else
                throw new ArgumentNullException("No users are logged in");
        }

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="user">.</param>
        /// <returns>Result.</returns>
        public async Task<Result> UpdateUser(UserModel user)
        {
            var model = _userManager.Users.FirstOrDefault(x => x.Id == user.Id);
            if (model == null)
            {
                return Result.Fail("Could not find user.");
            }
            model.FirstName = user.FirstName;
            model.LastName = user.LastName;
            model.Email = user.Email;
            model.IsActive = user.IsActive;
            model.UpdatedById = GetCurrentUserId();
            model.UpdatedOnUtc = DateTime.UtcNow;

            //if (user.Password != null)
            //{
            //    model.PasswordHash = _userManager.PasswordHasher.HashPassword(model, user.Password);
            //}

            await _userManager.UpdateAsync(model);

            if (user.Roleid != null)
            {
                var currentRoles = await _userManager.GetRolesAsync(model);

                if (currentRoles != null)
                {
                    await _userManager.RemoveFromRolesAsync(model, currentRoles);
                }
                var addToRole = _roleManager.Roles.FirstOrDefault(x => x.Id == user.Roleid && !x.IsDeleted);
                if (addToRole != null)
                {
                    await _userManager.AddToRoleAsync(model, addToRole.Name);
                }
            }

            return Result.Ok();
        }

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="user">.</param>
        /// <returns>Fail if the user cannot be found and OK if the method is completed successfully.</returns>
        public async Task<Result> DeleteUser(UserModel user)
        {
            var model = _userManager.Users.FirstOrDefault(x => x.Id == user.Id);
            if (model == null)
            {
                return Result.Fail("Could not find user.");
            }
            model.UserName = model.UserName + DateTime.Now.ToString();
            await _userManager.UpdateNormalizedUserNameAsync(model);
            await _userManager.DeleteAsync(model);
            return Result.Ok();
        }
        /// <summary>
        /// Recover a user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<Result> RecoverUser(Guid id)
        {
            var model = _userManager.Users.FirstOrDefault(x => x.Id == id);
            if (model == null)
            {
                return Result.Fail("Could not find user.");
            }
            var usernameTaken = _userManager.Users.Any(x => x.DisplayUserName == model.DisplayUserName);
            if (usernameTaken)
                return Result.Fail("Cannot recover user.");
            if (model.DisplayUserName != null && !string.IsNullOrWhiteSpace(model.DisplayUserName) && !string.IsNullOrEmpty(model.DisplayUserName))
            {
                model.UserName = model.DisplayUserName;
                await _userManager.UpdateNormalizedUserNameAsync(model);
                await _userManager.UpdateAsync(model);
            }
            model.IsDeleted = false;
            await _userManager.UpdateAsync(model);
            return Result.Ok();
        }
        /// <summary>
        /// Implements the <see cref="ChangePassword(ChangePasswordViewModel)"/> method of interface.
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public async Task<Result> ChangePassword(ChangePasswordModel model)
        {
            var user = _userManager.Users.FirstOrDefault(x => x.Id == model.UserId);
            if (user == null) return Result.Fail("Could not find user.");
            var result = await _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (result.Succeeded)
            {
                return Result.Ok();
            }
            else
            {
                return Result.Fail("Cannot change password. Password must contain at least 8 characters.");
            }
        }
        /// <summary>
        /// Create a new student
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public Result<Guid> CreateStudent(CreateUserModel model)
        {
            Student student = new Student()
            {
                SSN = model.SSN,
                StudentCardId = Guid.Parse(""),
                CreatedById = Guid.Parse("223FC8FD-641A-486C-B4C6-39D6F803BF03"),
                CreatedOnUtc = DateTime.UtcNow,
                IsActive = true,
                IsDeleted = false
            };
            _studentRepository.Insert(student);
            return Result.Ok<Guid>(student.Id);
        }
    }
}
