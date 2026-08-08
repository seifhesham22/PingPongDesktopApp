namespace PingPongDesktopApp.Services
{
    /// <summary>
    /// Mirrors the API's Result type: either a value or a message we can show.
    /// Keeps HttpRequestException out of the view models.
    /// </summary>
    public sealed class ApiResult<T>
    {
        public T? Value { get; }
        public string? ErrorMessage { get; }
        public bool IsSuccess { get; }

        private ApiResult(T value)
        {
            Value = value;
            IsSuccess = true;
        }

        private ApiResult(string errorMessage)
        {
            ErrorMessage = errorMessage;
            IsSuccess = false;
        }

        public static ApiResult<T> Success(T value) => new(value);
        public static ApiResult<T> Failure(string message) => new(message);
    }
}
