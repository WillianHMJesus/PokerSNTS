using PokerSNTS.Domain.Entities;
using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingPunctuationDTO
    {
        public RankingPunctuationDTO(RankingPunctuation rankingPunctuation)
        {
            Id = rankingPunctuation.Id;
            Position = rankingPunctuation.Position;
            Punctuation = rankingPunctuation.Punctuation;
        }

        public Guid Id { get; private set; }
        public short Position { get; private set; }
        public short Punctuation { get; private set; }
    }
}
