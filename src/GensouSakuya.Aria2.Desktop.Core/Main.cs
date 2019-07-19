using GensouSakuya.Aria2.SDK.Model;
using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public class Main: IDisposable
    {
        public Main(string configFilePath = null)
        {
            Start(configFilePath);
        }

        protected void Start(string configFilePath = null)
        {
            _configFilePath = configFilePath ?? ConfigConst.Default_Config_File_Path;

            Aria2 = Aria2Core.StartUp(Config.Aria2Path, Config.ToString(), Config.Aria2Host, Config.ListenPort);
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
            Aria2Core.Shutdown(Aria2);
        }

        public void Dispose()
        {
            Shutdown();
        }

        private volatile object configLocker = new object();
        private ConfigInfo _config = null;
        private string _configFilePath = null;

        protected ConfigInfo Config
        {
            get
            {
                if (_config == null)
                {
                    lock (configLocker)
                    {
                        if (_config == null)
                        {
                            _config = ConfigManager.GetFromFile(_configFilePath);
                            if (_config == null)
                            {
                                _config = ConfigManager.SaveToFile(path: _configFilePath);
                            }
                        }
                    }
                }

                return _config;
            }
        }

        internal GensouSakuya.Aria2.SDK.Aria2Client Aria2 { get; private set; } = null;
    }
}
