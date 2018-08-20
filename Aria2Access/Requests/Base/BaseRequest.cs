using EdjCase.JsonRpc.Core;
using System.Linq;

namespace Aria2Access
{
    internal abstract class BaseRequest
    {
        protected abstract string MethodName { get; }

        protected virtual RpcParameters Parameters { get {
                return new RpcParameters(this.GetType().GetProperties().ToDictionary(p => p.Name, p => p.GetValue(this)));
            } }

        public virtual RpcRequest ToRpcRequest()
        {
            return new RpcRequest(MethodName, Parameters);
        }
    }
}
