using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxDotNet
    {
        [JsonPropertyName("DotNetName")]
        public string DotNetName { get; set; }
        [JsonPropertyName("DotNetVer")]
        public string DotNetVer { get; set; }

        public LinuxDotNet(string i)
        {
            //break each list entry into parts
            List<string> parts = i.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            //Define each entry of the array created above with the first and last methods
            DotNetName = parts.FirstOrDefault();
            DotNetVer = parts.LastOrDefault();
        }
    }
}
