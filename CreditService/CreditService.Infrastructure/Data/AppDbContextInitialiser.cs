using CredirService.Domain.Enums;
using Microsoft.EntityFrameworkCore;

namespace CreditService.Infrastructure.Data
{
    public class AppDbContextInitialiser
    {
        private readonly ApplicationDbContext _context;

        public AppDbContextInitialiser(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task InitialiseTaskAsync()
        {
            try
            {
                await _context.Database.MigrateAsync();
            }
            catch (Exception ex)
            {

            }
        }
        public async Task SeedAsync()
        {
            try
            {
                if (!_context.Users.Any())
                {
                    var adminId = new Guid();
                    _context.Users.AddRange(
                        new CredirService.Domain.Entity.User
                        {
                            Id = adminId,
                            FirstName = "Adm",
                            LastName = "",
                            UsertName = "admin",
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = null,
                            LastModifiedAt = DateTime.UtcNow,
                            LastModifiedBy = null

                        },
                        new CredirService.Domain.Entity.User
                        {
                            Id = Guid.NewGuid(),
                            FirstName = "Dima",
                            LastName = "L",
                            UsertName = "Dimon10",
                            CreatedAt = DateTime.UtcNow,
                            CreatedBy = adminId,
                            LastModifiedAt = DateTime.UtcNow,
                            LastModifiedBy = adminId

                        },
                         new CredirService.Domain.Entity.User
                         {
                             Id = Guid.NewGuid(),
                             FirstName = "Tolik",
                             LastName = "D",
                             UsertName = "Tolik$$",
                             CreatedAt = DateTime.UtcNow,
                             CreatedBy = adminId,
                             LastModifiedAt = DateTime.UtcNow,
                             LastModifiedBy = adminId

                         }
                        );
                }
                if (!_context.Credits.Any())
                {
                    _context.Credits.AddRange(
                       new CredirService.Domain.Entity.Credit
                       {
                           Id = Guid.NewGuid(),
                           Status = Status.Published,
                           Number = "f1",
                           Amount = 20,
                           TermValue = 2,
                           InterestValue = 0.01m,
                           CreatedAt = DateTime.UtcNow,
                           CreatedBy = null,
                           LastModifiedAt = DateTime.UtcNow,
                           LastModifiedBy = null
                       },
                       new CredirService.Domain.Entity.Credit
                       {
                           Id = Guid.NewGuid(),
                           Status = Status.Unpublished,
                           Number = "f2",
                           Amount = 10,
                           TermValue = 1,
                           InterestValue = 0.005m,
                           CreatedAt = DateTime.UtcNow,
                           CreatedBy = null,
                           LastModifiedAt = DateTime.UtcNow,
                           LastModifiedBy = null
                       });
                }

                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
