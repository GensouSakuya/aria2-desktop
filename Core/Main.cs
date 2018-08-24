using Aria2Access;
using Model;
using System;

namespace Core
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

            _aria2 = Aria2Manager.StartUp(Config.Aria2Path, Config.ToString(), Config.Aria2Host, Config.ListenPort);
        }

        public void Shutdown()
        {
            Aria2Manager.Shutdown(_aria2);
        }

        public void Dispose()
        {
            Shutdown();
        }

        private volatile object configLocker = new object();
        private ConfigInfo _config = null;
        private string _configFilePath = null;
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

        public Aria2 Aria2
        {
            get { return _aria2; }
        }
    }
}
