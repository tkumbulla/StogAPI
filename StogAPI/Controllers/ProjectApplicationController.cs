using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stog.Application.Interfaces.Authentication;
using Stog.Application.Interfaces.Students;
using Swashbuckle.AspNetCore.Annotations;

namespace StogAPI.Controllers
{
    /// <summary>
    /// Controller that will handle all requests regarding project applications
    /// </summary>
    [ApiController]
    [Route("[Controller]")]
    public class ProjectApplicationController : Controller
    {
        private readonly IProjectApplicationService _projectApplicationService;
        private readonly IAuthenticationService _authenticationService;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="projectApplicationService"></param>
        public ProjectApplicationController(IProjectApplicationService projectApplicationService, IAuthenticationService authenticationService)
        {
            _projectApplicationService = projectApplicationService; 
            _authenticationService = authenticationService;
        }
        /// <summary>
        /// Gets the list of all submitted applications
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("submitted")]
        [SwaggerOperation("Get all submitted applications")]
        [Authorize]
        public IActionResult SubmittedApplications()
        {
            var result = _projectApplicationService.GetSubmittedApplications();
            if (result.Success) return Ok(result.Value);
            return BadRequest("Could not get submitted applications list.");
        }
    }
}
