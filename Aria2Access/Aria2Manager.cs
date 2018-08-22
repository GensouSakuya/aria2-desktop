using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Aria2Access
{
    public static class Aria2Manager
    {
        private static ServerProxy _proxy = new ServerProxy("http://localhost:6800/jsonrpc", null);

        /// <summary>
        /// 新增下载
        /// </summary>
        /// <param name="uri">HTTP/FTP/SFTP/Magnet下载链接</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        public static void AddUri(string uri, int? split = null, string proxy = null, int? position = null)
        {
            AddUri(new List<string>
            {
                uri
            }, split, proxy, position);
        }

        /// <summary>
        /// 新增同资源下多链接下载
        /// </summary>
        /// <param name="uris">相同资源下的HTTP/FTP/SFTP/Magnet下载链接集合，资源不相同则会失败</param>
        /// <param name="split">下载所有链接的总连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        public static void AddUri(IEnumerable<string> uris, int? split = 0, string proxy = null,int? position = null)
        {
            var option = split.HasValue || !string.IsNullOrWhiteSpace(proxy) ? new Options(split, proxy) : null;
            _proxy.SendRequest(new AddUriRequest
            {
                Uris = uris.ToList(),
                Options = option,
                Position = position
            });
        }

        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentBase64">Base64编码的Torrent文件</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        public static void AddTorrentBase64(string torrentBase64, int? split = 0, string proxy = null, int? position = null)
        {
            var option = split.HasValue || !string.IsNullOrWhiteSpace(proxy) ? new Options(split, proxy) : null;
            _proxy.SendRequest(new AddTorrentRequest
            {
                torrent = torrentBase64,
                Options = option,
                Position = position
            });
        }
        
        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentFilePath">Torrent文件路径</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        public static void AddTorrentFile(string torrentFilePath, int? split = 0, string proxy = null, int? position = null)
        {
            if (!File.Exists(torrentFilePath))
            {
                throw new System.Exception("Torrent文件不存在");
            }

            FileInfo fi = new FileInfo(torrentFilePath);
            AddTorrentFile(fi, split, proxy, position);
        }


        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentFile">Torrent文件对象</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        public static void AddTorrentFile(FileInfo torrentFile, int? split = 0, string proxy = null, int? position = null)
        {
            byte[] buff = new byte[torrentFile.Length];

            using (var fs = torrentFile.OpenRead())
            {
                fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            }

            var base64Torrent = Convert.ToBase64String(buff);
            AddTorrentBase64(base64Torrent, split, proxy, position);
        }
    }
}
