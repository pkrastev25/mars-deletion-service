using Newtonsoft.Json;

namespace mars_deletion_svc.Middlewares.Models
{
    public class ErrorMessageModel
    {
        [JsonProperty("error")]
        public string Error { get; set; }

        public ErrorMessageModel(
            string error
        )
        {
            Error = error;
        }
    }
}