using FluentValidation;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace PoC.Keycloak.Web.Api.Exceptions;

public static class EcxeptionValidator
{
    public static Exception[] Exceptions<T>(this AbstractValidator<T> validator, T instance)
    {
        var validationResult = validator.Validate(instance);
        if (validationResult.IsValid) return default!;
        return validationResult.Errors.Select(_ => new ValidationException(_.ErrorMessage)).ToArray();
    }
}