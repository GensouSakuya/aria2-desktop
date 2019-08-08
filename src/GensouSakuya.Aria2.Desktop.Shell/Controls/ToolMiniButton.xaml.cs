using System;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Animation;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GensouSakuya.Aria2.Desktop.Shell.Controls.ViewModels;

namespace GensouSakuya.Aria2.Desktop.Shell.Controls
{
    public class ToolMiniButton : UserControl
    {
        private const double DefaultOpacity = 0.7;
        private static readonly TimeSpan DefaultAnimationDuration = new TimeSpan(0, 0, 0, 0, 200);

        public ToolMiniButton()
        {
            this.InitializeComponent();

            Initialized += (sender, e) =>
            {
                Tapped += NewTappedEvent;
                PointerEnter += NewPointerEnter;
                PointerLeave += NewPointerLeave;
            };
            
            Opacity = DefaultOpacity;
            Transitions.Add(new DoubleTransition()
            {
                Property = OpacityProperty, Duration = DefaultAnimationDuration
            });

            PseudoClass<ToolMiniButton>(IsProcessingProperty, ":processing");
            PseudoClass<ToolMiniButton>(IsProcessingProperty, ":canpress");
        }

        public static readonly DirectProperty<ToolMiniButton, bool> IsProcessingProperty = 
            AvaloniaProperty.RegisterDirect<ToolMiniButton, bool>(nameof(IsProcessing), o => o.IsProcessing,
                (o, value) => o.IsProcessing = value, defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);
        private bool _isProcessing = false;
        public bool IsProcessing
        {
            get => _isProcessing;
            set
            {
                Opacity = 0.1d;
                SetAndRaise(IsProcessingProperty, ref _isProcessing, value);
            }
        } 
        
        public static readonly DirectProperty<ToolMiniButton, bool> CanPressProperty = 
            AvaloniaProperty.RegisterDirect<ToolMiniButton, bool>(nameof(CanPress), o => o.CanPress,
                (o, value) => o.CanPress = value, defaultBindingMode: Avalonia.Data.BindingMode.TwoWay);
        private bool _canPress = true;
        public bool CanPress
        {
            get => _canPress;
            set { SetAndRaise(CanPressProperty, ref _canPress, value); }
        }


        private void NewTappedEvent(object sender, RoutedEventArgs e)
        {
            if (!CanPress)
                return;
            if (this.DataContext is ToolButtonViewModel)
            {
                var model = this.DataContext as ToolButtonViewModel;
                if (model.Click != null)
                {
                    BeforeClick();
                    Task.Run(() =>
                    {
                        model.Click();
                    }).ContinueWith((task) =>
                    {
                        AfterClick();
                    }, TaskScheduler.FromCurrentSynchronizationContext());
                }
            }
        }

        private void BeforeClick()
        {
            IsProcessing = true;
            CanPress = false;
            OpacityChange(0.2);
        }

        private void AfterClick()
        {
            IsProcessing = false;
            CanPress = true;
            OpacityResume();
        }

        private void OpacityChange(double value)
        {
            Opacity = value;
        }

        private void OpacityResume()
        {
            OpacityChange(DefaultOpacity);
        }

        private void NewPointerEnter(object sender, PointerEventArgs e)
        {
            if (!IsProcessing)
            {
                OpacityChange(1);
            }
        }

        private void NewPointerLeave(object sender, PointerEventArgs e)
        {
            if (!IsProcessing)
            {
                OpacityResume();
            }
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
