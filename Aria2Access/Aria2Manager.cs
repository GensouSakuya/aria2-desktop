using System.Collections.Generic;
using System.Linq;

namespace Aria2Access
{
    public static class Aria2Manager
    {
        private static ServerProxy _proxy = new ServerProxy("http://localhost:6800/jsonrpc", null);
        
        public static void AddUri(string uri, int? split = null, string proxy = null, int? position = null)
        {
            AddUri(new List<string>
            {
                uri
            }, split, proxy, position);
        }

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
    }
}
