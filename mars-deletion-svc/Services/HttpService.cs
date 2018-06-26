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

        public HttpService(
            HttpClient httpClient
        )
        {
            _httpClient = httpClient;
        }

        public async Task<HttpResponseMessage> PostAsync<T>(
            string requestUri,
            T newModel
        )
        {
            return await _httpClient.PostAsync(requestUri, CreateStringContent(newModel));
        }

        public async Task<HttpResponseMessage> GetAsync(
            string requestUri
        )
        {
            return await _httpClient.GetAsync(requestUri);
        }

        public async Task<HttpResponseMessage> PutAsync<T>(
            string requestUri,
            T updatedModel
        )
        {
            return await _httpClient.PutAsync(requestUri, CreateStringContent(updatedModel));
        }

        public async Task<HttpResponseMessage> DeleteAsync(
            string requestUri
        )
        {
            return await _httpClient.DeleteAsync(requestUri);
        }

        private StringContent CreateStringContent<T>(
            T updatedModel
        )
        {
            return new StringContent(
                JsonConvert.SerializeObject(updatedModel),
                Encoding.UTF8,
                "application/json"
            );
        }
    }
}