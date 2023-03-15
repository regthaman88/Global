using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;

namespace HumWebAPI3.Models
{
    public class LinuxCertsEntry
    {
        [JsonPropertyName("Subject")]
        public string? Subject { get; set; }
        [JsonPropertyName("SAN")]
        public SubjectAlternativeNameBuilder SAN { get; set; }
        [JsonPropertyName("Issuer")]
        public string? Issuer { get; set; }
        [JsonPropertyName("Serial")]
        public string? Serial { get; set; }
        [JsonPropertyName("NotBefore")]
        public DateTime NotBefore { get; set; }
        [JsonPropertyName("NotAfter")]
        public DateTime NotAfter { get; set; }
        [JsonPropertyName("Thumbprint")]
        public string? Thumbprint { get; set; }
        [JsonPropertyName("SigAlgo")]
        public string? SigAlgo { get; set; }
        [JsonPropertyName("PubAlgo")]
        public string? PubAlgo { get; set; }
        [JsonPropertyName("PrivateExists")]
        public bool? PrivateExists { get; set; }


        //This method takes in a Cert of type X509 and parses its pieces into the values above
        public LinuxCertsEntry(X509Certificate2 certificate)
        {
            Subject = certificate.Subject;
            SubjectAlternativeNameBuilder builder = new SubjectAlternativeNameBuilder();
            SAN = builder;
            Issuer = certificate.Issuer;
            Serial = certificate.GetSerialNumberString();
            NotBefore = certificate.NotBefore;
            NotAfter = certificate.NotAfter;
            Thumbprint = certificate.Thumbprint;
            var str1 = certificate.SignatureAlgorithm.FriendlyName + " " + certificate.SignatureAlgorithm.Value;
            SigAlgo = str1;
            var str2 = certificate.PublicKey.Oid.FriendlyName + " " + certificate.PublicKey.Oid.Value;
            PubAlgo = str2;
            PrivateExists = certificate.HasPrivateKey;            
        }
    }
}