using System;
using System.Diagnostics;
using System.IO;

namespace Aria2Access
{
    public static class Aria2Manager
    {
        public static Aria2 StartUp(string path,string args,string host,int port)
        {
            if (!File.Exists(path))
            {
                throw new Exception("未找到aria2应用");
            }

            var process = new Process();
            process.StartInfo.FileName = path;
            process.StartInfo.Arguments = args;
            process.Start();

            return new Aria2(host, port);
        }


    }
}
