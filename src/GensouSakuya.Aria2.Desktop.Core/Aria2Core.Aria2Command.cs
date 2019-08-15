using System;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public partial class Aria2Core: IDisposable
    {
        /// <summary>
        /// 创建Http/Magnet下载任务
        /// </summary>
        /// <param name="url">下载链接</param>
        /// <returns>任务GID</returns>
        private async Task<string> StartHttpDownload(string url)
        {
            return await Aria2.AddUri(url);
        }

        /// <summary>
        /// 创建BT种子下载任务
        /// </summary>
        /// <param name="torrentFilePath"></param>
        /// <returns></returns>
        private async Task<string> StartTorrentFileDownload(string torrentFilePath)
        {
            return await Aria2.AddTorrentFile(torrentFilePath);
        }
    }
}
