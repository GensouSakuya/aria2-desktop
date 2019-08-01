using GensouSakuya.Aria2.SDK.Model;
using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public class Aria2Core: IDisposable
    {
        public Aria2Core(Aria2Config config)
        {
            Aria2Config = config;
        }

        protected void Start()
        {
            //_configFilePath = configFilePath ?? ConfigConst.Default_Config_File_Path;
            if (Aria2 == null)
            {
                Aria2 = Aria2Config.IsAria2ServerExist
                    ? Aria2Helper.Connect(Aria2Config.Aria2Host, Aria2Config.ListenPort)
                    : Aria2Helper.StartUp(Aria2Config.Aria2Path, Aria2Config.ToArgs(), Aria2Config.Aria2Host, Aria2Config.ListenPort);
            }
        }

        public List<DownloadStatusInfo> GetAllTasks()
        {
            var downloadTaskModelList = new List<DownloadStatusModel>();
            var lockObj = new object();
            var taskList = new List<Task>();
            taskList.Add(Task.Factory.StartNew(() =>
            {
                lock (lockObj)
                {
                    downloadTaskModelList.AddRange(Aria2.TellActive().Result);
                }
            }));
            taskList.Add(Task.Factory.StartNew(() =>
            {
                lock (lockObj)
                {
                    downloadTaskModelList.AddRange(Aria2.TellWaiting().Result);
                }
            }));
            taskList.Add(Task.Factory.StartNew(() =>
            {
                lock (lockObj)
                {
                    downloadTaskModelList.AddRange(Aria2.TellStopped().Result);
                }
            }));
            Task.WaitAll(taskList.ToArray());
            return downloadTaskModelList.Select(p => p.Convert()).ToList();
        }

        public DownloadStatusInfo GetTaskDownloadStatus(string gid)
        {
            return Aria2.TellStatus(gid).Result.Convert();
        }

        public void Shutdown()
        {
            if (!Aria2Config.IsAria2ServerExist)
            {
                Aria2Helper.Shutdown(Aria2);
            }
        }

        public void Dispose()
        {
            Shutdown();
        }

        private readonly Aria2Config Aria2Config;

        internal GensouSakuya.Aria2.SDK.Aria2Client Aria2 { get; private set; } = null;
    }
}
