using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace mars_deletion_svc.DependantResource.Interfaces
{
    public interface IDependantResourcesHandler
    {
        Task<IActionResult> DeleteDependantResources(
            string resourceType,
            string resourceId,
            string projectId
        );
    }
}