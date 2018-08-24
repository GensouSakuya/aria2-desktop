using EdjCase.JsonRpc.Core;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Aria2Access
{
    internal class TellStatusRequest:BaseRequest
    {
        public string GID { get; set; }
        public List<string> Keys { get; set; }

        protected override string MethodName => "aria2.tellStatus";
        protected override void PrepareParam()
        {
            if (string.IsNullOrWhiteSpace(GID))
            {
                throw new Exception();
            }

            AddParam(GID);

            if (Keys != null && Keys.Any())
            {
                AddParam(Keys);
            }
        }
    }

    internal class TellStatusResponse : BaseResponse
    {
        public TellStatusResponse(BaseResponse res) : base(res)
        {
            Info = JsonConvert.DeserializeObject<DownloadStatusModel>(res.Result as string);
        }
        public DownloadStatusModel Info { get; private set; }
    }


}
