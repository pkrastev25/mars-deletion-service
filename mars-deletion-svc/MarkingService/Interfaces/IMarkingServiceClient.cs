using System.Collections.Generic;
using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;

namespace mars_deletion_svc.MarkingService.Interfaces
{
    public interface IMarkingServiceClient
    {
        Task<IEnumerable<DependantResourceModel>> CreateMarkSession(
            string resourceType,
            string resourceId,
            string projectId
        );

        Task DeleteMarkingSession(
            string resourceId
        );
    }
}