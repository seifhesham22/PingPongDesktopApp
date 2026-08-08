using CommunityToolkit.Mvvm.ComponentModel;
using PingPongDesktopApp.Services;

namespace PingPongDesktopApp.ViewModels
{
    public partial class MainWindowViewModel : ViewModelBase
    {
        private readonly AuthService _auth;

        /// <summary>The view model currently shown in the window's content area.</summary>
        [ObservableProperty]
        private ViewModelBase _currentPage = null!;

        public MainWindowViewModel(AuthService auth)
        {
            _auth = auth;
            ShowLogin();
        }

        /// <summary>Parameterless overload for the XAML previewer only.</summary>
        public MainWindowViewModel() : this(DesignTime.AuthService) { }

        private void ShowLogin()
        {
            var login = new LoginViewModel(_auth);
            login.LoggedIn += (_, _) => OnLoggedIn();
            CurrentPage = login;
        }

        private void OnLoggedIn()
        {
            // Next step: swap in the main chat shell once that view exists.
        }
    }
}
