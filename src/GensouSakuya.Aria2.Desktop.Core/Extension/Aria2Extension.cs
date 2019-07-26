using GensouSakuya.Aria2.Desktop.Model;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public static partial class Aria2Extension
    {
        public static async Task<string> StartDownload(this Aria2Core aria2Core, string url)
        {
            return await aria2Core.Aria2.AddUri(url);
        }

        public static async Task<DownloadStatusInfo> GetDownloadProcess(this Aria2Core aria2Core, string gid)
        {
            var res = await aria2Core.Aria2.TellStatus(gid);
            return res.Convert();
        }
    }
}
