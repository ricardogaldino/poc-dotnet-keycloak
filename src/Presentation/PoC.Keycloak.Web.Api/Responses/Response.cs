namespace PoC.Keycloak.Web.Api.Responses;

public class Response<TResponse> : BaseResponse
{
    protected Response(Exception[] exceptions) : base(exceptions)
    {
    }

    protected Response(TResponse value)
    {
        Value = value;
    }

    public TResponse Value { get; } = default!;

    public static Response<TResponse> Success(TResponse value)
    {
        return new Response<TResponse>(value);
    }

    public static Response<TResponse> Failure(params Exception[] exceptions)
    {
        return new Response<TResponse>(exceptions);
    }

    public void Deconstruct(out TResponse response, out Exception[] exceptions)
    {
        response = Value;
        exceptions = Exceptions;
    }
}