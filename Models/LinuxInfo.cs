using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxInfo
    {
        [JsonPropertyName("HostName")]
        public string? HostName { get; set; }
        [JsonPropertyName("Version")]
        public string? Version { get; set; }
        [JsonPropertyName("Domain")]
        public string? Domain { get; set; }
        [JsonPropertyName("Env")]
        public string? Env { get; set; }
        [JsonPropertyName("OsVer")]
        public string? OsVer { get; set; }
        [JsonPropertyName("OsCaption")]
        public string? OsCaption { get; set; }
        [JsonPropertyName("LastBootUpTime")]
        public string? LastBootUpTime { get; set; }
        [JsonPropertyName("LocalDateTime")]
        public string? LocalDateTime { get; set; }
        [JsonPropertyName("CurrentTimeZone")]
        public string? CurrentTimeZone { get; set; }
        [JsonPropertyName("Cpus")]
        public int Cpus { get; set; }
        [JsonPropertyName("TotalMemory")]
        public int TotalMemory { get; set; }
        [JsonPropertyName("UsedMemory")]
        public int UsedMemory { get; set; }
        [JsonPropertyName("IsHosted")]
        public bool IsHosted { get; set; }
        [JsonPropertyName("JavaInstalled")]
        public string? JavaInstalled { get; set; }
        [JsonPropertyName("LinuxDotNet")]
        public List<LinuxDotNet>? LinuxDotNet { get; set; }
        [JsonPropertyName("LinuxNetworks")]
        public List<LinuxNetworks>? LinuxNetworks { get; set; }
        [JsonPropertyName("Sudoers")]
        public List<string>? Sudoers { get; set; }
        [JsonPropertyName("Capabilities")]
        public List<string>? Capabilities { get; set; }
        [JsonPropertyName("LinuxDisks")]
        public List<LinuxDisks>? LinuxDisks { get; set; }
        [JsonPropertyName("Ciphers")]
        public List<string>? Ciphers { get; set; }

    }
}