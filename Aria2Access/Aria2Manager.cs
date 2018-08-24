using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Aria2Access
{
    public static class Aria2Manager
    {
        public static Aria2 StartUp(string path,string args,string host,int port)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new Exception("未指定Aria2路径");
            }

            if (!File.Exists(path))
            {
                throw new Exception("未找到aria2应用");
            }

            if (!Process.GetProcessesByName("aria2c").Any())
            {
                var process = new Process();
                process.StartInfo.FileName = path;
                process.StartInfo.Arguments = args;
                process.Start();
            }

            return new Aria2(host, port);
        }

        public static void Shutdown(Aria2 aria2)
        {
            aria2?.Shutdown();
        }

    }
}
