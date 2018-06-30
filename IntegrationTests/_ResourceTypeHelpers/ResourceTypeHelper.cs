using System;
using System.Net.Http;
using System.Threading.Tasks;
using IntegrationTests._ResourceTypeHelpers.Models;
using mars_deletion_svc.Constants;
using mars_deletion_svc.Services;
using mars_deletion_svc.Utils;

namespace IntegrationTests._ResourceTypeHelpers
{
    public static class ResourceTypeHelper
    {
        public static async Task<MetadataModel> RetreiveMetadata(
            string metadataId
        )
        {
            var metadataBaseUrlEnvironmental = Environment.GetEnvironmentVariable(Constants.MetadataSvcUrlKey);
            var metadataBaseUrl = string.IsNullOrEmpty(metadataBaseUrlEnvironmental)
                ? "metadata-svc"
                : metadataBaseUrlEnvironmental;

            var httpService = new HttpService(new HttpClient());
            var response = await httpService.GetAsync(
                $"http://{metadataBaseUrl}/metadata/{metadataId}"
            );

            return await response.Deserialize<MetadataModel>();
        }
    }
}