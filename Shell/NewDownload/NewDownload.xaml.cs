﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Core;

namespace Shell
{
    /// <summary>
    /// NewDownload.xaml 的交互逻辑
    /// </summary>
    public partial class NewDownload : Window
    {
        NewDownloadViewModel model = new NewDownloadViewModel();

        public NewDownload()
        {
            InitializeComponent();
            this.TextBox1.SetBinding(TextBox.TextProperty, new Binding
            {
                Path = new PropertyPath(nameof(NewDownloadViewModel.DownloadUrl)),
                Source = model,
                Mode = BindingMode.TwoWay
            });
        }
        
        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            await MainManager.Main.StartDownload(model.DownloadUrl);
        }
    }
}