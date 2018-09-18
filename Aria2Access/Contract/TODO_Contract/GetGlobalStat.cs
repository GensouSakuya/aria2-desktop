namespace Aria2Access.Contract
{
    internal class GetGlobalStatRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.getGlobalStat

        protected override string MethodName => "aria2.getGlobalStat";

        protected override void PrepareParam()
        {
        }
    }

    internal class GetGlobalStatResponse : BaseResponse
    {
        public GetGlobalStatResponse(BaseResponse res) : base(res)
        {
        }
    }
}
