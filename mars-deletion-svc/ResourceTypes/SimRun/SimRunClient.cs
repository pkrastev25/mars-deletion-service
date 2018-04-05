using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.SimRun.Interfaces;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.ResourceTypes.SimRun
{
    public class SimRunClient : ISimRunClient
    {
        private readonly IHttpService _httpService;
        private readonly ILoggerService _loggerService;

        public SimRunClient(
            IHttpService httpService,
            ILoggerService loggerService
        )
        {
            _httpService = httpService;
            _loggerService = loggerService;
        }

        public async Task DeleteResource(DependantResourceModel dependantResourceModel)
        {
            // TODO: It seems that the sim-runner-svc is not able to delete a simRun!!!
            var response = await _httpService.DeleteAsync(
                $"http://sim-runner-svc/simrun?simRunId={dependantResourceModel.ResourceId}"
            );

            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                _loggerService.LogSkipEvent(dependantResourceModel.ToString());
                return;
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from sim-runner-svc!"
                );
            }

            _loggerService.LogDeleteEvent(dependantResourceModel.ToString());
        }
    }
}