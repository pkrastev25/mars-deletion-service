using Newtonsoft.Json;

namespace mars_deletion_svc.MarkingService.Models
{
    public class DependantResourceModel
    {
        [JsonProperty("resourceType")]
        public string ResourceType { get; set; }

        [JsonProperty("resourceId")]
        public string ResourceId { get; set; }

        public override string ToString()
        {
            return $"{ResourceType} with id: {ResourceId}";
        }
    }
}