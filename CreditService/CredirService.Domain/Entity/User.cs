using CredirService.Domain.Common;

namespace CredirService.Domain.Entity
{
    public class User : BaseAuditableEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UsertName { get; set; }
    }
}
