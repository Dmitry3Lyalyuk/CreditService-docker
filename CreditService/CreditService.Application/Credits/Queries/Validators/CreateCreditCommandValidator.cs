using CreditService.Application.Common;
using CreditService.Application.Credits.Commands;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace CreditService.Application.Credits.Queries.Validators
{
    public class CreateCreditCommandValidator : AbstractValidator<CreateCreditCommand>
    {
        private readonly IApplicationDbContext _context;
        public CreateCreditCommandValidator(IApplicationDbContext context)
        {
            _context = context;

            RuleFor(n => n.Number)
                .NotNull()
                .NotEmpty()
                .WithMessage("Number is required")
                .MustAsync(BeUniqueTitle).WithMessage("Number should be unique");

            RuleFor(a => a.Amount)
                .NotEmpty()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(t => t.TermValue)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(1);

            RuleFor(i => i.InterestValue)
                .NotNull()
                .NotEmpty()
                .GreaterThanOrEqualTo(0.1m);
        }
        private async Task<bool> BeUniqueTitle(string name, CancellationToken cancellationToken)
        {
            return !await _context.Credits.AnyAsync(n => n.Number == name, cancellationToken);
        }
    }
}
