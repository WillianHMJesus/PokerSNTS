using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingPunctuationService
    {
        Task<bool> AddAsync(RankingPunctuation rankingPunctuation);
        Task<bool> UpdateAsync(Guid id, RankingPunctuation rankingPunctuation);
        Task<IEnumerable<RankingPunctuation>> GetAllAsync();
        Task<RankingPunctuation> GetRankingPunctuationByPositionAsync(short position);
    }
}
