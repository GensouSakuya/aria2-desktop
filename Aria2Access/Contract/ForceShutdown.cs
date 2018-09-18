namespace Aria2Access
{
    internal class ForceShutdownRequest : BaseRequest
    {
        protected override string MethodName => "aria2.forceShutdown";

        protected override void PrepareParam()
        {
        }
    }

    internal class ForceShutdownResponse : BaseResponse
    {
        public ForceShutdownResponse(BaseResponse res) : base(res)
        {
        }
    }
}
