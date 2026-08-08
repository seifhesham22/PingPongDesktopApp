using System.Collections.Generic;

namespace PingPongDesktopApp.Models
{
    public sealed record LoginRequest(string Email, string Password);

    public sealed record RegisterRequest(string Email, string Password);

    /// <summary>
    /// Identity returns 400 with this shape when registration fails, keyed by
    /// its own error codes (DuplicateEmail, PasswordTooShort, and so on).
    /// </summary>
    public sealed record IdentityValidationProblem(
        string? Title,
        int? Status,
        Dictionary<string, string[]>? Errors);

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
