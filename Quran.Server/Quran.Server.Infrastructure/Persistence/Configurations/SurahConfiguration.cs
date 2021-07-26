using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Quran.Server.Domain.Entities;

namespace Quran.Server.Infrastructure.Persistence.Configurations
{
    public class SurahConfiguration:IEntityTypeConfiguration<Surah>
    {
        public void Configure(EntityTypeBuilder<Surah> builder)
        {
            builder
                .HasMany(x => x.Ayat)
                .WithOne(x => x.Surah)
                .HasForeignKey(x => x.SurahId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}