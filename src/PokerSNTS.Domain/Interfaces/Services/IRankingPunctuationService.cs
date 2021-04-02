using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingPunctuationService
    {
        Task<bool> Add(RankingPunctuation rankingPunctuation);
        Task<bool> Update(Guid id, RankingPunctuation rankingPunctuation);
        Task<IEnumerable<RankingPunctuation>> GetAll();
        Task<RankingPunctuation> GetRankingPunctuationByPosition(short position);
    }
}
