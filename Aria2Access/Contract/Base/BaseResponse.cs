using EdjCase.JsonRpc.Core;
using System;

namespace Aria2Access
{
    internal class BaseResponse
    {
        public BaseResponse()
        { }

        public BaseResponse(RpcResponse rpcRes)
        {
            Guid = new Guid(rpcRes.Id.StringValue);
            IsSuccess = !rpcRes.HasError;
            Result = rpcRes.Result;
            ErrorMessage = rpcRes.Error?.Message;
        }

        public Guid Guid { get; set; }

        public bool IsSuccess { get; set; }

        public object Result { get; set; }

        public string ErrorMessage { get;  set; }
    }
}
