namespace Model
{
    public class DownloadStatusInfo
    {
        public string GID { get; set; }
        public DownloadStatus Status { get; set; }
        public long TotalLength { get; set; }
        public long CompletedLength { get; set; }
        public long UploadLength { get; set; }
        public string BitField { get; set; }
        public decimal DownloadSpeed { get; set; }
        public decimal UploadSpeed { get; set; }
    }

    public enum DownloadStatus
    {
        Active = 0,
        Waiting = 1,
        Paused = 2,
        Error = 3,
        Complete = 4,
        Removed = 5
    }
}
