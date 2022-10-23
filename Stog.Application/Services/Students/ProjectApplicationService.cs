using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Stog.Application.Interfaces.Authentication;
using Stog.Application.Interfaces.Students;
using Stog.Application.Interfaces.User;
using Stog.Application.Models.Students.ProjectApplication;
using Stog.Application.Results;
using Stog.Domain.Interfaces;
using Stog.Domain.Models.Institutions;
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
        private readonly IWebHostEnvironment _hostingEnvironment;
        private readonly IRepository<Stog.Domain.Models.Common.File> _fileRepository;
        private readonly IRepository<Project> _projectRepository;
        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="projectApplicationRepository"></param>
        public ProjectApplicationService(IRepository<ProjectApplication> projectApplicationRepository, IAuthenticationService authenticationService, IUserService userService, IWebHostEnvironment hostingEnvironment, IRepository<Stog.Domain.Models.Common.File> fileRepository, IRepository<Project> projectRepository)
        {
            _projectApplicationRepository = projectApplicationRepository;
            _authenticationService = authenticationService;
            _userService = userService;
            _hostingEnvironment = hostingEnvironment;
            _fileRepository = fileRepository;
            _projectRepository = projectRepository;
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
        /// <summary>
        /// Method needed to submit an application for a project
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public Result Apply(ApplyPostRequest request)
        {
            var userId = _userService.GetCurrentUserId();
            var user = _userService.GetUserById(userId);
            Guid fileId = Guid.Parse("1C478022-263B-4C16-A5A4-C5699A1F8FA3");
            if(user.StudentId != null)
            {
                if(request.File != null)
                {
                    if (string.IsNullOrWhiteSpace(_hostingEnvironment.WebRootPath))
                    {
                        _hostingEnvironment.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "Content");
                    }
                    var uploads = Path.Combine(_hostingEnvironment.WebRootPath);
                    if (!Directory.Exists(uploads))
                    {
                        Directory.CreateDirectory(uploads);
                    }
                    string fileName = request.File.FileName;
                    string filePath = Path.Combine(uploads, fileName);
                    byte[] fileBytes = new byte[] { };
                    using (var ms = new MemoryStream())
                    {
                        request.File.CopyTo(ms);
                        fileBytes = ms.ToArray();
                    }
                    System.IO.File.WriteAllBytes(filePath, fileBytes);
                    Stog.Domain.Models.Common.File file = new Stog.Domain.Models.Common.File()
                    {
                        Description = request.File.Name,
                        IsDeleted = false,
                        ContentType = request.File.ContentType,
                        CreatedById = userId,
                        CreatedOnUtc = DateTime.UtcNow,
                        Name = request.File.FileName,
                        IsActive = true,
                        PhysicalPath = filePath,
                        VirtualPath = filePath
                    };
                    _fileRepository.Insert(file);
                    fileId = file.Id;
                }
                ProjectApplication application = new ProjectApplication()
                {
                    AdditionalNotes = request.AdditionalNotes ?? "",
                    CreatedById = userId,
                    CreatedOnUtc = DateTime.UtcNow,
                    IsActive = true,
                    ProjectId = request.ProjectId,
                    ProposalFileId = fileId,
                    Status = Domain.Models.Enumerations.Status.Pending,
                    StudentId = user.StudentId.GetValueOrDefault(),
                };
                _projectApplicationRepository.Insert(application);
                return Result.Ok();
            }
            return Result.Fail("You cannot apply for this project.");
        }
        /// <summary>
        /// Get all projects where user has not applied yet.
        /// </summary>
        /// <returns></returns>
        public Result<List<ProjectModel>> GetProjects()
        {
            var userId = _userService.GetCurrentUserId();
            var user = _userService.GetUserById(userId);
            var projectsWhereUserHasApplied = _projectApplicationRepository.TableNoTracking.Where(x => x.StudentId == user.StudentId).Select(x => x.ProjectId).ToList();
            var projects = _projectRepository.TableNoTracking.Include(x => x.FinancialInstitution).Include(x => x.RequirementFile).Where(x => !projectsWhereUserHasApplied.Contains(x.Id)).ToList();
            var allProjectsList =  projects.Select(x => new ProjectModel()
            {
                EndDate = x.EndDate.ToString("dd/MM/yyyy"),
                ProjectDescription = x.Description,
                StartDate = x.StartDate.ToString("dd/MM/yyyy"),
                Budget = x.Budget,
                FinancialInstitutionId = x.FinancialInstitutionId,
                FinancialInstitutionName = x.FinancialInstitution.Name,
                Id = x.Id,
                ProjectTitle = x.Title,
                RequirementFileContentType = x.RequirementFile.ContentType,
                RequirementFileId = x.RequirementFileId,
                RequirementFileName = x.RequirementFile.Name,
                RequirementFileVirtualPath = x.RequirementFile.VirtualPath
            }).ToList();
            return Result.Ok(allProjectsList);
        }
    }
}
