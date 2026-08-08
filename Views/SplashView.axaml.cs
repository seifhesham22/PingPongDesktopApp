using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Threading;
using Avalonia.VisualTree;
using PingPongDesktopApp.ViewModels;
using System;
using System.Threading.Tasks;

namespace PingPongDesktopApp.Views
{
    public partial class SplashView : UserControl
    {
        private bool _played;

        public SplashView()
        {
            InitializeComponent();
        }

        protected override async void OnLoaded(RoutedEventArgs e)
        {
            base.OnLoaded(e);

            if (_played) return;
            _played = true;

            // Hold, then hand over. The cross-fade in MainWindow covers the swap,
            // so this view doesn't animate its own exit.
            await Task.Delay(SplashViewModel.Duration);
            if (!Root.IsAttachedToVisualTree()) return;

            (DataContext as SplashViewModel)?.NotifyFinished();
        }
    }
}
