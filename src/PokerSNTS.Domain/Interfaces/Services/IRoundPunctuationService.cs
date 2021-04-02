using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundPunctuationService
    {
        Task<bool> AddAsync(RoundPunctuation roundPunctuation);
        Task<bool> UpdateAsync(Guid id, RoundPunctuation roundPunctuation);
    }
}
