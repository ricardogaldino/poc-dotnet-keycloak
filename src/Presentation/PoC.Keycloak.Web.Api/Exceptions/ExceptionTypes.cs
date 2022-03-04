namespace PoC.Keycloak.Web.Api.Exceptions;

public class BusinessException : Exception
{
    public BusinessException(string message, Exception? innerException = default!) : base(message, innerException)
    {
    }
}

public class NotFoundException : BusinessException
{
    public NotFoundException(string message, Exception? innerException = default!) : base(message, innerException)
    {
    }
}

public class FatalException : BusinessException
{
    public FatalException(string message, Exception? innerException = default!) : base(message, innerException)
    {
    }
}