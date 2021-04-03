using System;

namespace PokerSNTS.Domain.DTOs
{
    public class RoundPunctuationDTO
    {
        public RoundPunctuationDTO(Guid id, short position, short punctuation, Guid playerId, Guid roundId)
        {
            Id = id;
            Position = position;
            Punctuation = punctuation;
            PlayerId = playerId;
            RoundId = roundId;
        }

        public Guid Id { get; private set; }
        public short Position { get; private set; }
        public short Punctuation { get; private set; }
        public Guid PlayerId { get; private set; }
        public Guid RoundId { get; private set; }
    }
}
