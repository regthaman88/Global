using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxDisks
    {
        [JsonPropertyName("DiskName")]
        public string DiskName { get; set; }
        [JsonPropertyName("TotalSpace")]
        public string TotalSpace { get; set; }
        [JsonPropertyName("FreeSpace")]
        public string FreeSpace { get; set; }

        public LinuxDisks(string i)
        {
            //break each list entry into parts
            List<string> parts = i.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();

            //Define each entry of the array created above with the elementat method
            DiskName = parts.ElementAtOrDefault(0);
            TotalSpace = parts.ElementAtOrDefault(1);
            FreeSpace = parts.ElementAtOrDefault(2);

        }

    }
}
