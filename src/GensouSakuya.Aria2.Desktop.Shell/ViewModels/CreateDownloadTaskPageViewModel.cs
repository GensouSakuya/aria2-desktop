using Avalonia.Controls;
using GensouSakuya.Aria2.Desktop.Shell.Helper;
using ReactiveUI;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class CreateDownloadTaskPageViewModel : ViewModelBase
    {
        public CreateDownloadTaskPageViewModel()
        {
            DownloadLink = "";
        }

        public string TorrentFilePath { get; set; }
        private string _downloadLink = "";

        public string DownloadLink
        {
            get => _downloadLink;
            set => this.RaiseAndSetIfChanged(ref _downloadLink, value);
        }

        public async Task Submit()
        {
            await Aria2Helper.Aria2.StartDownload(DownloadLink);
            Close();
        }

        public void Close()
        {
            if (Self != null)
            {
                Self.Close();
            }
        }

        public ICommand SubmitTorrent =>
            ReactiveCommand.Create(async () =>
            {
                var res = await DialogHelper.OpenFileDialog(new List<FileDialogFilter>
                {
                    new FileDialogFilter
                    {
                        Name = "Torrent",
                        Extensions =
                        {
                            "torrent"
                        }
                    }
                });
                if (res == null || res.Length < 1)
                {
                    return;
                }

                var torrentFilePath = res[0];
                await Aria2Helper.Aria2.NewTorrentDownload(torrentFilePath);
                Close();
            });

        private const string MagnetPrefix = "magnet:?xt=urn:btih:";

        public ICommand AddMagnetPrefixCommand =>
            ReactiveCommand.Create(() =>
            {
                if ((!DownloadLink?.Contains(MagnetPrefix)) ?? false)
                {
                    DownloadLink = $"magnet:?xt=urn:btih:{DownloadLink}";
                }
            });
    }
}
