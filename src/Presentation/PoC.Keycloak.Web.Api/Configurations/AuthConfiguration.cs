namespace PoC.Keycloak.Web.Api.Configurations;

public class AuthConfiguration
{
    public string GetAccessTokenUrl { get; set; } = default!;
    public string ClientId { get; set; } = default!;
    public string ClientSecret { get; set; } = default!;
}