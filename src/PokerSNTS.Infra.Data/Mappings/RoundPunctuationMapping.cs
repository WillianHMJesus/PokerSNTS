using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class RoundPointMapping : IEntityTypeConfiguration<RoundPoint>
    {
        public void Configure(EntityTypeBuilder<RoundPoint> builder)
        {
            builder.ToTable("RoundsPoints");
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.Player)
                .WithMany(x => x.RoundsPoints)
                .HasForeignKey(x => x.PlayerId);

            builder.HasOne(x => x.Round)
                .WithMany(x => x.RoundsPoints)
                .HasForeignKey(x => x.RoundId);

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
