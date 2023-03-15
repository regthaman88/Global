using System.Diagnostics;

namespace HumWebAPI3.Models
{
    public static class PowerSheller
    {
        public static string PS(this string cmd)
        {
            var escapedArgs = cmd.Replace("\"", "\\\"");

            var process = new Process()
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = "C:\\Windows\\System32\\WindowsPowerShell\\v1.0\\Powershell.exe",
                    Arguments = $"\"{escapedArgs}\"",
                    RedirectStandardOutput = true,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                }
            };
            process.Start();
            string result = process.StandardOutput.ReadToEnd();
            process.WaitForExit();
            return result;
        }
    }
}