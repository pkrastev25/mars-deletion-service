using System;
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
        private readonly string _baseUrl;
        private readonly IHttpService _httpService;

        public SimPlanClient(
            IHttpService httpService
        )
        {
            var baseUrl = Environment.GetEnvironmentVariable(Constants.Constants.SimRunnerSvcUrlKey);
            _baseUrl = string.IsNullOrEmpty(baseUrl) ? "sim-runner-svc" : baseUrl;
            _httpService = httpService;
        }

        public async Task DeleteResource(
            DependantResourceModel dependantResourceModel,
            string projectId
        )
        {
            if (await DoesSimPlanExist(dependantResourceModel.ResourceId, projectId))
            {
                var response = await _httpService.DeleteAsync(
                    $"http://{_baseUrl}/simplan?simPlanId={dependantResourceModel.ResourceId}"
                );

                response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                    new FailedToDeleteResourceException(
                        $"Failed to delete {dependantResourceModel} from sim-runner-svc!" +
                        await response.IncludeStatusCodeAndMessageFromResponse()
                    )
                );
            }
        }

        private async Task<bool> DoesSimPlanExist(
            string simPlanId,
            string projectId
        )
        {
            var response = await _httpService.GetAsync(
                $"http://{_baseUrl}/simplan?id={simPlanId}&projectid={projectId}"
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
                $"Failed to get simPlan with id: {simPlanId}, projectId: {projectId} from sim-runner-svc!" +
                await response.IncludeStatusCodeAndMessageFromResponse()
            );
        }
    }
}