using System;
using System.Collections.Generic;
using System.Linq;

namespace PokerSNTS.Domain.DTOs
{
    public class RankingOverallDTO
    {
        public RankingOverallDTO()
        {
            Punctuations = new List<PunctuationOverallDTO>();
        }

        public string Name { get; set; }
        public decimal PunctuationSum { get => Punctuations.Sum(x => x.Punctuation); }
        public ICollection<PunctuationOverallDTO> Punctuations { get; set; }
    }
}
