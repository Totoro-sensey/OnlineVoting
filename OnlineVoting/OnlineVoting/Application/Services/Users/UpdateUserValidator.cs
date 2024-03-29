using FluentValidation;
using OnlineVoting.DTOs.UserDtos.UpdateDto;

namespace OnlineVoting.Services.Users;

public class UpdateUserValidator : AbstractValidator<UserDto>
{
    public UpdateUserValidator()
    {
        RuleFor(i => i.Password)
            .NotEmpty()
            .WithMessage("The password must contain at least 8 characters");
        RuleFor(i => i.PersonalInformation.PassportNumber)
            .NotNull()
            .Length(6)
            .WithMessage("The passport number must contain 6 characters");
        RuleFor(i => i.PersonalInformation.PassportSeries)
            .NotNull()
            .Length(12)
            .WithMessage("The passport series must contain 12 characters");
    }
}