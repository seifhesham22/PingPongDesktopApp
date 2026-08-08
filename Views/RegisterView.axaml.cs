using Avalonia.Controls;
using PingPongDesktopApp.Controls;
using PingPongDesktopApp.ViewModels;
using System;

namespace PingPongDesktopApp.Views
{
    public partial class RegisterView : UserControl
    {
        private RegisterViewModel? _viewModel;

        public RegisterView()
        {
            InitializeComponent();
            DataContextChanged += OnDataContextChanged;
        }

        private void OnDataContextChanged(object? sender, EventArgs e)
        {
            if (_viewModel is not null)
                _viewModel.RegistrationFailed -= OnRegistrationFailed;

            _viewModel = DataContext as RegisterViewModel;

            if (_viewModel is not null)
                _viewModel.RegistrationFailed += OnRegistrationFailed;
        }

        private async void OnRegistrationFailed(object? sender, EventArgs e) =>
            await ShakeBehavior.ShakeAsync(FormPanel);
    }
}
