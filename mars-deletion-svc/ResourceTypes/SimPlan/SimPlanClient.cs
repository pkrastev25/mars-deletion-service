using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.SimPlan.Interfaces;
using mars_deletion_svc.Services.Inerfaces;

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

        public async Task DeleteResource(DependantResourceModel dependantResourceModel)
        {
            var response = await _httpService.DeleteAsync(
                $"http://sim-runner-svc/simplan?simPlanId={dependantResourceModel.ResourceId}"
            );

            if (response.StatusCode != HttpStatusCode.Accepted)
            {
                throw new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from sim-runner-svc!"
                );
            }

            _loggerService.LogDeleteEvent(dependantResourceModel.ToString());
        }
    }
}