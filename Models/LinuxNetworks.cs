using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxNetworks
    {
        [JsonPropertyName("IPAddress")]
        public string IPAddress { get; set; }
        [JsonPropertyName("SubnetMask")]
        public string SubnetMask { get; set; }
        [JsonPropertyName("Gateway")]
        public string Gateway { get; set; }

        public LinuxNetworks(string i)
        {
            List<string> parts = i.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            
            IPAddress = parts.ElementAtOrDefault(0);
            SubnetMask = parts.ElementAtOrDefault(1);
            Gateway = parts.ElementAtOrDefault(2);
        }

    }
}
