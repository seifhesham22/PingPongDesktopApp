using CommunityToolkit.Mvvm.ComponentModel;
using PingPongDesktopApp.Services;

namespace PingPongDesktopApp.ViewModels
{
    /// <summary>
    /// The shell. Owns which page is showing and the transitions between them.
    /// </summary>
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly AuthService _auth;

        /// <summary>The view model currently shown in the window's content area.</summary>
        [ObservableProperty]
        private ViewModelBase _currentPage = null!;

        public MainWindowViewModel(AuthService auth)
        {
            _auth = auth;
            ShowSplash();
        }

        /// <summary>Parameterless overload for the XAML previewer only.</summary>
        public MainWindowViewModel() : this(DesignTime.AuthService) { }

        private void ShowSplash()
        {
            var splash = new SplashViewModel();
            splash.Finished += (_, _) => ShowLogin();
            CurrentPage = splash;
        }

        private void ShowLogin()
        {
            var login = new LoginViewModel(_auth);
            login.LoggedIn += (_, _) => OnLoggedIn();
            login.SignUpRequested += (_, _) => ShowRegister();
            CurrentPage = login;
        }

        private void ShowRegister()
        {
            var register = new RegisterViewModel(_auth);
            register.LoginRequested += (_, _) => ShowLogin();

            // Registration doesn't sign the user in, so send them back to log in.
            register.Registered += (_, _) => ShowLogin();
            CurrentPage = register;
        }

        private void OnLoggedIn()
        {
            // Next step: swap in the main chat shell once that view exists.
        }
    }
}
