using Model;

namespace Core
{
    public static class Main
    {
        private static ConfigInfo _config = null;

        public static void Start()
        {
            //1.获取配置
            _config = ConfigManager.GetFromFile();
            if (_config == null)
            {
                _config = ConfigManager.SaveToFile();
            }

            //2.根据配置文件组装Aria2配置

            //3.使用配置启动Aria2
        }
    }
}
