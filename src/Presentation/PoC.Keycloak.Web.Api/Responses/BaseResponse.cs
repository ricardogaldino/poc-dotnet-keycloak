namespace PoC.Keycloak.Web.Api.Responses;

public class BaseResponse
{
    protected BaseResponse()
    {
    }

    protected BaseResponse(Exception[] exceptions)
    {
        Exceptions = exceptions;
    }

    public Exception[] Exceptions { get; } = default!;

    public bool HasErrors()
    {
        return Exceptions is not null && Exceptions.Any();
    }
}