using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DataAccessLayer
{
    public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<MangaProjectDbContext>
    {
        public MangaProjectDbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<MangaProjectDbContext>();

            // usa a mesma string do OnConfiguring ou do appsettings.json
            optionsBuilder.UseSqlServer(
                @"Server=localhost\SQLEXPRESS;Database=WebMangaProjectDb;Trusted_Connection=True;",
                options => options.EnableRetryOnFailure(5)
            );

            return new MangaProjectDbContext(optionsBuilder.Options);
        }
    }
}
