using CredirService.Domain.Enums;
using CreditService.Application.Common;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CreditService.Application.Credits.Queries
{
    public record GetAllCreditsQuery(
        Status? Status = null,
        decimal? MinAmount = null,
        decimal? MaxAmount = null,
        int? MinTerm = null,
        int? MaxTerm = null
        ) : IRequest<List<CreditDTO>>
    {

    }
    public class GetAllCreditsQueryHandler : IRequestHandler<GetAllCreditsQuery, List<CreditDTO>>
    {
        private readonly IApplicationDbContext _context;
        public GetAllCreditsQueryHandler(IApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<CreditDTO>> Handle(GetAllCreditsQuery request, CancellationToken cancellationToken)
        {
            var query = _context.Credits.AsNoTracking();

            if (request.Status.HasValue)
            {
                query = query.Where(c => c.Status == request.Status.Value);
            }

            if (request.MinAmount.HasValue) query = query.Where(c => c.Amount >= request.MinAmount.Value);
            if (request.MaxAmount.HasValue) query = query.Where(c => c.Amount <= request.MaxAmount.Value);
            if (request.MinTerm.HasValue) query = query.Where(c => c.TermValue >= request.MinTerm.Value);
            if (request.MaxTerm.HasValue) query = query.Where(c => c.TermValue <= request.MaxTerm.Value);
            {

            }
            return await query.Select(c => new CreditDTO
            {
                Number = c.Number,
                Amount = c.Amount,
                TermValue = c.TermValue,
                InterestValue = c.InterestValue,
                Status = c.Status,
                CreatedAt = c.CreatedAt,
                LastModifiedAt = c.LastModifiedAt

            }).ToListAsync(cancellationToken);
        }
    }
}
