using Stog.Application.Models.Students.ProjectApplication;
using Stog.Application.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Interfaces.Students
{
    /// <summary>
    /// Interface that defines all the operations that will be performed on project applications
    /// </summary>
    public interface IProjectApplicationService
    {
        /// <summary>
        /// Gets the list of submitted applications
        /// </summary>
        /// <returns></returns>
        Result<ProjectApplicationListModel> GetSubmittedApplications();
    }
}
