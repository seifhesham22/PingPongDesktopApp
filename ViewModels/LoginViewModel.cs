using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PingPongDesktopApp.Services;
using System;
using System.Threading.Tasks;

namespace PingPongDesktopApp.ViewModels
{
    public partial class LoginViewModel : ViewModelBase
    {
        private readonly AuthService _auth;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
        private string _email = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(LogInCommand))]
        private string _password = string.Empty;

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private bool _isPasswordVisible;

        /// <summary>Raised once the user is signed in, so the shell can swap views.</summary>
        public event EventHandler? LoggedIn;

        /// <summary>Raised on a failed attempt, so the view can shake the form.</summary>
        public event EventHandler? LoginFailed;

        /// <summary>Raised when the user chooses to create an account instead.</summary>
        public event EventHandler? SignUpRequested;

        public LoginViewModel(AuthService auth) => _auth = auth;

        /// <summary>Parameterless overload for the XAML previewer only.</summary>
        public LoginViewModel() : this(DesignTime.AuthService) { }

        private bool CanLogIn() =>
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !LogInCommand.IsRunning;

        [RelayCommand(CanExecute = nameof(CanLogIn), AllowConcurrentExecutions = false)]
        private async Task LogInAsync()
        {
            ErrorMessage = null;

            var result = await _auth.LoginAsync(Email.Trim(), Password);

            if (!result.IsSuccess)
            {
                ErrorMessage = result.ErrorMessage;
                LoginFailed?.Invoke(this, EventArgs.Empty);
                return;
            }

            LoggedIn?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        private void TogglePasswordVisibility() => IsPasswordVisible = !IsPasswordVisible;

        [RelayCommand]
        private void GoToSignUp() => SignUpRequested?.Invoke(this, EventArgs.Empty);

        [RelayCommand]
        private void ForgotPassword() { /* wired up when the reset view exists */ }
    }
}
