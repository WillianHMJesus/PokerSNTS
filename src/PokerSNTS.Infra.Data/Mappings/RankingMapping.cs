using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class RankingMapping : IEntityTypeConfiguration<Ranking>
    {
        public void Configure(EntityTypeBuilder<Ranking> builder)
        {
            builder.ToTable("Ranking");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(100);

            builder.HasMany(x => x.Rounds)
                .WithOne(x => x.Ranking)
                .HasForeignKey(x => x.RankingId);

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
