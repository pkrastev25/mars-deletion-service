using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkSession.Models;
using mars_deletion_svc.Services.Inerfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.MarkingService
{
    public class MarkingServiceClient : IMarkingServiceClient
    {
        private const string MarkSessionTypeToBeDeleted = "TO_BE_DELETED";

        private readonly IHttpService _httpService;

        public MarkingServiceClient(
            IHttpService httpService
        )
        {
            _httpService = httpService;
        }

        public async Task<MarkSessionModel> CreateMarkSession(
            string resourceType,
            string resourceId,
            string projectId
        )
        {
            var response = await _httpService.PostAsync(
                $"http://marking-svc/api/markSession/{resourceType}/{resourceId}?markSessionType={MarkSessionTypeToBeDeleted}&projectId={projectId}",
                ""
            );

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.Deserialize<MarkSessionModel>();
                case HttpStatusCode.Conflict:
                    throw new ResourceConflictException(
                        $"Failed to create mark session for {resourceType} with id: {resourceId} and projectId: {projectId} from marking-svc, because it already exists or some of the resources are locked and cannot be used!"
                    );
                default:
                    throw new FailedToCreateMarkSessionException(
                        $"Failed to create mark session for {resourceType} with id: {resourceId} and projectId: {projectId} from marking-svc! The response status code is {response.StatusCode}"
                    );
            }
        }

        public async Task<MarkSessionModel> GetMarkSessionById(string markSessionId)
        {
            var response = await _httpService.GetAsync(
                $"http://marking-svc/api/markSession/{markSessionId}"
            );

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.Deserialize<MarkSessionModel>();
                case HttpStatusCode.NotFound:
                    throw new MarkSessionDoesNotExistException(
                        $"Mark session with id: {markSessionId} does not exist!"
                    );
                default:
                    throw new FailedToGetMarkSessionException(
                        $"Failed to get mark session with id: {markSessionId} from marking-svc! The response status code is {response.StatusCode}"
                    );
            }
        }

        public async Task DeleteMarkingSession(string markSessionId)
        {
            var response = await _httpService.DeleteAsync(
                $"http://marking-svc/api/markSession/{markSessionId}"
            );

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    break;
                default:
                    throw new FailedToDeleteMarkSessionException(
                        $"Failed to delete mark session with id: {markSessionId} from marking-svc! The response status code is {response.StatusCode}"
                    );
            }
        }
    }
}