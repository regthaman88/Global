using HumWebAPI3.Models;
using Microsoft.AspNetCore.Mvc;

namespace HumWebAPI3.Controllers
{
    [ApiController]
    [Route("/HumWebAPI3/api/[controller]")]
    public class LinuxWebsitesController : ControllerBase
    {
        private readonly ILogger<LinuxWebsitesController> _logger;
        public LinuxWebsitesController(ILogger<LinuxWebsitesController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "LinuxWebsites")]
        public IEnumerable<LinuxWebsites> Get()
        {
            //Detect Middleware Installed using a dir command and boolean Contains
            var hostname = System.Environment.MachineName.ToString();
            bool iswls = "dir /opt/oracle".Bash().ToString().Contains("middleware");
            bool istomcat = "dir /etc".Bash().ToString().Contains("tomcat");
            bool isapache = "dir /etc".Bash().ToString().Contains("httpd");

            //WebLogic Log checks
            List<string> server1log = "cat /opt/oracle/middleware/user_projects/domains/humana/servers/server1/logs/server1.out | tail -n 50".Bash().Split('\n').ToList();
            string wlsstr = "";
            foreach (string log in server1log)
            {
                    wlsstr += log + "@";
            }
            List<string> wlslogsfound= wlsstr.Split('@').ToList();


            //Apache Log checks
            string apacheserver1logs = "";
            List<string> apachelogsdir = "dir /var/log/".Bash().Split('\n').ToList();
            string apachestr = "";
            foreach (string apachelog in apachelogsdir)
            {
                if (apachelog.Contains("server.out"))
                {
                    apachestr += apachelog + ",";
                }
                if (apachelog.Contains("apache.log"))
                {
                    apachestr += apachelog + ",";
                }
            }
            List<string> apachereturnlogs = new List<string>();
            List<string> apachelogsfound = apachestr.Split(',').ToList();

            //Apache Tomcat Log checks
            string tomcatserver1logs = "";
            List<string> tomcatlogsdir = "dir /etc/tomcat/logs".Bash().Split('\n').ToList();
            string tomcatstr = "";
            foreach (string tomcatlog in tomcatlogsdir)
            {
                if (tomcatlog.Contains("catalina.out"))
                {
                    tomcatstr += tomcatlog + ",";
                }
                if (tomcatlog.Contains("tomcat.log"))
                {
                    tomcatstr += tomcatlog + ",";
                }
            }
            List<string> tomcatreturnlogs = new List<string>();
            List<string> tomcatlogsfound = tomcatstr.Split(',').ToList();

            //Set vars needed for next steps
            string? wls = null;
            string? tomcat = null;
            string? httpd = null;
            

            //
            if (iswls)
            {
                wls = "http://" + hostname + ":7001/console";
            }




            if (isapache)
            {
                httpd = "http://" + hostname + ":80";

                foreach (string log in apachelogsfound)
                {
                    if (!string.IsNullOrEmpty(log))
                    {
                        apachereturnlogs.Add(log);
                    }
                }
            }




            if (istomcat)
            {
                tomcat = "http://" + hostname + ":8080";

                foreach (string log in tomcatlogsfound)
                {
                    if (!string.IsNullOrEmpty(log))
                    {
                        tomcatreturnlogs.Add(log);
                    }
                }
            }
            


            return Enumerable.Range(1, 1).Select(index => new LinuxWebsites
            {
                //Get WebLogic Admin Site
                WLSSites = wls,

                WLSLogs = wlslogsfound,

                //WLSSecurity = null,

                //Get Apache
                ApacheSites = httpd,
                
                //ApacheLogs = apachereturnlogs,

                //ApacheSecurity = null,

                //Get Tomcat
                TomcatSites = tomcat,

                //TomcatLogs = tomcatreturnlogs,

                //TomcatSecurity = null,
            })
            .ToArray();
        }
    }
}