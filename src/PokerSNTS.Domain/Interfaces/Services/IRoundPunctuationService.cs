using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundPunctuationService
    {
        Task<RoundPunctuation> AddAsync(RoundPunctuation roundPunctuation);
        Task<RoundPunctuation> UpdateAsync(Guid id, RoundPunctuation roundPunctuation);
    }
}
