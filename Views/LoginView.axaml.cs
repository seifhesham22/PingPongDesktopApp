using Avalonia.Controls;
using PingPongDesktopApp.Controls;
using PingPongDesktopApp.ViewModels;
using System;

namespace PingPongDesktopApp.Views
{
    public partial class LoginView : UserControl
    {
        private LoginViewModel? _viewModel;

        public LoginView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            if (_viewModel is not null)
                _viewModel.LoginFailed -= OnLoginFailed;

            _viewModel = DataContext as LoginViewModel;

            if (_viewModel is not null)
                _viewModel.LoginFailed += OnLoginFailed;
        }

        private async void OnLoginFailed(object? sender, EventArgs e) =>
            await ShakeBehavior.ShakeAsync(FormPanel);
    }
}
