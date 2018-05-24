using System;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.ResultData.Interfaces;
using mars_deletion_svc.Services.Inerfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.ResourceTypes.ResultData
{
    public class ResultDataClient : IResultDataClient
    {
        private readonly string _baseUrl;
        private readonly IHttpService _httpService;

        public ResultDataClient(
            IHttpService httpService
        )
        {
            var baseUrl = Environment.GetEnvironmentVariable(Constants.Constants.DatabaseUtilitySvcUrlKey);
            _baseUrl = string.IsNullOrEmpty(baseUrl) ? "database-utility-svc:8090" : baseUrl;
            _httpService = httpService;
        }

        public async Task DeleteResource(
            DependantResourceModel dependantResourceModel
        )
        {
            var response = await _httpService.DeleteAsync(
                $"http://{_baseUrl}/delete/mongodb-result/{dependantResourceModel.ResourceId}"
            );

            response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from database-util-svc!" +
                    await response.IncludeStatusCodeAndMessageFromResponse()
                )
            );
        }
    }
}