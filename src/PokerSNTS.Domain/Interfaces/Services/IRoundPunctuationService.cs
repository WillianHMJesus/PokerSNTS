using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundPunctuationService
    {
        Task AddAsync(RoundPunctuation roundPunctuation);
        Task UpdateAsync(Guid id, RoundPunctuation roundPunctuation);
        Task<IEnumerable<RoundPunctuation>> GetAllAsync();
        Task<RoundPunctuation> GetByIdAsync(Guid id);
    }
}
