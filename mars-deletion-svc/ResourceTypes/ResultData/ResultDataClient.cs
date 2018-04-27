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
        private readonly IHttpService _httpService;
        private readonly ILoggerService _loggerService;

        public ResultDataClient(
            IHttpService httpService,
            ILoggerService loggerService
        )
        {
            _httpService = httpService;
            _loggerService = loggerService;
        }

        public async Task DeleteResource(
            DependantResourceModel dependantResourceModel
        )
        {
            var response = await _httpService.GetAsync(
                $"http://database-utility-svc/delete/mongodb-result/{dependantResourceModel.ResourceId}"
            );

            response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from database-util-svc! The response status code is {response.StatusCode}"
                )
            );

            _loggerService.LogDeleteEvent(dependantResourceModel.ToString());
        }
    }
}