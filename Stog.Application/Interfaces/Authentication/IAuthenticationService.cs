﻿using Stog.Application.Results;
using Stog.Application.Services.Authentication;
using Stog.Domain.Models.User;

namespace Stog.Application.Interfaces.Authentication
{
    /// <summary>
    /// Authentication and authorization management service.
    /// </summary>
    public interface IAuthenticationService
    {
        /// <summary>
        /// Sign in a user.
        /// </summary>
        /// <param name="user">User to authenticate.</param>user
        /// <param name="rememberMe">Value whether to persist the cookie.</param>user
        /// <param name="cancellationToken">Cancellation token.</param>
        Task<Result> SignInAsync(
            ApplicationUser user,
            bool rememberMe,
            CancellationToken cancellationToken = default);

        /// <summary>
        /// Sign out.
        /// </summary>
        Task SignOutAsync();

        /// <summary>
        /// Gets currently authenticated user.
        /// </summary>
        Task<AuthenticatedUser?> GetAuthenticatedUserAsync();

        /// <summary>
        /// Gets currently authenticated user or the default guest.
        /// </summary>
        Task<AuthenticatedUser> GetAuthenticatedUserOrGuestAsync();
    }
}
