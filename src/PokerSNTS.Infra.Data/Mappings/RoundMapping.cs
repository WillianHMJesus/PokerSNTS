using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class RoundMapping : IEntityTypeConfiguration<Round>
    {
        public void Configure(EntityTypeBuilder<Round> builder)
        {
            builder.ToTable("Rounds");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.Property(x => x.Date)
                .HasColumnType("date");

            builder.HasMany(x => x.PlayersRounds)
                .WithOne(x => x.Round)
                .HasForeignKey(x => x.RoundId);

            builder.HasMany(x => x.Ranking)
                .WithMany(x => x.Rounds)
                .UsingEntity(x => x.ToTable("RankingRounds"));

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
