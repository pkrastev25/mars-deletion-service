using System;
using System.Threading.Tasks;
using mars_deletion_svc.Exceptions;
using mars_deletion_svc.MarkingService.Models;
using mars_deletion_svc.ResourceTypes.Metadata.Interfaces;
using mars_deletion_svc.Services.Inerfaces;
using mars_deletion_svc.Utils;

namespace mars_deletion_svc.ResourceTypes.Metadata
{
    public class FileClient : IFileClient
    {
        private readonly string _baseUrl;
        private readonly IHttpService _httpService;

        public FileClient(
            IHttpService httpService
        )
        {
            var baseUrl = Environment.GetEnvironmentVariable(Constants.Constants.FileSvcUrlKey);
            _baseUrl = string.IsNullOrEmpty(baseUrl) ? "file-svc" : baseUrl;
            _httpService = httpService;
        }

        public async Task DeleteResource(
            DependantResourceModel dependantResourceModel
        )
        {
            var response = await _httpService.DeleteAsync(
                $"http://{_baseUrl}/files/{dependantResourceModel.ResourceId}"
            );

            response.ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
                new FailedToDeleteResourceException(
                    $"Failed to delete {dependantResourceModel} from file-svc!" +
                    await response.IncludeStatusCodeAndMessageFromResponse()
                )
            );
        }
    }
}