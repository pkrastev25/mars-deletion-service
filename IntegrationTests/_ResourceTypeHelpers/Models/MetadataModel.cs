using Newtonsoft.Json;

namespace IntegrationTests._ResourceTypeHelpers.Models
{
    public class MetadataModel
    {
        public const string ToBeDeletedState = "TO_BE_DELETED";

        [JsonProperty("dataId")]
        public string DataId { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }
    }
}