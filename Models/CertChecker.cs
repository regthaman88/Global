using CommandLine;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace HumWebAPI3.Models
{

    public static class CertChecker
    {

        public static void Main(string[] args)
        {
            Parser.Default.ParseArguments<ListOptions>(args)
                .WithParsed<ListOptions>(
                    opts => ListCertificates(
                        Enum.Parse<StoreName>(
                            opts.StoreName,
                            ignoreCase: true),
                        Enum.Parse<StoreLocation>(
                            opts.StoreLocation,
                            ignoreCase: true)))
                .WithNotParsed(
                    errs =>
                        Console.WriteLine(
                            $"Error parsing\n {string.Join('\n', errs)}"));
        }

        private static void ListCertificates(StoreName storeName, StoreLocation storeLocation)
        {
            var store = new X509Store(storeName, storeLocation);
            store.Open(OpenFlags.ReadOnly);

            if (store.Certificates.Count > 0)
            {
                Console.WriteLine($"Certificates stored in '{storeName}' certificate store (location: {storeLocation}):");
                Console.WriteLine();

                var counter = 0;
                foreach (var certificate in store.Certificates)
                {
                    counter++;
                    Console.WriteLine($"#{counter}:");

                    var certificateInfo = new Dictionary<string, string>
                    {
                        { "Subject", certificate.Subject },
                        { "Issuer", certificate.Issuer },
                        { "Serial Number", certificate.GetSerialNumberString() },
                        { "Not Before", certificate.GetEffectiveDateString() },
                        { "Not After", certificate.GetExpirationDateString() },
                        { "Thumbprint", certificate.Thumbprint },
                        { "Signature Algorithm", $"{certificate.SignatureAlgorithm.FriendlyName} ({certificate.SignatureAlgorithm.Value})" },
                        { "PublicKey Algorithm", $"{certificate.PublicKey.Oid.FriendlyName} ({certificate.PublicKey.Oid.Value})" },
                        { "Has PrivateKey", certificate.HasPrivateKey ? "Yes" : "No" }
                    };

                    foreach (var info in certificateInfo)
                    {
                        Console.WriteLine($"  {info.Key,-20}: {info.Value}");
                    }

                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine($"No certificates found in '{storeName}' certificate store (location: {storeLocation}).");
            }

            store.Close();
        }
    }

    [Verb("list", HelpText = "List all certificates in selected store.")]
    internal sealed class ListOptions : BaseOptions { }

    internal abstract class BaseOptions
    {
        [Option(shortName: 's', longName: "store-name", Default = "Root", HelpText = "Certificate store name (My, Root, etc.). See 'System.Security.Cryptography.X509Certificates.StoreName' for more information.")]
        public string StoreName { get; set; }

        [Option(shortName: 'l', longName: "store-location", Default = "LocalMachine", HelpText = "Certificate store location (CurrentUser, LocalMachine, etc.). See 'System.Security.Cryptography.X509Certificates.StoreLocation' for more information.")]
        public string StoreLocation { get; set; }
    }
}