using Autofac;
using MahApps.Metro.Controls;
using MSI.Keyboard.Backlight.Manager.UI.ViewModels;
using System;
using System.Windows;

namespace MSI.Keyboard.Backlight.Manager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public MainWindow()
        {
            DataContext = App.ContainerScope.Resolve<MainWindowViewModel>();

            InitializeComponent();
        }

        public void RestoreFromTray()
        {
            WindowState = WindowState.Normal;
        }

        private void MyNotifyIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            RestoreFromTray();
        }

        private void MetroWindow_StateChanged(object sender, EventArgs e)
        {
            if(WindowState == WindowState.Minimized)
            {
                ShowInTaskbar = false;
                NotifyIcon.Visibility = Visibility.Visible;
            }
            else
            {
                ShowInTaskbar = true;
                NotifyIcon.Visibility = Visibility.Hidden;
            }
        }
    }
}
