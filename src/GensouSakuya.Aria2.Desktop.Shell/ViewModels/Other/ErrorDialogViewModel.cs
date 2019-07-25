using GensouSakuya.Aria2.Desktop.Shell.Enums;

namespace GensouSakuya.Aria2.Desktop.Shell.ViewModels.Other
{
    public class ErrorDialogViewModel: ViewModelBase
    {
        public string Message { get; set; }
        public ErrorLevel ErrorLevel { get; set; }

        //TODO:找弹窗素材图片
        public string ErrorImg => "";
    }
}
