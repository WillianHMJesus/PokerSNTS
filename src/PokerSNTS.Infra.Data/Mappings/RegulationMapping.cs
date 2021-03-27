using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class RegulationMapping : IEntityTypeConfiguration<Regulation>
    {
        public void Configure(EntityTypeBuilder<Regulation> builder)
        {
            builder.ToTable("Regulations");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasColumnType("varchar")
                .HasMaxLength(8000);

            builder.Ignore(x => x.ValidationResult);
            builder.Ignore(x => x.IsValid);
        }
    }
}
