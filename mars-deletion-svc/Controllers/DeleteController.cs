using System.Threading.Tasks;
using mars_deletion_svc.DependantResource.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.Controllers
{
    [Route("api/[controller]")]
    public class DeleteController : Controller
    {
        private readonly IDependantResourcesHandler _dependantResourcesHandler;

        public DeleteController(
            IDependantResourcesHandler dependantResourcesHandler
        )
        {
            _dependantResourcesHandler = dependantResourcesHandler;
        }

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
                    return await _dependantResourcesHandler.DeleteDependantResources(
                        resourceType,
                        resourceId,
                        projectId
                    );
                case "resultData":
                    return BadRequest("Not implemented yet!");
                default:
                    return BadRequest("resourceType is unknown!");
            }
        }
    }
}