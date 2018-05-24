using System;
using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.SimRun.Interfaces;
using mars_deletion_svc.Services.Inerfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.ResourceTypes.SimRun
{
    public class SimRunClient : ISimRunClient
    {
        private readonly string _baseUrl;
        private readonly IHttpService _httpService;

        public SimRunClient(
            IHttpService httpService
        )
        {
            var baseUrl = Environment.GetEnvironmentVariable(Constants.Constants.FileSvcUrlKey);
            _baseUrl = string.IsNullOrEmpty(baseUrl) ? "sim-runner-svc" : baseUrl;
            _httpService = httpService;
        }

        public async Task DeleteResource(
            DependantResourceModel dependantResourceModel,
            string projectId
        )
        {
            if (await DoesSimRunExist(dependantResourceModel.ResourceId, projectId))
            {
                var response = await _httpService.DeleteAsync(
                    $"http://{_baseUrl}/simrun?simRunId={dependantResourceModel.ResourceId}"
                );

                response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                    new FailedToDeleteResourceException(
                        $"Failed to delete {dependantResourceModel} from sim-runner-svc!" +
                        await response.IncludeStatusCodeAndMessageFromResponse()
                    )
                );
            }
        }

        private async Task<bool> DoesSimRunExist(
            string simRunId,
            string projectId
        )
        {
            var response = await _httpService.GetAsync(
                $"http://{_baseUrl}/simrun?simRunId={simRunId}&projectid={projectId}"
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
                $"Failed to get simRun with id: {simRunId}, projectId: {projectId} from sim-runner-svc!" +
                await response.IncludeStatusCodeAndMessageFromResponse()
            );
        }
    }
}