using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class RoundPunctuationMapping : IEntityTypeConfiguration<RoundPunctuation>
    {
        public void Configure(EntityTypeBuilder<RoundPunctuation> builder)
        {
            builder.ToTable("RoundsPunctuations");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.RoundsPunctuations)
                .HasForeignKey(x => x.PlayerId);

            builder.HasOne(x => x.Round)
                .WithMany(x => x.RoundsPunctuations)
                .HasForeignKey(x => x.RoundId);

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
