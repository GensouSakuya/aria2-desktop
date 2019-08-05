using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GensouSakuya.Aria2.Desktop.Model
{
    public class DownloadTask
    {
        [Key]
        public string GID { get; set; }
        public DownloadStatus Status { get; set; }
        public long TotalLength { get; set; }
        public long CompletedLength { get; set; }

        public decimal CompletePercent
        {
            get { return TotalLength == 0 ? 0 : Math.Round(CompletePercent / TotalLength, 1); }
        }
        
        [NotMapped]
        public decimal DownloadSpeed { get; set; }
        [NotMapped]
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
