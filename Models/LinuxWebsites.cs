using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxWebsites
    {
        [JsonPropertyName("WLSSites")]
        public string? WLSSites { get; set; }
        [JsonPropertyName("WLSLogs")]
        public List<string>? WLSLogs { get; set; }
        [JsonPropertyName("WLSSecurity")]
        public List<string>? WLSSecurity { get; set; }
        [JsonPropertyName("ApacheSites")]
        public string? ApacheSites { get; set; }
        [JsonPropertyName("ApacheLogs")]
        public List<string>? ApacheLogs { get; set; }
        [JsonPropertyName("ApacheSecurity")]
        public List<string>? ApacheSecurity { get; set; }
        [JsonPropertyName("TomcatSites")]
        public string? TomcatSites { get; set; }
        [JsonPropertyName("TomcatLogs")]
        public List<string>? TomcatLogs { get; set; }
        [JsonPropertyName("TomcatSecurity")]
        public List<string>? TomcatSecurity { get; set; }
    }
}