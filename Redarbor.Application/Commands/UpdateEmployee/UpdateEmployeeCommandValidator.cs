using FluentValidation;

namespace Redarbor.Application.Commands.UpdateEmployee;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.CompanyId)
            .GreaterThan(0).WithMessage("CompanyId is required.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email is required.")
            .EmailAddress().WithMessage("Email is not valid.");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password is required.");

        RuleFor(x => x.PortalId)
            .GreaterThan(0).WithMessage("PortalId is required.");

        RuleFor(x => x.RoleId)
            .GreaterThan(0).WithMessage("RoleId is required.");

        RuleFor(x => x.StatusId)
            .GreaterThan(0).WithMessage("StatusId is required.");

        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Username is required.");
    }
}