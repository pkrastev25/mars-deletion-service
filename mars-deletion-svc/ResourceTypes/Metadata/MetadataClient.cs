using System.Net;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Metadata.Interfaces;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.ResourceTypes.Metadata
{
    public class MetadataClient : IMetadataClient
    {
        private readonly IHttpService _httpService;
        private readonly ILoggerService _loggerService;

        public MetadataClient(
            IHttpService httpService,
            ILoggerService loggerService
        )
        {
            _httpService = httpService;
            _loggerService = loggerService;
        }

        public async Task DeleteResource(DependantResourceModel dependantResourceModel)
        {
            var response =
                await _httpService.DeleteAsync($"http://file-svc/files/{dependantResourceModel.ResourceId}");

            if (response.StatusCode != HttpStatusCode.OK && response.StatusCode != HttpStatusCode.NoContent)
            {
                throw new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from metadata-svc!"
                );
            }

            _loggerService.LogDeleteEvent(dependantResourceModel.ToString());
        }
    }
}