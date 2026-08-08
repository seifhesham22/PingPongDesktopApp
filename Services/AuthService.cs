using PingPongDesktopApp.Models;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PingPongDesktopApp.Services
{
    public sealed class AuthService
    {
        private readonly HttpClient _http;
        private readonly TokenStore _tokens;

        private static readonly JsonSerializerOptions JsonOptions = new()
        {
            PropertyNameCaseInsensitive = true
        };

        public AuthService(HttpClient http, TokenStore tokens)
        {
            _http = http;
            _tokens = tokens;
        }

        public async Task<ApiResult<LoginResponse>> LoginAsync(
            string email, string password, CancellationToken cancellationToken = default)
        {
            try
            {
                var response = await _http.PostAsJsonAsync(
                    "auth/login",
                    new LoginRequest(email, password),
                    cancellationToken);

                if (response.IsSuccessStatusCode)
                {
                    var login = await response.Content.ReadFromJsonAsync<LoginResponse>(
                        JsonOptions, cancellationToken);

                    if (login is null)
                        return ApiResult<LoginResponse>.Failure("The server returned an empty response.");

                    _tokens.Set(login.AccessToken, login.RefreshToken);
                    return ApiResult<LoginResponse>.Success(login);
                }

                return ApiResult<LoginResponse>.Failure(
                    await ReadErrorAsync(response, "Incorrect email or password.", cancellationToken));
            }
            catch (HttpRequestException)
            {
                return ApiResult<LoginResponse>.Failure("Can't reach the server. Check your connection.");
            }
            catch (TaskCanceledException)
            {
                return ApiResult<LoginResponse>.Failure("The request timed out. Try again.");
            }
        }

        /// <summary>
        /// Identity returns 401 with no body for bad credentials, and ProblemDetails
        /// elsewhere, so fall back to a caller-supplied message.
        /// </summary>
        private static async Task<string> ReadErrorAsync(
            HttpResponseMessage response, string fallback, CancellationToken cancellationToken)
        {
            try
            {
                var problem = await response.Content.ReadFromJsonAsync<ApiProblem>(
                    JsonOptions, cancellationToken);

                if (!string.IsNullOrWhiteSpace(problem?.Title))
                    return problem.Title;
            }
            catch (Exception)
            {
                // Not a ProblemDetails body — fall through to the default message.
            }

            return fallback;
        }
    }
}
