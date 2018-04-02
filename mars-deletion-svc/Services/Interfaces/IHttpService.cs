using System.Net.Http;
using System.Threading.Tasks;

namespace mars_deletion_svc.Services.Inerfaces
{
    public interface IHttpService
    {
        Task<HttpResponseMessage> DeleteAsync(string requestUri);
    }
}