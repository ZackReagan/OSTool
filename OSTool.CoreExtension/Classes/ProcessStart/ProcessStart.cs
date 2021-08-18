using System.IO;
using OSTool.Core;
using System.Diagnostics;

namespace OSTool.CoreExtension
{
    public class ProcessStart
    {
        public static void Process(string location, string filename, string[] arguments)
        {
            var proc = new Process { StartInfo = { FileName = filename, CreateNoWindow = true, UseShellExecute = false, WindowStyle = ProcessWindowStyle.Hidden } };

            if (File.Exists(location))
            {
                foreach (var arg in arguments)
                {
                    proc.StartInfo.Arguments = arg;
                    proc.Start();
                }
            }
            else
            {
                Dialog.Show("Process start failed", "Program will continue as usual. Necessary components unavailable.");
            }
        }
    }
}