using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.ResultConfig.Interfaces;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.ResourceTypes.ResultConfig
{
    public class ResultConfigClient : IResultConfigClient
    {
        private readonly IHttpService _httpService;
        private readonly ILoggerService _loggerService;

        public ResultConfigClient(
            IHttpService httpService,
            ILoggerService loggerService
        )
        {
            _httpService = httpService;
            _loggerService = loggerService;
        }

        public async Task DeleteResource(DependantResourceModel dependantResourceModel)
        {
            var response = await _httpService.DeleteAsync(
                $"http://resultcfg-svc/api/ResultConfigs/{dependantResourceModel.ResourceId}"
            );

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from resultcfg-svc!"
                );
            }

            _loggerService.LogDeleteEvent(dependantResourceModel.ToString());
        }
    }
}