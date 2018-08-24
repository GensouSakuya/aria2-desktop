namespace Aria2Access
{
    internal class ShutdownRequest : BaseRequest
    {
        protected override string MethodName => "aria2.shutdown";

        protected override void PrepareParam()
        {
        }
    }

    internal class ShutdownResponse : BaseResponse
    {
        public ShutdownResponse(BaseResponse res) : base(res)
        {
        }
    }
}
