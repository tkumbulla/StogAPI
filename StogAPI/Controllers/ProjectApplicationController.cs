using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Stog.Application.Interfaces.Authentication;
using Stog.Application.Interfaces.Students;
using Stog.Application.Models.Students.ProjectApplication;
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
        [HttpPost]
        [Route("apply")]
        [SwaggerOperation("Submit an application for a project")]
        [Authorize]
        public IActionResult SubmitApplication([FromForm] ApplyPostRequest request)
        {
            var result = _projectApplicationService.Apply(request);
            if (result.Success) return Ok();
            return BadRequest("Could not submit.");
        }
        [HttpGet]
        [Route("projects")]
        [SwaggerOperation("List of projects where user has not applied yet")]
        [Authorize]
        public IActionResult GetProjects()
        {
            var result = _projectApplicationService.GetProjects();
            if (result.Success) return Ok(result.Value);
            return BadRequest("Could not get projects list.");
        }
    }
}
