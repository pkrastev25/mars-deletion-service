using System;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace mars_deletion_svc.Utils
{
    public static class HttpResponseMessageExtensions
    {
        public static async Task<TModel> Deserialize<TModel>(
            this HttpResponseMessage httpResponseMessage
        )
        {
            var jsonResponse = await httpResponseMessage.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<TModel>(jsonResponse);
        }

        public static void ThrowExceptionIfNotSuccessfulResponse(
            this HttpResponseMessage httpResponseMessage,
            Exception exception
        )
        {
            if (httpResponseMessage == null)
            {
                throw new ArgumentNullException(nameof(httpResponseMessage));
            }

            if (!httpResponseMessage.IsSuccessStatusCode)
            {
                throw exception;
            }
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

        public static bool IsEmptyResponse(
            this HttpResponseMessage httpResponseMessage
        )
        {
            switch (httpResponseMessage.StatusCode)
            {
                case HttpStatusCode.NoContent:
                case HttpStatusCode.NotFound:
                    return true;
                default:
                    return false;
            }
        }

        public static async Task<string> IncludeStatusCodeAndMessageFromResponse(
            this HttpResponseMessage httpResponseMessage
        )
        {
            var responseMessage = await httpResponseMessage.Content.ReadAsStringAsync();
            responseMessage = string.IsNullOrEmpty(responseMessage) ? "None" : responseMessage;
            
            return
                $" \n [REASON] response status code: {httpResponseMessage.StatusCode}, response message: {responseMessage}";
        }
    }
}