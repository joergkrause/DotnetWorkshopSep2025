using FluentValidation;
using Workshop.DataTransferModels;

namespace Workshop.Persistence.Validation;

public class CustomerAddDtoValidator : AbstractValidator<CustomerAddDto>
{
  
  public CustomerAddDtoValidator(ICustomValidatorService customValidatorService)
  {
    RuleFor(c => c.Name)
      .NotEmpty().WithMessage("Name is required.")
      .MaximumLength(customValidatorService.MaxNameLength).WithMessage("Name cannot exceed 100 characters.");
    RuleFor(c => c.Email)
      .NotEmpty().WithMessage("Email is required.")
      .EmailAddress().WithMessage("A valid email is required.")
      .MaximumLength(200).WithMessage("Email cannot exceed 200 characters.");
    RuleFor(c => c.Phone)
      .MaximumLength(20).WithMessage("Phone number cannot exceed 20 characters.")
      .When(c => !string.IsNullOrEmpty(c.Phone));
  }
}
