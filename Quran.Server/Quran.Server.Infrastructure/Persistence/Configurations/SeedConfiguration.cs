using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.Extensions.Configuration;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Infrastructure.Persistence.Configurations
{
    public class SeedConfiguration : IEntityTypeConfiguration<SeedingEntry>
    {
        public void Configure(EntityTypeBuilder<SeedingEntry> builder)
        {
            builder.ToTable("__SeedingHistory");
            builder.HasKey(s => s.Name);
        }
    }

    public class EmployeesDatabaseContextFactory : IDesignTimeDbContextFactory<ApplicationDbContext>
    {

        public ApplicationDbContext CreateDbContext(string[] args)
        {
            var dbContext = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer(
                new ConfigurationBuilder()
                    .AddJsonFile(Path.Combine(Directory.GetCurrentDirectory(), $"appsettings.json"))
                    .AddEnvironmentVariables()
                    .AddUserSecrets("5ceeed6e-1185-4717-a3b7-0e705f281cd4")
                    .Build()
                    .GetConnectionString("QuranDb")
            ).Options);
            return dbContext;
        }
    }
}