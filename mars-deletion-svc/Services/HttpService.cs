using System.Net.Http;
using System.Threading.Tasks;
using mars_deletion_svc.Services.Inerfaces;

namespace mars_deletion_svc.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await _httpClient.DeleteAsync(requestUri);
        }
    }
}