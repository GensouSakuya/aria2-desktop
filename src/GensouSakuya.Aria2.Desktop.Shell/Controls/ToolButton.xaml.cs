using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;
using GensouSakuya.Aria2.Desktop.Shell.Views;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class ToolButton : UserControl
    {
        public ToolButton()
        {
            this.InitializeComponent();

            Initialized += (sender, e) =>
            {
                Tapped += (sender2, e2) =>
                {
                    if (this.DataContext is ToolButtonViewModel)
                    {
                        var model = this.DataContext as ToolButtonViewModel;
                        if (model.Click != null)
                        {
                            model.Click();
                        }
                    }
                };
            };
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
