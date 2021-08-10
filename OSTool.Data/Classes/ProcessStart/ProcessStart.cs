using System;
using System.IO;
using System.Diagnostics;

namespace OSTool.Core
{
    public class ProcessStart
    {
        public static Process Process(string location, string filename, string[] arguments, bool redirect)
        {
            var proc = new Process { StartInfo = { FileName = filename, CreateNoWindow = true, UseShellExecute = false, RedirectStandardError = redirect, WindowStyle = ProcessWindowStyle.Hidden } };

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
                throw new ApplicationException("Process start failed, necessary components unavailable.");
            }
            return proc;
        }
    }
}