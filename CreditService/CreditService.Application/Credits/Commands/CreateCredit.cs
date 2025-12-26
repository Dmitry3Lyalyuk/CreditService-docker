using CredirService.Domain.Entity;
using CredirService.Domain.Enums;
using CreditService.Application.Common;
using MediatR;

namespace CreditService.Application.Credits.Commands
{
    public class CreateCreditCommand : IRequest<Guid>
    {
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public int TermValue { get; set; }
        public decimal InterestValue { get; set; }
    }

    public class CreateCreditCommandHandler : IRequestHandler<CreateCreditCommand, Guid>
    {
        private readonly IApplicationDbContext _context;
        public CreateCreditCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<Guid> Handle(CreateCreditCommand request, CancellationToken cancellationToken)
        {
            var entity = new Credit()
            {
                Number = request.Number,
                Amount = request.Amount,
                TermValue = request.TermValue,
                InterestValue = request.InterestValue,
                Status = Status.Published,
            };

            _context.Credits.Add(entity);

            await _context.SaveChangedAsync(cancellationToken);

            return entity.Id;
        }
    }
}
