using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class PlayerRoundMapping : IEntityTypeConfiguration<PlayerRound>
    {
        public void Configure(EntityTypeBuilder<PlayerRound> builder)
        {
            builder.ToTable("PlayersRounds");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.PlayersRounds)
                .HasForeignKey(x => x.PlayerId);

            builder.HasOne(x => x.Round)
                .WithMany(x => x.PlayersRounds)
                .HasForeignKey(x => x.RoundId);

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.IsValid);
        }
    }
}
