using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingPunctuationDTO
    {
        public RankingPunctuationDTO(Guid id, short position, short punctuation)
        {
            Id = id;
            Position = position;
            Punctuation = punctuation;
        }

        public Guid Id { get; private set; }
        public short Position { get; private set; }
        public short Punctuation { get; private set; }
    }
}
