using EdjCase.JsonRpc.Client;
using System;

namespace Aria2Access
{
    internal class ServerProxy
    {
        private RpcClient _client = null;

        public ServerProxy(string baseUri)
        {
            var uri = new Uri(baseUri);
            _client = new RpcClient(uri);
        }

        public void SendRequest(BaseRequest req)
        {
            _client.SendRequestAsync(req.ToRpcRequest());
        }
    }
}
