using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;

namespace PokerSNTS.Domain.Adapters
{
    public class RoundPunctuationAdapter
    {
        public static RoundPunctuationDTO ToRoundPunctuationDTO(RoundPunctuation roundPunctuation)
        {
            return new RoundPunctuationDTO(roundPunctuation.Id, roundPunctuation.Position, roundPunctuation.Punctuation, roundPunctuation.PlayerId, roundPunctuation.RoundId);
        }
    }
}
