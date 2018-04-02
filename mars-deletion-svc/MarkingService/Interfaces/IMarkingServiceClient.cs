using System.Collections.Generic;
using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.MarkingService.Interfaces
{
    public interface IMarkingServiceClient
    {
        Task<IEnumerable<DependantResourceModel>> GetDependantResources(
            string resourceType,
            string resourceId,
            string projectId
        );
    }
}