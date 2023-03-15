using HumWebAPI3.Models;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography.X509Certificates;


namespace HumWebAPI3.Controllers
{
    [ApiController]
    [Route("/HumWebAPI3/api/[controller]")]
    public class LinuxCertsController : ControllerBase
    {
        private readonly ILogger<LinuxCertsController> _logger;
        public LinuxCertsController(ILogger<LinuxCertsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "LinuxCerts")]
        public IEnumerable<LinuxCerts> Get()
        {
            List<LinuxCertsEntry> certlist = new List<LinuxCertsEntry>();

            X509Store store = new X509Store(StoreName.Root, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);
                
            foreach (var certificate in store.Certificates)
            {
                certlist.Add(new LinuxCertsEntry(certificate));
            }

            store.Close();

            return Enumerable.Range(1, 1).Select(index => new LinuxCerts
            {
                GeneralCertsList = certlist,
            })
            .ToArray();
        }
    }
}