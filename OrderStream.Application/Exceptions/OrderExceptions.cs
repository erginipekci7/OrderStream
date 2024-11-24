namespace OrderStream.Application.Exceptions;

public class OrderNotFoundException : Exception
{
    public OrderNotFoundException(string message) : base(message) { }
    public OrderNotFoundException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class OrderValidationException : Exception
{
    public OrderValidationException(string message) : base(message) { }
    public OrderValidationException(string message, Exception innerException) 
        : base(message, innerException) { }
} 