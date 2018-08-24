namespace Shell
{
    public class NewDownloadViewModel : BindingBase
    {
        private string _downloadUrl = "123";

        public string DownloadUrl
        {
            get { return _downloadUrl; }
            set { Set(ref _downloadUrl, value); }
        }
    }
}
