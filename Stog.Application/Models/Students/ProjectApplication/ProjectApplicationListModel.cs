using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.Students.ProjectApplication
{
    public class ProjectApplicationListModel
    {
        public List<ProjectApplicationDetailModel> Applications { get; set; } = new List<ProjectApplicationDetailModel>();
        public int TotalCount { get; set; }
    }
}
