using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mars_deletion_svc.Utils
{
    public static class ExtensionUtils
    {
        public static async Task<TModel> Deserialize<TModel>(this HttpResponseMessage httpResponseMessage)
        {
            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TModel>(jsonResponse);
        }
    }
}