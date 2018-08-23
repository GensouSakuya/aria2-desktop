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

        public void Dispose()
        {
            if (_config != null)
            {
                _config = ConfigManager.SaveToFile();
            }
        }


        private volatile object lockObj = new object();
        private ConfigInfo _config = null;
        private string _configFilePath = null;

        protected ConfigInfo Config
        {
            get
            {
                if (_config == null)
                {
                    lock (lockObj)
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
    }
}
