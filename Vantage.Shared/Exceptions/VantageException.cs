namespace Vantage.Shared.Exceptions
{
    /// <summary>
    /// The foundational base exception for the Project Vantage ecosystem.
    /// Enforces the RFC 9457 Problem Details standard by guaranteeing all domain exceptions
    /// carry a routable HTTP Status Code and a standard Title.
    /// </summary>
    public abstract class VantageException(string title, string message, int statusCode) : Exception(message)
    {
        /// <summary>
        /// The short, human-readable summary of the problem type.
        /// </summary>
        public string Title { get; } = title;

        /// <summary>
        /// The HTTP status code dictated by the nature of the error (e.g., 400, 404, 409).
        /// </summary>
        public int StatusCode { get; } = statusCode;
    }
}