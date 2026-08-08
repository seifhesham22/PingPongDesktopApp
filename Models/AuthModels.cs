namespace PingPongDesktopApp.Models
{
    public sealed record LoginRequest(string Email, string Password);

    public sealed record LoginResponse(
        string TokenType,
        string AccessToken,
        int ExpiresIn,
        string RefreshToken);

    /// <summary>
    /// The ProblemDetails shape the API returns on failure. The API puts the
    /// human-readable message in Title and its own error code in Type.
    /// </summary>
    public sealed record ApiProblem(string? Title, string? Type, int? Status, string? Detail);
}
