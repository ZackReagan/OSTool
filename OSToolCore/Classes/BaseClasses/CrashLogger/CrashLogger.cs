using System;
using System.IO;
using System.Diagnostics;

namespace OSTool.Core
{
    class CrashLogger
    {
        public static void Log(Exception error)
        {
            var path = Path.Combine(Environment.CurrentDirectory.ToString(), @"OSTool.CoreLogs");
            var file = Path.Combine(path, "OSTool.Core.txt");
            var trace = new StackTrace(error, true);
            var line = Environment.NewLine;

            try
            {
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                using (StreamWriter write = File.AppendText(file))
                {
                    write.WriteLine("------------------- Crash Log creation " + DateTime.Now.ToString() + " -------------------");
                    write.WriteLine(line + "Error Cause: " + error.Message.ToString() + line + line + "Error Location: " + trace.GetFrame(0).GetMethod().ReflectedType.FullName + " at line " + trace.GetFrame(0).GetFileLineNumber() + ", column " + trace.GetFrame(0).GetFileColumnNumber() + "." + line);
                    write.WriteLine("------------------------------------- End -------------------------------------" + line);
                    write.Flush();
                    write.Close();
                }
            }
            catch
            {
                Directory.Delete(path, true);
                throw new ApplicationException("Crash log write error, closing now.");
            }
        }
    }
}