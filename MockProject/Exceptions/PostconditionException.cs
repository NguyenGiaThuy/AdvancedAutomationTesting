namespace MockProject.Exceptions;

public class PostconditionException : Exception
{
    public (string Message, string? InnerException) Details { get; }

    public PostconditionException(string message, Exception? innerException = null)
        : base(message)
    {
        Details = (message, innerException?.ToString());
    }

    public override string ToString()
    {
        return base.ToString()
            + (Details.Message != null ? $", Message: {Details.Message}" : "")
            + (Details.InnerException != null ? $", InnerException: {Details.InnerException}" : "");
    }
}
