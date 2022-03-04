﻿namespace PoC.Keycloak.Web.Api.Responses;

public class LoginResponse
{
    public string AccessToken { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;
    public UserResponse User { get; set; } = default!;
}
