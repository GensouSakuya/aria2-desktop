using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using GensouSakuya.Aria2.SDK;

namespace Aria2Access
{
    public static class Aria2Core
    {
        public static Aria2Client StartUp(string path,string args,string host,int port)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new Exception("未指定Aria2路径");
            }

            if (!File.Exists(path))
            {
                throw new Exception("未找到aria2应用");
            }

            if (!Process.GetProcessesByName("aria2c-fg").Any())
            {
                var process = new Process();
                process.StartInfo.FileName = path;
                process.StartInfo.Arguments = args;
                process.Start();
            }

            return new Aria2Client(host, port);
        }

        public static void Shutdown(Aria2Client aria2)
        {
            aria2?.Shutdown();
        }

    }
}
