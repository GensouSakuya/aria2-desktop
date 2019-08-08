using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Primitives;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class ToolMiniButton : UserControl
    {
        public ToolMiniButton()
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
                            IsProcessing = true;
                            Task.Run(() =>
                            {
                                Thread.Sleep(50000);
                                model.Click();
                            }).ContinueWith((task) =>
                            {
                                IsProcessing = false;
                            }, TaskScheduler.FromCurrentSynchronizationContext());
                        }
                    }
                };
            };

            PseudoClass<ToolMiniButton>(IsProcessingProperty, ":processing");
        }

        public static readonly DirectProperty<ToolMiniButton, bool> IsProcessingProperty = 
            AvaloniaProperty.RegisterDirect<ToolMiniButton, bool>(nameof(IsProcessing), o => o.IsProcessing,
                (o, value) => o.IsProcessing = value, defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);
        


        private bool _isProcessing = true;
        public bool IsProcessing
        {
            get => _isProcessing;
            set { SetAndRaise(IsProcessingProperty, ref _isProcessing, value); }
        } 

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
