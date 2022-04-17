using BlogManager.Web.Areas.Office.Models;
using FluentValidation;


namespace BlogManager.Web.Areas.Office.Validators
{
    public class DepartmentViewModelValidator : AbstractValidator<DepartmentViewModel>
    {
        public DepartmentViewModelValidator()
        {
            RuleFor(p => p.Name)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull()
               .MaximumLength(50).WithMessage("{PropertyName} must not exceed 50 characters.");

            RuleFor(p => p.Description)
               .NotEmpty().WithMessage("{PropertyName} is required.")
               .NotNull();

            RuleFor(p => p.Status)
           .NotEmpty().WithMessage("{PropertyName} is required.")
           .NotNull();

        }
    }
}
