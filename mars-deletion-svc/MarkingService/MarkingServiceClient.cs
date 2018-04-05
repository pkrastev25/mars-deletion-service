using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Interfaces;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.Services.Inerfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.MarkingService
{
    public class MarkingServiceClient : IMarkingServiceClient
    {
        private readonly IHttpService _httpService;

        public MarkingServiceClient(
            IHttpService httpService
        )
        {
            _httpService = httpService;
        }

        public async Task<IEnumerable<DependantResourceModel>> GetDependantResources(
            string resourceType,
            string resourceId,
            string projectId
        )
        {
            var response = await _httpService.GetAsync(
                $"http://marking-svc/api/mark/{resourceType}/{resourceId}?projectId={projectId}"
            );

            switch (response.StatusCode)
            {
                case HttpStatusCode.OK:
                    return await response.Deserialize<IEnumerable<DependantResourceModel>>();
                case HttpStatusCode.Conflict:
                    throw new ResourceConflictException(
                        $"Failed to get dependant resources for {resourceType} with id: {resourceId} and projectId: {projectId} from marking-svc, because some of the resources are locked and cannot be used!"
                    );
                default:
                    throw new FailedToGetDependantResourcesException(
                        $"Failed to get dependant resources for {resourceType} with id: {resourceId} and projectId: {projectId} from marking-svc!"
                    );
            }
        }
    }
}