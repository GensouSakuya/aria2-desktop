using EdjCase.JsonRpc.Core;
using System;

namespace Aria2Access
{
    internal class BaseResponse
    {
        private BaseResponse()
        { }

        public BaseResponse(BaseResponse res)
        {
            Guid = res.Guid;
            IsSuccess = res.IsSuccess;
            Result = res.Result;
            ErrorMessage = res.ErrorMessage;
        }

        public BaseResponse(RpcResponse rpcRes)
        {
            Guid = new Guid(rpcRes.Id.StringValue);
            IsSuccess = !rpcRes.HasError;
            Result = rpcRes.Result;
            ErrorMessage = rpcRes.Error?.Message;
        }

        public Guid Guid { get; set; }

        public bool IsSuccess { get; private set; }

        public object Result { get; private set; }

        public string ErrorMessage { get; private set; }
    }
}
