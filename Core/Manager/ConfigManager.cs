using Model;
using Newtonsoft.Json;
using System.IO;

namespace Core
{
    public static class ConfigManager
    {
        public static ConfigInfo GetFromFile(string path = ConfigConst.Default_Config_File_Path)
        {
            var configStr = "";
            using (StreamReader file = File.OpenText(path))
            {
                configStr = file.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ConfigInfo>(configStr);
        }

        public static void SaveToFile(ConfigInfo config, string path = ConfigConst.Default_Config_File_Path)
        {
            var confStr = JsonConvert.SerializeObject(config);
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            using (StreamWriter stream = File.CreateText(path))
            {
                stream.Write(confStr);
            }
        }
    }
}
