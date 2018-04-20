using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mars_deletion_svc.Utils
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TModel> Deserialize<TModel>(this HttpResponseMessage httpResponseMessage)
        {
            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TModel>(jsonResponse);
        }

        public static void ThrowExceptionIfNotSuccessfulResponseOrNot404Response(
            this HttpResponseMessage httpResponseMessage,
            Exception exception
        )
        {
            if (httpResponseMessage == null)
            {
                throw new ArgumentNullException(nameof(httpResponseMessage));
            }

            if (!httpResponseMessage.IsSuccessStatusCode && httpResponseMessage.StatusCode != HttpStatusCode.NotFound)
            {
                throw exception;
            }
        }
    }
}