using GensouSakuya.Aria2.Desktop.Shell.Helper;
using System.Threading.Tasks;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels
{
    public class CreateDownloadTaskViewModel: ViewModelBase
    {
        public CreateDownloadTaskViewModel()
        {
            DownloadLink = "";
        }

        public string DownloadLink { get; set; }

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
    }
}
