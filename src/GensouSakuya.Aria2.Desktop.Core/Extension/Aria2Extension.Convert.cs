using GensouSakuya.Aria2.SDK.Model;
using GensouSakuya.Aria2.Desktop.Model;
using System;
using System.Linq;
using System.IO;

namespace GensouSakuya.Aria2.Desktop.Core
{
    public static class Aria2Extension
    {
        public static DownloadTask Convert(this DownloadStatusModel model)
        {
            DownloadStatus status;
            Enum.TryParse(model.Status, true, out status);
            var filename = "新任务";
            if (model.Files != null && model.Files.Count == 1)
            {
                var file = model.Files.First();
                if (File.Exists(file.Path))
                {
                    filename = new FileInfo(file.Path).Name;
                }
            }
            return new DownloadTask
            {
                GID = model.GID,
                Status = status,
                //暂时先不处理多文件的任务名
                TaskName = filename,
                CompletedLength = model.CompletedLength,
                DownloadSpeed = model.DownloadSpeed,
                TotalLength = model.TotalLength,
                UploadSpeed = model.UploadSpeed
            };
        }
    }
}
