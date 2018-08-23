using Aria2Access;
using Model;
using System;

namespace Core
{
    public class Main: IDisposable
    {
        public void Start(string configFilePath = null)
        {
            _configFilePath = configFilePath;

            //1.获取配置

            //2.根据配置文件组装Aria2配置

            //3.使用配置启动Aria2
        }

        public void Shutdown()
        {

        }

        public void Dispose()
        {
            Shutdown();
        }

        private volatile object configLocker = new object();
        private ConfigInfo _config = null;
        private string _configFilePath = null;
        private volatile object aria2Locker = new object();
        private Aria2 _aria2 = null;

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

        protected Aria2 Aria2
        {
            get
            {
                if (_aria2 == null)
                {
                    lock (aria2Locker)
                    {
                        if (_aria2 == null)
                        {
                            //TODO:增加path
                            _aria2 = Aria2Manager.StartUp("",Config.ToString(),Config.Aria2Host, Config.ListenPort);
                        }
                    }
                }

                return _aria2;
            }
        }
    }
}
