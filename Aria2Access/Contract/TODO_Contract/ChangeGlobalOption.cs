namespace Aria2Access.Contract
{
    internal class ChangeGlobalOptionRequest : BaseRequest
    {
        //https://aria2.github.io/manual/en/html/aria2c.html#aria2.changeGlobalOption

        protected override string MethodName => "aria2.changeGlobalOption";

        protected override void PrepareParam()
        {
        }
    }

    internal class ChangeGlobalOptionResponse : BaseResponse
    {
        public ChangeGlobalOptionResponse(BaseResponse res) : base(res)
        {
        }
    }
}
