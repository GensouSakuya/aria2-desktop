using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Aria2Access
{
    public class Aria2
    {
        internal Aria2(string host, int port)
        {
            if (!host.StartsWith("http://") && !host.StartsWith("https://"))
            {
                host = "http://" + host;
            }
            var url = $"{host}:{port}/jsonrpc";
            _proxy = new ServerProxy(url, null);
        }

        private ServerProxy _proxy = null;

        /// <summary>
        /// 新增下载
        /// </summary>
        /// <param name="uri">HTTP/FTP/SFTP/Magnet下载链接</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public async Task<string> AddUri(string uri, int? split = null, string proxy = null, int? position = null)
        {
            return await AddUri(new List<string>
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
        /// <returns>下载请求的GID</returns>
        public async Task<string> AddUri(IEnumerable<string> uris, int? split = 0, string proxy = null,int? position = null)
        {
            var option = split.HasValue || !string.IsNullOrWhiteSpace(proxy) ? new Options(split, proxy) : null;
            var res = new AddUriResponse(await _proxy.SendRequestAsync(new AddUriRequest
            {
                Uris = uris.ToList(),
                Options = option,
                Position = position
            }));
            return res?.GID;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gid"></param>
        /// <param name="keys"></param>
        public async Task<DownloadStatusModel> TellStatus(string gid, Expression<Func<DownloadStatusModel, DownloadStatusModel>> keys = null)
        {
            var strKeys = new List<string>();
            if (keys != null)
            {
                MemberInitExpression init = keys.Body as MemberInitExpression;
                strKeys.AddRange(init.Bindings.Select(p => p.Member.Name));
            }
            var res = new TellStatusResponse(await _proxy.SendRequestAsync(new TellStatusRequest
            {
                GID = gid,
                Keys = strKeys
            }));
            return res?.Info;
        }

        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentBase64">Base64编码的Torrent文件</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public string AddTorrentBase64(string torrentBase64, int? split = 0, string proxy = null, int? position = null)
        {
            var option = split.HasValue || !string.IsNullOrWhiteSpace(proxy) ? new Options(split, proxy) : null;
            return (_proxy.SendRequest(new AddTorrentRequest
            {
                torrent = torrentBase64,
                Options = option,
                Position = position
            }) as AddTorrentResponse)?.GID;
        }

        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentFilePath">Torrent文件路径</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public string AddTorrentFile(string torrentFilePath, int? split = 0, string proxy = null, int? position = null)
        {
            if (!File.Exists(torrentFilePath))
            {
                throw new System.Exception("Torrent文件不存在");
            }

            FileInfo fi = new FileInfo(torrentFilePath);
            return AddTorrentFile(fi, split, proxy, position);
        }
        
        /// <summary>
        /// 新增BT下载
        /// </summary>
        /// <param name="torrentFile">Torrent文件对象</param>
        /// <param name="split">下载连接数</param>
        /// <param name="proxy">代理地址</param>
        /// <param name="position">下载队列位置，超过队列长度则排到队尾</param>
        /// <returns>下载请求的GID</returns>
        public string AddTorrentFile(FileInfo torrentFile, int? split = 0, string proxy = null, int? position = null)
        {
            byte[] buff = new byte[torrentFile.Length];

            using (var fs = torrentFile.OpenRead())
            {
                fs.Read(buff, 0, Convert.ToInt32(fs.Length));
            }

            var base64Torrent = Convert.ToBase64String(buff);
            return AddTorrentBase64(base64Torrent, split, proxy, position);
        }

        public void Shutdown()
        {
            //TODO:如果等待响应则会锁住，但文档中说调用后是有响应的
            _proxy.SendRequestWithoutResponse(new ShutdownRequest { });
        }
    }
}
