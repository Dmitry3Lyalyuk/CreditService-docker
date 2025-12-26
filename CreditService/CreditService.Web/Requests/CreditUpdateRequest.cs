using CredirService.Domain.Enums;

namespace CreditService.Web.Requests
{
    public record CreditUpdateRequest
    {
        public Status Status { get; set; }
    }
}
