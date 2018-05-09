using System.Collections.Generic;
using System.Threading.Tasks;
using mars_deletion_svc.MarkSession.Models;

namespace mars_deletion_svc.MarkingService.Interfaces
{
    public interface IMarkingServiceClient
    {
        Task<MarkSessionModel> CreateMarkSession(
            string resourceType,
            string resourceId,
            string projectId,
            string markSessionType
        );

        Task<MarkSessionModel> GetMarkSessionById(
            string markSessionId
        );

        Task<IEnumerable<MarkSessionModel>> GetMarkSessionsByMarkSessionType(
            string markSessionType
        );

        Task DeleteEmptyMarkingSession(
            string markSessionId
        );
    }
}