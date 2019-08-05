using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GensouSakuya.Aria2.Desktop.Model
{
    public class DownloadTask
    {
        [Key]
        public string GID { get; set; }
        public string TaskName { get; set; }
        public DownloadStatus Status { get; set; }
        public long TotalLength { get; set; }
        public long CompletedLength { get; set; }

        public decimal CompletePercent
        {
            get { return TotalLength == 0 ? 0 : Math.Round((decimal) CompletedLength / TotalLength * 100, 2); }
        }
        
        [NotMapped]
        public decimal DownloadSpeed { get; set; }
        [NotMapped]
        public decimal UploadSpeed { get; set; }

        public override bool Equals(object obj)
        {
            return obj is DownloadTask task &&
                   GID == task.GID &&
                   TaskName == task.TaskName &&
                   Status == task.Status &&
                   TotalLength == task.TotalLength &&
                   CompletedLength == task.CompletedLength;
        }

        public override int GetHashCode()
        {
            var hashCode = -2130185868;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(GID);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(TaskName);
            hashCode = hashCode * -1521134295 + Status.GetHashCode();
            hashCode = hashCode * -1521134295 + TotalLength.GetHashCode();
            hashCode = hashCode * -1521134295 + CompletedLength.GetHashCode();
            return hashCode;
        }
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
