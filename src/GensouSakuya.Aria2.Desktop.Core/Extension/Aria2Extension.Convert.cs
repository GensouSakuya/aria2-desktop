using GensouSakuya.Aria2.SDK.Model;
using GensouSakuya.Aria2.Desktop.Model;
using System;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public static partial class Aria2Extension
    {
        public static DownloadStatusInfo Convert(this DownloadStatusModel model)
        {
            DownloadStatus status;
            Enum.TryParse(model.Status, out status);
            return new DownloadStatusInfo
            {
                GID = model.GID,
                Status = status,
                CompletedLength = model.CompletedLength,
                DownloadSpeed = model.DownloadSpeed,
                TotalLength = model.TotalLength,
                UploadSpeed = model.UploadSpeed
            };
        }
    }
}
