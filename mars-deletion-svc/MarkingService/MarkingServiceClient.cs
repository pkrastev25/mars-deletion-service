using System.Collections.Generic;
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
        public const string MarkSessionTypeToBeDeleted = "TO_BE_DELETED";

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
            string projectId,
            string markSessionType
        )
        {
            var response = await _httpService.PostAsync(
                $"http://marking-svc/api/markSession/{resourceType}/{resourceId}?markSessionType={markSessionType}&projectId={projectId}",
                ""
            );

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.Deserialize<MarkSessionModel>();
                case HttpStatusCode.Conflict:
                    throw new ResourceConflictException(
                        $"Failed to create mark session, type: {markSessionType} for {resourceType} with id: {resourceId} and projectId: {projectId} from marking-svc!" +
                        await response.IncludeStatusCodeAndMessageFromResponse()
                    );
                default:
                    throw new FailedToCreateMarkSessionException(
                        $"Failed to create mark session, type: {markSessionType} for {resourceType} with id: {resourceId} and projectId: {projectId} from marking-svc!" +
                        await response.IncludeStatusCodeAndMessageFromResponse()
                    );
            }
        }

        public async Task<MarkSessionModel> GetMarkSessionById(
            string markSessionId
        )
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
                        $"Failed to get mark session with id: {markSessionId} from marking-svc!" +
                        response.IncludeStatusCodeAndMessageFromResponse()
                    );
            }
        }

        public async Task<IEnumerable<MarkSessionModel>> GetMarkSessionsByMarkSessionType(
            string markSessionType
        )
        {
            var response = await _httpService.GetAsync(
                $"http://marking-svc/api/markSession?markSessionType={markSessionType}"
            );

            response.ThrowExceptionIfNotSuccessfulResponse(
                new FailedToGetMarkSessionException(
                    $"Failed to get mark sessions for type: {markSessionType} from marking-svc!" +
                    await response.IncludeStatusCodeAndMessageFromResponse()
                )
            );

            if (response.IsEmptyResponse())
            {
                return new List<MarkSessionModel>();
            }

            return await response.Deserialize<List<MarkSessionModel>>();
        }

        public async Task DeleteEmptyMarkingSession(
            string markSessionId
        )
        {
            var response = await _httpService.DeleteAsync(
                $"http://marking-svc/api/markSession/{markSessionId}/emptySession"
            );

            response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                new FailedToDeleteMarkSessionException(
                    $"Failed to delete mark session with id: {markSessionId} from marking-svc!" +
                    await response.IncludeStatusCodeAndMessageFromResponse()
                )
            );
        }
    }
}