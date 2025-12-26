using CredirService.Domain.Enums;
using CreditService.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CreditService.Application.Credits.Commands
{
    public record UpdateCreditCommand : IRequest
    {
        public Guid Id { get; set; }
        public Status Status { get; set; }
        public string Number { get; set; }
    }

    public class UpdateCreditCommandHandler : IRequestHandler<UpdateCreditCommand>
    {
        private readonly IApplicationDbContext _context;
        public UpdateCreditCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task Handle(UpdateCreditCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Credits
            .FirstOrDefaultAsync(x => x.Number == request.Number, cancellationToken);

            if (entity == null)
            {
                throw new Exception($"Entity with Number={request.Number} was not found.");
            }
            entity.Status = request.Status;

            await _context.SaveChangedAsync(cancellationToken);
        }
    }
}
