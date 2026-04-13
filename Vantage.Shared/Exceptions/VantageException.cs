namespace Vantage.Shared.Exceptions
{
    public abstract class VantageException(string title, string message, int statusCode) : Exception(message)
    {
        public string Title { get; } = title;
        public int StatusCode { get; } = statusCode;
    }
}