using Microsoft.Extensions.DependencyInjection;

namespace CreditService.Infrastructure.Data
{
    public static class InitialiserExtensions
    {
        public static async Task InitialiseDbAsync(this IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            var initialiser = scope.ServiceProvider.GetRequiredService<AppDbContextInitialiser>();

            await initialiser.InitialiseTaskAsync();
            await initialiser.SeedAsync();
        }
    }
}
