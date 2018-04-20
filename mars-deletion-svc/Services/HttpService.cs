using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using mars_deletion_svc.Services.Inerfaces;
using Newtonsoft.Json;

namespace mars_deletion_svc.Services
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _httpClient;

        public HttpService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(string requestUri, T newModel)
        {
            var httpContent = new StringContent(
                JsonConvert.SerializeObject(newModel),
                Encoding.UTF8,
                "application/json"
            );

            return await _httpClient.PostAsync(requestUri, httpContent);
        }

        public async Task<HttpResponseMessage> GetAsync(string requestUri)
        {
            return await _httpClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> DeleteAsync(string requestUri)
        {
            return await _httpClient.DeleteAsync(requestUri);
        }
    }
}