using System.Threading.Tasks;
using mars_deletion_svc.BackgroundJobs.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.Controllers
{
    [Route("api/[controller]")]
    public class BackgroundJobController : Controller
    {
        private readonly IBackgroundJobsHandler _backgroundJobsHandler;

        public BackgroundJobController(
            IBackgroundJobsHandler backgroundJobsHandler
        )
        {
            _backgroundJobsHandler = backgroundJobsHandler;
        }

        [HttpGet("{backgroundJobId}/status")]
        public async Task<IActionResult> GetStatusForBackgroundJob(
            string backgroundJobId
        )
        {
            if (string.IsNullOrEmpty(backgroundJobId))
            {
                return BadRequest("backgroundJobId is not specified!");
            }

            var backgroundJobState = await _backgroundJobsHandler.GetJobStatusForBackgroundJobId(backgroundJobId);

            return Ok(
                backgroundJobState
            );
        }
    }
}