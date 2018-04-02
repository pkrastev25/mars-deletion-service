using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.Controllers
{
    [Route("api/[controller]")]
    public class DeleteController : Controller
    {
        [HttpDelete("{resourceType}/{resourceId}")]
        public async Task<IActionResult> DeleteResource(
            string resourceType,
            string resourceId,
            [FromQuery(Name = "projectId")] string projectId
        )
        {
            if (string.IsNullOrEmpty(resourceType))
            {
                return BadRequest("resourceType is not specified!");
            }

            if (string.IsNullOrEmpty(resourceId))
            {
                return BadRequest("resourceId is not specified!");
            }

            if (string.IsNullOrEmpty(projectId))
            {
                return BadRequest("projectId is not specified!");
            }

            switch (resourceType)
            {
                case "project":
                case "metadata":
                case "scenario":
                case "resultConfig":
                case "simPlan":
                case "simRun":
                    return BadRequest("Not implemented yet!");
                case "resultData":
                    return BadRequest("Not implemented yet!");
                default:
                    return BadRequest("resourceType is unknown!");
            }
        }
    }
}