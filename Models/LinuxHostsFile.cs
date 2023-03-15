using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxHostsFile
    {
        [JsonPropertyName("IP")]
        public string IP { get; set; }
        [JsonPropertyName("HostName")]
        public string HostName { get; set; }
        [JsonPropertyName("Comment")]
        public string Comment { get; set; }

        public LinuxHostsFile(string i)
        {
            //break each list entry into parts
            List<string> hostitem = i.Split(new char[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            //skip any list items with localhost addresses
            if (hostitem.Contains<string>("localhost"))
                { }
            else
            {
                //Define each entry of the array created above with the first and last methods
                IP = hostitem.ElementAtOrDefault(0);
                HostName = hostitem.ElementAtOrDefault(1);
                Comment = hostitem.ElementAtOrDefault(2);

            }

        }
    }
}
