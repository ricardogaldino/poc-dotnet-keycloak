using Microsoft.AspNetCore.Mvc;
using PoC.Keycloak.Web.Api.Exceptions;
using PoC.Keycloak.Web.Api.Responses;

namespace PoC.Keycloak.Web.Api.Controllers;

[ApiController]
[Route("api/")]
public abstract class BaseController : Controller
{
    public ActionResult ResponseOk<T>(Response<T> response)
    {
        var errosResult = ValidateErros(response.Exceptions);
        if (errosResult is not null) return errosResult;
        return StatusCode(StatusCodes.Status200OK, response.Value);
    }

    public ActionResult ResponsePagedOk<T>(Response<T> response)
    {
        return StatusCode(StatusCodes.Status200OK, response.Value);
    }

    public ActionResult ResponseCreated<T>(string? actionName, Response<T> response)
    {
        var errosResult = ValidateErros(response.Exceptions);
        if (errosResult is not null) return errosResult;
        return StatusCode(StatusCodes.Status201Created, response.Value);
    }

    private ActionResult? ValidateErros(Exception[] errors)
    {
        if (errors is null) return null;
        var messages = errors.Select(e => e.Message);
        if (errors.Any(e => e is FatalException)) return StatusCode(StatusCodes.Status500InternalServerError, messages);
        if (errors.Any(e => e is NotFoundException)) return StatusCode(StatusCodes.Status404NotFound, messages);
        if (errors.Any(e => e is BusinessException)) return StatusCode(StatusCodes.Status400BadRequest, messages);
        return StatusCode(StatusCodes.Status500InternalServerError, messages);
    }
}