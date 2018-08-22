using Model;
using Newtonsoft.Json;

namespace Core
{
    public static class ConfigManager
    {
        public static ConfigInfo Get(string path)
        {
            var configStr = "";
            using (System.IO.StreamReader file = System.IO.File.OpenText(path))
            {
                configStr = file.ReadToEnd();
            }

            return JsonConvert.DeserializeObject<ConfigInfo>(configStr);
        }
    }
}
