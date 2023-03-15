using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxHosts
    {
        [JsonPropertyName("HostListing")]
        public List<LinuxHostsEntry>? HostListing { get; set; }
    }
}