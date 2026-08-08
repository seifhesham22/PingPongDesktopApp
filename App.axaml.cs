using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using PingPongDesktopApp.Services;
using PingPongDesktopApp.ViewModels;
using PingPongDesktopApp.Views;
using System;
using System.Net.Http;

namespace PingPongDesktopApp
{
    public partial class App : Application
    {
        /// <summary>Local API. Move to config before this ships anywhere.</summary>
        private const string ApiBaseAddress = "https://localhost:7008/";

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                var http = new HttpClient
                {
                    BaseAddress = new Uri(ApiBaseAddress),
                    Timeout = TimeSpan.FromSeconds(30)
                };

                var tokens = new TokenStore(http);
                var auth = new AuthService(http, tokens);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainWindowViewModel(auth),
                };

                // Closing the main window shuts the app down, rather than leaving
                // the process alive holding the exe and any open connections.
                desktop.ShutdownMode = ShutdownMode.OnMainWindowClose;
                desktop.ShutdownRequested += (_, _) => http.Dispose();
            }

            base.OnFrameworkInitializationCompleted();
        }
    }
}
