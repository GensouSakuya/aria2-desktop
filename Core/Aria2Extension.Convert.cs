using Aria2Access;
using Model;
using System;

namespace Core
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
            };
        }
    }
}
