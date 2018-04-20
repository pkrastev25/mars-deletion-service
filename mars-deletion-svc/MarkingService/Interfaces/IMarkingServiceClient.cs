using System.Threading.Tasks;
using mars_deletion_svc.MarkSession.Models;

namespace mars_deletion_svc.MarkingService.Interfaces
{
    public interface IMarkingServiceClient
    {
        Task<MarkSessionModel> CreateMarkSession(
            string resourceType,
            string resourceId,
            string projectId
        );

        Task<MarkSessionModel> GetMarkSessionById(
            string markSessionId
        );

        Task DeleteMarkingSession(
            string markSessionId
        );
    }
}