using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Stog.Application.Interfaces.User;
using Stog.Application.Models.User;
using Stog.Application.Results.Messages;
using Stog.Domain.Models.User;
using Swashbuckle.AspNetCore.Annotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : Controller
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly RoleManager<ApplicationRole> roleManager;
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="userManager"></param>
        /// <param name="roleManager"></param>
        /// <param name="configuration"></param>
        public AuthenticationController(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager, IConfiguration configuration, IUserService userService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _userService = userService;
        }

        /// <summary>
        /// Create a new user and assign them a new role
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] CreateUserModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);
            if (userExists != null)
                return BadRequest("A user with this email account already exists.");
            var studentResult = _userService.CreateStudent(model);
            if (!studentResult.Success) return BadRequest("Something went wrong. Please try again.");

            ApplicationUser user = new()
            {
                Id = Guid.NewGuid(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                NormalizedUserName = model.Username.ToLower(),
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                CreatedById = Guid.Parse("223FC8FD-641A-486C-B4C6-39D6F803BF03"),
                CreatedOnUtc = DateTime.Now,
                UserName = model.Username,
                StudentId = studentResult.Value
            };

            var result = await userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
                return BadRequest("Something went wrong. Please try again.");

            var role = await roleManager.FindByIdAsync(model.RoleId.ToString());
            if (role != null)
            {
                await userManager.AddToRoleAsync(user, role.Name);
            }
            return Ok();
        }

        /// <summary>
        /// Login with an existing account
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("login")]
        [SwaggerOperation("Log in")]
        public async Task<IActionResult> Login([FromBody] LogInModel model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            //var user = await userManager.Users.FirstOrDefaultAsync(x => x.UserName == model.Username);
            if (user == null)
            {
                return Unauthorized(new Message("Nuk ekziston përdoruesi"));
            }
            else
            {
                if (user.IsDeleted)
                {
                    return Unauthorized(new Message("Nuk ekziston përdoruesi"));
                }
                if (!user.IsActive)
                {
                    return Unauthorized(new Message("Përdoruesi nuk është përdorues aktiv."));
                }
                if (await userManager.CheckPasswordAsync(user, model.Password))
                {
                    var userRoles = await userManager.GetRolesAsync(user);
                    var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                    foreach (var userRole in userRoles)
                    {
                        authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                    }
                    IConfigurationRoot configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                    var x = _configuration.GetSection("JWT")["SecretKey"];

                    var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(x));

                    var token = new JwtSecurityToken(

                    expires: DateTime.Now.AddDays(30),
                    claims: authClaims,
                    signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
                    );


                    return Ok(new
                    {
                        token = new JwtSecurityTokenHandler().WriteToken(token),
                        expiration = token.ValidTo
                    });
                }
                else
                {
                    return Unauthorized(new Message("Fjalëkalimi i gabuar."));
                }
            }
        }

        /// <summary>
        /// Get current user details
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("CurrentUser")]
        [Authorize]
        [SwaggerOperation("Get data for the currently logged in user")]
        public async Task<IActionResult> GetCurrentUserAsync()
        {
            var user = await _userService.GetCurrentUserDetailsAsync();
            return Json(user);
        }

        /// <summary>
        /// POST: Change user password
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("ChangePassword")]
        [SwaggerOperation("Change user password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordModel model)
        {
            
            if (!ModelState.IsValid)
            {
                return BadRequest("Ju lutem plotësoni të gjitha fushat.");
            }
            var result = await _userService.ChangePassword(model);
            if (result.Success)
            {
                return Ok();
            }
            else
            {
                return BadRequest("Fjalëkalimi ekzistues nuk është i saktë.");
            }
        }

    }
}
