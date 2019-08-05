using GensouSakuya.Aria2.SDK.Model;
using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Linq;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public static class Aria2Extension
    {
        public static DownloadTask Convert(this DownloadStatusModel model)
        {
            DownloadStatus status;
            Enum.TryParse(model.Status, true, out status);
            return new DownloadTask
            {
                GID = model.GID,
                Status = status,
                //暂时先不处理多文件的任务名
                TaskName = (model.Files?.Count ?? 0)== 1? new System.IO.FileInfo(model.Files.First().Path).Name:"新任务",
                CompletedLength = model.CompletedLength,
                DownloadSpeed = model.DownloadSpeed,
                TotalLength = model.TotalLength,
                UploadSpeed = model.UploadSpeed
            };
        }
    }
}
