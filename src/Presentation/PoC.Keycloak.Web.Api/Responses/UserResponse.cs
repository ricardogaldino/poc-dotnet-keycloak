namespace PoC.Keycloak.Web.Api.Responses;

public class UserResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
}