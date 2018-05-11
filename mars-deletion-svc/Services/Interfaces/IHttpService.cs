using System.Net.Http;
using System.Threading.Tasks;

namespace mars_deletion_svc.Services.Inerfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> PostAsync<T>(
            string requestUri,
            T newModel
        );

        Task<HttpResponseMessage> GetAsync(
            string requestUri
        );

        Task<HttpResponseMessage> PutAsync<T>(
            string requestUri,
            T updatedModel
        );

        Task<HttpResponseMessage> DeleteAsync(
            string requestUri
        );
    }
}