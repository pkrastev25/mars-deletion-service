using System.Collections.Generic;
using mars_deletion_svc.MarkingService.Models;
using Newtonsoft.Json;

namespace mars_deletion_svc.MarkSession.Models
{
    public class MarkSessionModel
    {
        [JsonProperty("markSessionId")]
        public string MarkSessionId { get; set; }

        [JsonProperty("projectId")]
        public string ProjectId { get; set; }

        [JsonProperty("dependantResources")]
        public List<DependantResourceModel> DependantResources { get; set; }
    }
}