using Stog.Application.Results;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using System.Globalization;
using System.Security.Authentication;
using Microsoft.AspNetCore.Authentication;
using Stog.Domain.Models.User;

namespace Stog.Application.Services.Authentication
{
    /// <summary>
    /// Authentication and authorization management service.
    /// </summary>
    public class AuthenticationService : Stog.Application.Interfaces.Authentication.IAuthenticationService
    {
        // dependencies
        private readonly IHttpContextAccessor _httpContextAccessor;

        private AuthenticatedUser? _cachedUser;

        public AuthenticationService(
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        /// <summary>
        /// Sign in a user.
        /// </summary>
        /// <param name="user">User to authenticate.</param>user
        /// <param name="rememberMe">Value whether to persist the cookie.</param>user
        /// <param name="cancellationToken">Cancellation token.</param>
        public virtual async Task<Result> SignInAsync(
            ApplicationUser user,
            bool rememberMe,
            CancellationToken cancellationToken = default)
        {
            // everything went well, create claims
            var claims = BuildClaims(user);


            //create principal for the current authentication scheme
            var userIdentity = new ClaimsIdentity(claims, AuthenticationSettings.AuthenticationScheme);
            var userPrincipal = new ClaimsPrincipal(userIdentity);

            //set value indicating whether session is persisted and the time at which the authentication was issued
            var authenticationProperties = new AuthenticationProperties
            {
                IsPersistent = rememberMe,
                IssuedUtc = DateTime.UtcNow
            };

            if (_httpContextAccessor.HttpContext != null)
                //sign in
                await _httpContextAccessor.HttpContext.SignInAsync(AuthenticationSettings.AuthenticationScheme, userPrincipal, authenticationProperties);
            else return Result.Fail("Unable to sign in.");
            return Result.Ok();
        }

        /// <summary>
        /// Sign out.
        /// </summary>
        public virtual async Task SignOutAsync()
        {
            //reset cached user
            _cachedUser = null;

            if(_httpContextAccessor.HttpContext != null)
            //and sign out from the current authentication scheme
            await _httpContextAccessor.HttpContext.SignOutAsync(AuthenticationSettings.AuthenticationScheme);
        }

        /// <summary>
        /// Gets currently authenticated user.
        /// </summary>
        public virtual async Task<AuthenticatedUser?> GetAuthenticatedUserAsync()
        {
            //whether there is a cached user
            if (_cachedUser != null)
                return _cachedUser;

            //try to get authenticated user identity
            var authenticatedUser = await GetAuthenticatedUserFromClaimsAsync();

            _cachedUser = authenticatedUser;

            return _cachedUser;
        }

        /// <summary>
        /// Gets currently authenticated user.
        /// </summary>
        public virtual async Task<AuthenticatedUser> GetAuthenticatedUserOrGuestAsync()
        {
            //whether there is a cached user
            if (_cachedUser != null)
                return _cachedUser;

            //try to get authenticated user identity
            var authenticatedUser = await GetAuthenticatedUserFromClaimsAsync() ?? throw new AuthenticationException("User not found.");

            _cachedUser = authenticatedUser;

            return _cachedUser;
        }

        #region Helpers

        private static List<Claim> BuildClaims(ApplicationUser user)
        {
            var claims = new List<Claim>();

            var idClaim = new Claim(ClaimTypes.Sid, user.Id.ToString(), ClaimValueTypes.String, AuthenticationSettings.ClaimsIssuer);

            var nameClaim = new Claim(ClaimTypes.Name, user.FirstName, ClaimValueTypes.String, AuthenticationSettings.ClaimsIssuer);

            var usernameClaim = new Claim(ClaimTypes.NameIdentifier, user.UserName, ClaimValueTypes.String,
                AuthenticationSettings.ClaimsIssuer);

            var dateLoggedInClaim = new Claim(ClaimTypes.AuthenticationInstant,
                DateTime.UtcNow.ToString(CultureInfo.InvariantCulture), ClaimValueTypes.DateTime,
                AuthenticationSettings.ClaimsIssuer);

            List<Claim> roleClaim = new List<Claim>();
            foreach (var role in user.UserRoles)
            {
                roleClaim.Add(new Claim(ClaimTypes.Role, role.Role.Name, ClaimValueTypes.String,
                AuthenticationSettings.ClaimsIssuer));
            }

            claims.Add(idClaim);
            claims.Add(nameClaim);
            claims.Add(usernameClaim);
            claims.Add(dateLoggedInClaim);

            claims.AddRange(roleClaim);

            return claims;
        }

        private async Task<AuthenticatedUser?> GetAuthenticatedUserFromClaimsAsync()
        {
            if (_httpContextAccessor.HttpContext == null) throw new AuthenticationException("Internal error. Null value for HttpContext.");
            var authenticateResult =
                await _httpContextAccessor.HttpContext.AuthenticateAsync(AuthenticationSettings.AuthenticationScheme);

                if (!authenticateResult.Succeeded)
                    return null;
                //throw new AuthenticationException("No user is logged in.");


                // get claims
                var idClaim = authenticateResult.Principal.FindFirst(claim =>
                    claim.Type == ClaimTypes.Sid &&
                    claim.Issuer.Equals(AuthenticationSettings.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

                var nameClaim = authenticateResult.Principal.FindFirst(claim =>
                    claim.Type == ClaimTypes.Name &&
                    claim.Issuer.Equals(AuthenticationSettings.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

                var usernameClaim = authenticateResult.Principal.FindFirst(claim =>
                    claim.Type == ClaimTypes.NameIdentifier &&
                    claim.Issuer.Equals(AuthenticationSettings.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

                var roleClaim = authenticateResult.Principal.FindAll(claim =>
                    claim.Type == ClaimTypes.Role &&
                    claim.Issuer.Equals(AuthenticationSettings.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));

            var studentIdClaim = authenticateResult.Principal.FindFirst(claim =>
                    claim.Type == ClaimTypes.PrimarySid &&
                    claim.Issuer.Equals(AuthenticationSettings.ClaimsIssuer, StringComparison.InvariantCultureIgnoreCase));
            // means user is found
            if (idClaim == null)
                    return null;
                //throw new AuthenticationException("No user is logged in. (Claims not found)");

                var authenticatedUser = new AuthenticatedUser
                {
                    Id = Guid.Parse(idClaim.Value),
                    Name = nameClaim?.Value ?? "",
                    Username = usernameClaim?.Value ?? "",
                    StudentId = Guid.Parse(studentIdClaim?.Value ?? "")
                };
                return authenticatedUser;
        }

        #endregion
    }
}
