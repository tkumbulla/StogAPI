using Stog.Application.Models.User;
using Stog.Application.Results;
using Stog.Domain.Models.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Interfaces.User
{
    /// <summary>
    /// Defines the structure of the IUserService interface.
    /// This interface defines all the operations regarding user management.
    /// </summary>
    public interface IUserService
    {
        /// <summary>
        /// Get all users according to filters
        /// </summary>
        /// <returns></returns>
        Task<HashSet<UserModel>> GetUsers();

        /// <summary>
        /// Get all roles.
        /// </summary>
        /// <returns>.</returns>
        IQueryable<RoleModel> GetRoles();

        /// <summary>
        /// Get user role.
        /// </summary>
        /// <param name="user">.</param>
        /// <returns>.</returns>
        Task<string> GetUserRoleAsync(ApplicationUser user);

        /// <summary>
        /// Get logged in user details.
        /// </summary>
        /// <returns>.</returns>
        Task<UserModel> GetCurrentUserDetailsAsync();

        /// <summary>
        /// Get user details by id.
        /// </summary>
        /// <param name="id">.</param>
        /// <returns>User View Model.</returns>
        UserModel GetUserById(Guid id);
        /// <summary>
        /// Get the details of a user from its identifier
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        Task<UserModel> GetUserByIdAsync(Guid id);

        /// <summary>
        /// Get current user id.
        /// </summary>
        /// <returns>.</returns>
        Guid GetCurrentUserId();

        /// <summary>
        /// Update an existing user.
        /// </summary>
        /// <param name="user">.</param>
        /// <returns>.</returns>
        Task<Result> UpdateUser(UserModel user);

        /// <summary>
        /// Delete an existing user.
        /// </summary>
        /// <param name="user">.</param>
        /// <returns>Fail if the user cannot be found and Ok if the method is completed successfully.</returns>
        Task<Result> DeleteUser(UserModel user);
        /// <summary>
        /// Recover a deleted user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Result> RecoverUser(Guid id);
        /// <summary>
        /// Change user password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        Task<Result> ChangePassword(ChangePasswordModel model);
    }
}
