using HumWebAPI3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumWebAPI3.Controllers
{
    [ApiController]
    [Route("/HumWebAPI3/api/[controller]")]
    public class LinuxHostsController : ControllerBase
    {
        private readonly ILogger<LinuxHostsController> _logger;
        public LinuxHostsController(ILogger<LinuxHostsController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "LinuxHosts")]
        public IEnumerable<LinuxHosts> Get()
        {
            List<LinuxHostsEntry> hostlist = new List<LinuxHostsEntry>();
            List<string> hst = "cat /etc/hosts".Bash().Split('\n').ToList();
            foreach (string entry in hst)
            {
                hostlist.Add(new LinuxHostsEntry(entry));
            }

            return Enumerable.Range(1, 1).Select(index => new LinuxHosts
            {
                HostListing = hostlist,
            })
            .ToArray();
        }
    }
}