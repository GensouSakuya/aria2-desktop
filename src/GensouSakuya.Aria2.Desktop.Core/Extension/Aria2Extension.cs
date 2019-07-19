using GensouSakuya.Aria2.Desktop.Model;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public static partial class Aria2Extension
    {
        public static async Task<string> StartDownload(this Main main, string url)
        {
            return await main.Aria2.AddUri(url);
        }

        public static async Task<DownloadStatusInfo> GetDownloadProcess(this Main main, string gid)
        {
            var res = await main.Aria2.TellStatus(gid);
            return res.Convert();
        }
    }
}
