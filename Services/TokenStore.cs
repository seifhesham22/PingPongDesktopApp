using System.Net.Http;
using System.Net.Http.Headers;

namespace PingPongDesktopApp.Services
{
    /// <summary>
    /// Holds the bearer token for the session and attaches it to the shared
    /// HttpClient. In-memory only: closing the app signs you out.
    /// </summary>
    public sealed class TokenStore
    {
        private readonly HttpClient _http;

        public string? AccessToken { get; private set; }
        public string? RefreshToken { get; private set; }
        public bool IsAuthenticated => !string.IsNullOrEmpty(AccessToken);

        public TokenStore(HttpClient http) => _http = http;

        public void Set(string accessToken, string refreshToken)
        {
            AccessToken = accessToken;
            RefreshToken = refreshToken;
            _http.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue("Bearer", accessToken);
        }

        public void Clear()
        {
            AccessToken = null;
            RefreshToken = null;
            _http.DefaultRequestHeaders.Authorization = null;
        }
    }
}
