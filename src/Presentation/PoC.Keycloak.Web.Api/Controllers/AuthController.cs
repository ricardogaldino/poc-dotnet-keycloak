using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PoC.Keycloak.Web.Api.Configurations;
using PoC.Keycloak.Web.Api.Exceptions;
using PoC.Keycloak.Web.Api.Responses;

namespace PoC.Keycloak.Web.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class AuthController : BaseController
{
    private readonly IConfiguration _configuration;

    public AuthController(
        IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpGet("v1/login/")]
    public async Task<ActionResult<LoginResponse>> Login(string userName, string password)
    {
        var response = new LoginResponse();
        response.User = new UserResponse {Email = "usuario@abcbrasil.com.br", Id = 1, Name = "Usuario Silva"};
        var authConfigurationDto = _configuration.GetSection("Jwt").Get<AuthConfiguration>();

        using (var httpClient = new HttpClient())
        {
            using (var request = new HttpRequestMessage(new HttpMethod("POST"), authConfigurationDto.GetAccessTokenUrl))
            {
                request.Headers.TryAddWithoutValidation("Accept", "*/*");
                request.Headers.TryAddWithoutValidation("Accept-Encoding", "gzip, deflate");
                request.Headers.TryAddWithoutValidation("Cache-Control", "no-cache");
                request.Headers.TryAddWithoutValidation("Connection", "keep-alive");

                var content =
                    $"grant_type=password&client_id={authConfigurationDto.ClientId}&client_secret={authConfigurationDto.ClientSecret}&username={userName}&password={password}";

                request.Content = new StringContent(content);
                request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");
                request.Headers.TryAddWithoutValidation("Content-Length", content.Length.ToString());
                var requestResponse = await httpClient.SendAsync(request);

                using (var reader = new StreamReader(await requestResponse.Content.ReadAsStreamAsync()))
                {
                    var json = await reader.ReadToEndAsync();
                    dynamic responseModel = JsonConvert.DeserializeObject(json)!;
                    response.AccessToken = responseModel.access_token;
                    response.RefreshToken = responseModel.refresh_token;
                }
            }
        }

        if (response.AccessToken != null)
            return ResponseOk(Response<LoginResponse>.Success(response));
        return ResponseOk(Response<LoginResponse>.Failure(new BusinessException("Invalid credentials.")));
    }
}