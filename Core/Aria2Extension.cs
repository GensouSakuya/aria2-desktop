using System.Threading.Tasks;

namespace Core
{
    public static class Aria2Extension
    {
        public static async Task<string> StartDownload(this Main main, string url)
        {
            return await main.Aria2.AddUri(url);
        }
    }
}
