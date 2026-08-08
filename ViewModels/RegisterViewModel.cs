using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using PingPongDesktopApp.Services;
using System;
using System.Threading.Tasks;

namespace PingPongDesktopApp.ViewModels
{
    public partial class RegisterViewModel : ViewModelBase
    {
        private readonly AuthService _auth;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignUpCommand))]
        private string _email = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignUpCommand))]
        private string _password = string.Empty;

        [ObservableProperty]
        [NotifyCanExecuteChangedFor(nameof(SignUpCommand))]
        private string _confirmPassword = string.Empty;

        [ObservableProperty]
        private string? _errorMessage;

        [ObservableProperty]
        private bool _isPasswordVisible;

        /// <summary>Raised once the account exists, so the shell can return to login.</summary>
        public event EventHandler? Registered;

        /// <summary>Raised on a failed attempt, so the view can shake the form.</summary>
        public event EventHandler? RegistrationFailed;

        /// <summary>Raised when the user chooses to go back to the login screen.</summary>
        public event EventHandler? LoginRequested;

        public RegisterViewModel(AuthService auth) => _auth = auth;

        /// <summary>Parameterless overload for the XAML previewer only.</summary>
        public RegisterViewModel() : this(DesignTime.AuthService) { }

        private bool CanSignUp() =>
            !string.IsNullOrWhiteSpace(Email) &&
            !string.IsNullOrWhiteSpace(Password) &&
            !string.IsNullOrWhiteSpace(ConfirmPassword) &&
            !SignUpCommand.IsRunning;

        [RelayCommand(CanExecute = nameof(CanSignUp), AllowConcurrentExecutions = false)]
        private async Task SignUpAsync()
        {
            ErrorMessage = null;

            // Checked here rather than server-side: the API only receives one password.
            if (Password != ConfirmPassword)
            {
                Fail("The passwords don't match.");
                return;
            }

            var result = await _auth.RegisterAsync(Email.Trim(), Password);

            if (!result.IsSuccess)
            {
                Fail(result.ErrorMessage);
                return;
            }

            Registered?.Invoke(this, EventArgs.Empty);
        }

        [RelayCommand]
        private void TogglePasswordVisibility() => IsPasswordVisible = !IsPasswordVisible;

        [RelayCommand]
        private void GoToLogin() => LoginRequested?.Invoke(this, EventArgs.Empty);

        private void Fail(string? message)
        {
            ErrorMessage = message;
            RegistrationFailed?.Invoke(this, EventArgs.Empty);
        }
    }
}
