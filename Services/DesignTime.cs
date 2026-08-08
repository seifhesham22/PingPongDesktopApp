using System;
using System.Net.Http;

namespace PingPongDesktopApp.Services
{
    /// <summary>
    /// Throwaway instances so the XAML previewer can construct view models.
    /// Never used at runtime — App builds the real ones.
    /// </summary>
    internal static class DesignTime
    {
        private static readonly HttpClient Http = new() { BaseAddress = new Uri("https://localhost:7008/") };

        public static AuthService AuthService { get; } = new(Http, new TokenStore(Http));
    }
}
