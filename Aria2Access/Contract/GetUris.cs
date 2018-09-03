using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aria2Access
{
    internal class GetUrisRequest : BaseRequest
    {
        public string GID { get; set; }

        protected override string MethodName => "aria2.getUris";

        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);
        }
    }

    internal class GetUrisResponse : BaseResponse
    {
        public GetUrisResponse(BaseResponse res) : base(res)
        {
            Info = JsonConvert.DeserializeObject<UriModel>(res.Result as string);
        }

        public UriModel Info { get; private set; }
    }
}
