using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.SimPlan.Interfaces;
using mars_deletion_svc.Services.Inerfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.ResourceTypes.SimPlan
{
    public class SimPlanClient : ISimPlanClient
    {
        private readonly IHttpService _httpService;
        private readonly ILoggerService _loggerService;

        public SimPlanClient(
            IHttpService httpService,
            ILoggerService loggerService
        )
        {
            _httpService = httpService;
            _loggerService = loggerService;
        }

        public async Task DeleteResource(
            DependantResourceModel dependantResourceModel,
            string projectId
        )
        {
            if (await DoesSimPlanExist(dependantResourceModel.ResourceId, projectId))
            {
                var response = await _httpService.DeleteAsync(
                    $"http://sim-runner-svc/simplan?simPlanId={dependantResourceModel.ResourceId}"
                );

                response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                    new FailedToDeleteResourceException(
                        $"Failed to delete {dependantResourceModel} from sim-runner-svc! The response status code is {response.StatusCode}"
                    )
                );
            }

            _loggerService.LogDeleteEvent(dependantResourceModel.ToString());
        }

        private async Task<bool> DoesSimPlanExist(
            string simPlanId,
            string projectId
        )
        {
            var response = await _httpService.GetAsync(
                $"http://sim-runner-svc/simplan?id={simPlanId}&projectid={projectId}"
            );

            if (response.IsSuccessStatusCode)
            {
                return response.StatusCode != HttpStatusCode.NoContent;
            }

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return false;
            }

            throw new FailedToGetResourceException(
                $"Failed to get simPlan with id: {simPlanId}, projectId: {projectId} from sim-runner-svc!"
            );
        }
    }
}