using System.Threading.Tasks;
using mars_deletion_svc.Controllers.Interfaces;
using mars_deletion_svc.ResourceTypes.Enums;
using mars_deletion_svc.Utils;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.Controllers
{
    [Route("api/[controller]")]
    public class DeleteController : Controller
    {
        private readonly IDeleteControllerHandler _deleteControllerHandler;

        public DeleteController(
            IDeleteControllerHandler deleteControllerHandler
        )
        {
            _deleteControllerHandler = deleteControllerHandler;
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
                if (resourceType == ResourceTypeEnum.Project)
                {
                    projectId = resourceId;
                }
                else
                {
                    return BadRequest("projectId is not specified!");
                }
            }

            if (!EnumUtil.DoesResourceTypeExist(resourceType))
            {
                return BadRequest("resourceType is not specified or is invalid!");
            }

            await _deleteControllerHandler.CreateMarkSessionAndDeleteDependantResurces(
                resourceType,
                resourceId,
                projectId
            );

            return Ok();
        }
    }
}