using EdjCase.JsonRpc.Core;
using System;

namespace Aria2Access
{
    internal class BaseResponse
    {
        public Guid Guid { get; set; }

        public bool IsSuccess { get; set; }

        public object Result { get; set; }

        public string ErrorMessage { get;  set; }
    }

    internal static class RpcResponseExtension
    {
        internal static BaseResponse GetResponse(this RpcResponse rpcRes)
        {
            var res = new BaseResponse();
            res.Guid = new Guid(rpcRes.Id.StringValue);
            res.IsSuccess = !rpcRes.HasError;
            res.Result = rpcRes.Result;
            res.ErrorMessage = rpcRes.Error?.Message;
            return res;
        }
    }
}
