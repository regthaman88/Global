using HumWebAPI3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumWebAPI3.Controllers
{
    [ApiController]
    [Route("/HumWebAPI3/api/[controller]")]
    public class LinuxInfoController : ControllerBase
    {
        private readonly ILogger<LinuxInfoController> _logger;
        public LinuxInfoController(ILogger<LinuxInfoController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "LinuxInfo")]
        public IEnumerable<LinuxInfo> Get()
        {
            List<char> charsToRemove = new List<char>() { '\n', '_', '"',};

            var ver = "3.0.0";

            List<string> capable = "LinuxHosts,LinuxWebSites,LinuxCerts".Split(',').ToList();

            var hostname = Environment.MachineName.ToUpper();

            var env = System.Environment.MachineName.ToString().Substring(7).Remove(1);

            var osver = Environment.OSVersion.ToString();

            var domain = "cat /etc/resolv.conf | grep search | awk '{print $2}'".Bash().StrCln(charsToRemove);

            var uptime = "uptime -s".Bash().StrCln(charsToRemove);

            var loctime = "timedatectl | grep Local | awk '{print $5}'".Bash().StrCln(charsToRemove);

            var timezone = "timedatectl | grep Local | awk '{print $6}'".Bash().StrCln(charsToRemove);

            var cpucores = int.Parse("cat /proc/cpuinfo | grep 'core id' | awk '{print $4}' | awk 'END { print NR }'".Bash());

            var releasestr = "cat /etc/os-release | grep PRETTY_NAME | awk -F '=' '{print $2}'".Bash().StrCln(charsToRemove);
            var release = "";
            if (releasestr.Contains("Red Hat Enterprise Linux"))
            {
                string releaseid = "cat /etc/os-release | grep VERSION_ID".Bash().Substring(11).StrCln(charsToRemove);
                release += releasestr + " " + releaseid;
            }
            else
            {
                release += releasestr;
            }

            var totalmem = int.Parse("cat /proc/meminfo | grep MemTotal: | awk '{print $2}'".Bash());

            var usedmem = int.Parse("cat /proc/meminfo | grep Active: | awk '{print $2}'".Bash());

            var virt = "cat /proc/modules | grep vmx".Bash().ToString().Contains("vmxnet3");

            List<string> javastr = "java -version &> /var/dotnet/HumWebAPI3/jdk.txt; cat /var/dotnet/HumWebAPI3/jdk.txt".Bash().Split('\n').ToList();
            string java = javastr.ElementAt(0);

            List<string> admins = "cat /etc/sudoers | grep %G | awk '{print $1}'".Bash().Split('\n').ToList();

            List<string> ciphers = "openssl ciphers -v | awk '{print $2}' | sort | uniq".Bash().Split('\n').ToList();


            List<LinuxDotNet> dotnetlist = new List<LinuxDotNet>();
            List<string> dtnt = "/root/dotnet/dotnet --list-runtimes | awk '{print $1,$2}'".Bash().Split('\n').ToList();
            foreach (string i in dtnt)
            {
                dotnetlist.Add(new LinuxDotNet(i));
            }

            List<LinuxNetworks> netlist = new List<LinuxNetworks>();
            List<string> nets = "ifconfig | grep inet | grep -v -e 'inet6' -e 'inet 127' | awk '{print $2,$4,$6}'".Bash().Split('\n').ToList();
            foreach (string i in nets)
            {
                netlist.Add(new LinuxNetworks(i));
            }

            List<LinuxDisks> drivelist = new List<LinuxDisks>();
            List<string> drvs = "df -hT | grep -v -e 'tmpfs' -e 'Filesystem'| awk '{print $1,$2,$3,$5,$7}'| awk '{print $5,$3,$4}'".Bash().Split('\n').ToList();
            foreach (string i in drvs)
            {
                {
                    drivelist.Add(new LinuxDisks(i));
                }
            }


            return Enumerable.Range(1, 1).Select(index => new LinuxInfo
            {
                //Get HostName
                HostName = hostname,

                //Set API Version
                Version = ver,

                //Get Domain
                Domain = domain,

                //Get Environment
                Env = env,

                //Get OS Version
                OsVer = osver,

                //Get OS Release (Caption)
                OsCaption = release,

                //Get Uptime
                LastBootUpTime = uptime,

                //Get Local Time
                LocalDateTime = loctime,

                //Get Time Zone
                CurrentTimeZone = timezone,

                //Is Server Virtual
                IsHosted = virt,

                //Get Java Version
                JavaInstalled = java,

                //Get Server CPU Cores
                Cpus = cpucores,

                //Get Server Total Memory
                TotalMemory = totalmem,

                //Get Server Used Memory
                UsedMemory = usedmem,

                //Get Server Frameworks
                LinuxDotNet = dotnetlist,

                //Get Server Networks
                LinuxNetworks = netlist,

                //Get Server Admins
                Sudoers = admins,

                //Get Server Capabilities
                Capabilities = capable,

                //Get Server Drives
                LinuxDisks = drivelist,

                //Get Handshake Protocols
                Ciphers = ciphers,
            })
            .ToArray();
        }
    }
}