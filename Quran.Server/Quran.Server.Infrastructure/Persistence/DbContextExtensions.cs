using System;
using System.IO;
using System.Linq;

using Microsoft.EntityFrameworkCore;

using Quran.Server.Domain.Entities;

namespace Quran.Server.Infrastructure.Persistence
{
    public static class DbContextExtensions
    {
        public static void SeedQuranData(this ApplicationDbContext context)
        {
            var assembly = typeof(DbContextExtensions).Assembly;
            var files = assembly.GetManifestResourceNames();

            var executedSeedings = context.SeedingEntries.ToArray();
            var filePrefix = $"{assembly.GetName().Name}.Data.";
            foreach (var file in files.Where(f => f.StartsWith(filePrefix) && f.EndsWith(".sql"))
                .Select(f => new
                {
                    PhysicalFile = f,
                    LogicalFile = f.Replace(filePrefix, String.Empty)
                })
                .OrderBy(f => f.LogicalFile))
            {
                if (executedSeedings.Any(e => e.Name == file.LogicalFile))
                    continue;

                string command;
                using (Stream stream = assembly.GetManifestResourceStream(file.PhysicalFile))
                {
                    using StreamReader reader = new StreamReader(stream ?? throw new InvalidOperationException("Seeding file cannot be read"));
                    command = reader.ReadToEnd();
                }

                if (String.IsNullOrWhiteSpace(command))
                    continue;

                using var transaction = context.Database.BeginTransaction();
                try
                {
                    context.Database.ExecuteSqlRaw(command);
                    context.SeedingEntries.Add(new SeedingEntry() { Name = file.LogicalFile });
                    context.SaveChanges();
                    transaction.Commit();
                }
                catch
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }
    }
}
