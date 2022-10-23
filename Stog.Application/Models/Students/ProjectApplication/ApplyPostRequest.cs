using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stog.Application.Models.Students.ProjectApplication
{
    /// <summary>
    /// Model needed to perform a post request for application
    /// </summary>
    public class ApplyPostRequest
    {
        /// <summary>
        /// The identifier of the project
        /// </summary>
        public Guid ProjectId { get; set; }
        /// <summary>
        /// Additional notes for application
        /// </summary>
        public string? AdditionalNotes { get; set; }
        /// <summary>
        /// Submitted file
        /// </summary>
        public IFormFile? File { get; set; }
    }
}
