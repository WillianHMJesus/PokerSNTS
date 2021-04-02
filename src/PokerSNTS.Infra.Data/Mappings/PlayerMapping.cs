using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class PlayerMapping : IEntityTypeConfiguration<Player>
    {
        public void Configure(EntityTypeBuilder<Player> builder)
        {
            builder.ToTable("Players");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(50);

            builder.HasMany(x => x.RoundsPunctuations)
                .WithOne(x => x.Player)
                .HasForeignKey(x => x.PlayerId);

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
