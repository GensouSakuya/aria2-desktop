using EdjCase.JsonRpc.Client;
using System;
using System.Threading.Tasks;

namespace Aria2Access
{
    internal class ServerProxy
    {
        private RpcClient _client = null;
        private string _secret = null;

        public ServerProxy(string baseUri,string secret)
        {
            var uri = new Uri(baseUri);
            _client = new RpcClient(uri);
            _secret = secret;
        }

        public BaseResponse SendRequest(BaseRequest req)
        {
            return _client.SendRequestAsync(req.ToRpcRequest()).Result.GetResponse();
        }

        public async Task<BaseResponse> SendRequestAsync(BaseRequest req)
        {
            return (await _client.SendRequestAsync(req.ToRpcRequest())).GetResponse();
        }
    }
}
