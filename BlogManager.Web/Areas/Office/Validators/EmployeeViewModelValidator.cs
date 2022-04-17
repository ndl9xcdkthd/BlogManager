using BlogManager.Web.Areas.Office.Models;
using FluentValidation;

namespace BlogManager.Web.Areas.Office.Validators
{
    public class EmployeeViewModelValidator : AbstractValidator<EmployeeViewModel>
    {
        public EmployeeViewModelValidator()
        {

            RuleFor(p => p.FristName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.LastName)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
           RuleFor(p => p.DateOfBirth)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();
            RuleFor(p => p.Gender)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.HireDate)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.Position)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.DepartmentId)
              .NotEmpty().WithMessage("{PropertyName} is required.")
              .NotNull();
            RuleFor(p => p.Status)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();
        }
    }
}
