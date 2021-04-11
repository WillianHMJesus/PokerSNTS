using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingPunctuationService
    {
        Task AddAsync(RankingPunctuation rankingPunctuation);
        Task UpdateAsync(Guid id, RankingPunctuation rankingPunctuation);
        Task<IEnumerable<RankingPunctuation>> GetAllAsync();
        Task<RankingPunctuation> GetByIdAsync(Guid id);
        Task<RankingPunctuation> GetByPositionAsync(short position);
    }
}
