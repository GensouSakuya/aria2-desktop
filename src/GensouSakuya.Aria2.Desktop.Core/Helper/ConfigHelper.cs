using GensouSakuya.Aria2.Desktop.Model;
using Newtonsoft.Json;
using System.IO;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public static class ConfigHelper
    {
        public static Aria2Config GetFromFile(string path)
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

            return JsonConvert.DeserializeObject<Aria2Config>(configStr);
        }

        public static Aria2Config SaveToFile(string path, Aria2Config aria2Config = null)
        {
            if (string.IsNullOrWhiteSpace(path))
            {
                throw new System.Exception("缺少配置文件路径");
            }

            //不指定则保存默认配置
            if (aria2Config == null)
            {
                aria2Config = new Aria2Config();
            }
            var confStr = JsonConvert.SerializeObject(aria2Config);

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

            return aria2Config;
        }
    }
}
