using CredirService.Domain.Common;
using CredirService.Domain.Enums;

namespace CredirService.Domain.Entity
{
    public class Credit : BaseAuditableEntity
    {
        public Status Status { get; set; }
        public string Number { get; set; }
        public decimal Amount { get; set; }
        public int TermValue { get; set; }
        public decimal InterestValue { get; set; }
    }
}
