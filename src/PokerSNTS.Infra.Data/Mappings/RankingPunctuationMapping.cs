using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Infra.Data.Mappings
{
    public class RankingPunctuationMapping : IEntityTypeConfiguration<RankingPunctuation>
    {
        public void Configure(EntityTypeBuilder<RankingPunctuation> builder)
        {
            builder.ToTable("RankingPunctuations");
            builder.HasKey(x => x.Id);

            builder.Ignore(x => x.ValidationResult);
        }
    }
}
