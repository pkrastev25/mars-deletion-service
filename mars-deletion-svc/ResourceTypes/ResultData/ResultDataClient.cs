using System.Threading.Tasks;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.ResultData.Interfaces;
using mars_deletion_svc.Services.Inerfaces;

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

        public async Task DeleteResource(DependantResourceModel dependantResourceModel)
        {
            await Task.Run(() =>
            {
                // TODO
            });
        }
    }
}