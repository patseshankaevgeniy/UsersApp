using FluentValidation;
using Users.Application.Clients.Models;

namespace Users.Application.Users.Models;

public class UserModelValidator : AbstractValidator<CreatedUserModel>
{
    public UserModelValidator()
    {
        RuleFor(u => u.Name).NotEmpty();
        RuleFor(u => u.Email).NotEmpty().EmailAddress();
        RuleFor(u => u.Age).ExclusiveBetween(1, 100);
        RuleFor(u => u.Password).NotEmpty();
    }
}
