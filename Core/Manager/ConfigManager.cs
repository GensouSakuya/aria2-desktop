using Model;
using Newtonsoft.Json;
using System.IO;

namespace Core
{
    public static class ConfigManager
    {
        public static ConfigInfo GetFromFile(string path = ConfigConst.Default_Config_File_Path)
        {
            if (!File.Exists(path))
            {
                return null;
            }

            string configStr = "";
            using (StreamReader file = File.OpenText(path))
            {
                configStr = file.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ConfigInfo>(configStr);
        }

        public static ConfigInfo SaveToFile(ConfigInfo config = null, string path = ConfigConst.Default_Config_File_Path)
        {
            //不指定则保存默认配置
            if (config == null)
            {
                config = new ConfigInfo();
            }
            var confStr = JsonConvert.SerializeObject(config);
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            using (StreamWriter stream = File.CreateText(path))
            {
                stream.Write(confStr);
            }

            return config;
        }
    }
}
