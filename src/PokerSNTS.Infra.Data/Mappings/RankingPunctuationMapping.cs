using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class RankingPointMapping : IEntityTypeConfiguration<RankingPoint>
    {
        public void Configure(EntityTypeBuilder<RankingPoint> builder)
        {
            builder.ToTable("RankingPoints");
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
