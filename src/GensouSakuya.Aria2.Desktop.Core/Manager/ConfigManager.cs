using GensouSakuya.Aria2.Desktop.Model;
using Newtonsoft.Json;
using System.IO;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public static class ConfigManager
    {
        public static ConfigInfo GetFromFile(string path)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new System.Exception("缺少配置文件路径");
            }

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

        public static ConfigInfo SaveToFile(string path, ConfigInfo config = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new System.Exception("缺少配置文件路径");
            }

            //不指定则保存默认配置
            if (config == null)
            {
                config = new ConfigInfo();
            }
            var confStr = JsonConvert.SerializeObject(config);

            if (!File.Exists(path))
            {
                var dir = new FileInfo(path).Directory;
                if (!dir.Exists)
                {
                    dir.Create();
                }

                var st = File.Create(path);
                st.Close();
            }

            using (StreamWriter stream = File.CreateText(path))
            {
                stream.Write(confStr);
            }

            return config;
        }
    }
}
