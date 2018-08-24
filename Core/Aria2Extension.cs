namespace Core
{
    public static class Aria2Extension
    {
        public static string StartDownload(this Main main, string url)
        {
            return main.Aria2.AddUri(url);
        }
    }
}
