using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using FitCompete.Infrastructure.Persistence;

namespace FitCompete.Infrastructure.Persistence.Factories
{
    /*
     * Ta fabryka jest używana WYŁĄCZNIE przez narzędzia deweloperskie w czasie projektowania (design-time),
     * takie jak `Add-Migration` czy `Update-Database`.
     * Pozwala ona narzędziom na utworzenie instancji DbContext bez konieczności uruchamiania całej aplikacji.
     */
    public class ApplicationDbContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlite("Data Source=..\\FitCompeteDatabase\\fitcompete.db",
                b => b.MigrationsAssembly("FitCompete.Infrastructure"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }
    }
}