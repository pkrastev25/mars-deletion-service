using System;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.ResultConfig.Interfaces;
using mars_deletion_svc.Services.Inerfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.ResourceTypes.ResultConfig
{
    public class ResultConfigClient : IResultConfigClient
    {
        private readonly string _baseUrl;
        private readonly IHttpService _httpService;

        public ResultConfigClient(
            IHttpService httpService
        )
        {
            var baseUrl = Environment.GetEnvironmentVariable(Constants.Constants.ResultConfigSvcUrlKey);
            _baseUrl = string.IsNullOrEmpty(baseUrl) ? "resultcfg-svc" : baseUrl;
            _httpService = httpService;
        }

        public async Task DeleteResource(
            DependantResourceModel dependantResourceModel
        )
        {
            var response = await _httpService.DeleteAsync(
                $"http://{_baseUrl}/api/ResultConfigs/{dependantResourceModel.ResourceId}"
            );

            response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from resultcfg-svc!" +
                    await response.IncludeStatusCodeAndMessageFromResponse()
                )
            );
        }
    }
}