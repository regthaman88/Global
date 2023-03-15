using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxCerts
    {
        [JsonPropertyName("GeneralCertsList")]
        public List<LinuxCertsEntry>? GeneralCertsList { get; set; }
    }
}