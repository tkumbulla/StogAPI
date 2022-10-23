using Microsoft.EntityFrameworkCore;
using Stog.Application.Interfaces.Authentication;
using Stog.Application.Interfaces.Students;
using Stog.Application.Interfaces.User;
using Stog.Application.Models.Students.ProjectApplication;
using Stog.Application.Results;
using Stog.Domain.Interfaces;
using Stog.Domain.Models.Students;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Services.Students
{
    /// <summary>
    /// Service class that will handle all operations regarding project applications.
    /// </summary>
    public class ProjectApplicationService : IProjectApplicationService
    {
        private readonly IRepository<ProjectApplication> _projectApplicationRepository;
        private readonly IUserService _userService;
        private readonly IAuthenticationService _authenticationService;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="projectApplicationRepository"></param>
        public ProjectApplicationService(IRepository<ProjectApplication> projectApplicationRepository, IAuthenticationService authenticationService, IUserService userService)
        {
            _projectApplicationRepository = projectApplicationRepository;
            _authenticationService = authenticationService;
            _userService = userService;
        }
        /// <summary>
        /// Returns the list of submitted applications.
        /// </summary>
        /// <returns></returns>
        public Result<ProjectApplicationListModel> GetSubmittedApplications()
        {
            var userId = _userService.GetCurrentUserId();
            var user = _userService.GetUserById(userId);
            ProjectApplicationListModel model = new ProjectApplicationListModel();
            model.Applications = new List<ProjectApplicationDetailModel>();
            var projectApplications = _projectApplicationRepository.TableNoTracking.Include(x => x.Project).ThenInclude(x => x.FinancialInstitution).Where(x => !x.IsDeleted && user.StudentId != null && x.StudentId == user.StudentId).ToList();
            foreach (var projectApplication in projectApplications)
            {
                ProjectApplicationDetailModel detailModel = new ProjectApplicationDetailModel();
                detailModel.ApplicationDate = projectApplication.CreatedOnUtc.ToString("dd/MM/yyyy");
                detailModel.ApplicationId = projectApplication.Id;
                detailModel.Status = projectApplication.Status;
                detailModel.ProjectId = projectApplication.ProjectId;
                detailModel.ProjectTitle = projectApplication.Project.Title;
                detailModel.Budget = projectApplication.Project.Budget;
                detailModel.FinancialInstitutionId = projectApplication.Project.FinancialInstitutionId;
                detailModel.FinancialInstitutionName = projectApplication.Project.FinancialInstitution.Name;
                model.Applications.Add(detailModel);
            }
            model.TotalCount = model.Applications.Count;
            return Result.Ok(model);
        }

    }
}
