namespace PoC.Keycloak.Web.Api.Responses;

public class VoidResponse : BaseResponse
{
    protected VoidResponse()
    {
    }

    protected VoidResponse(Exception[] exceptions) : base(exceptions)
    {
    }

    public static VoidResponse Success()
    {
        return new VoidResponse();
    }

    public static VoidResponse Failure(params Exception[] exceptions)
    {
        return new VoidResponse(exceptions);
    }

    public void Deconstruct(out Exception[] exceptions)
    {
        exceptions = Exceptions;
    }
}