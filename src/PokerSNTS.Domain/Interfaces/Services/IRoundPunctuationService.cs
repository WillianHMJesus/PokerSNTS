using PokerSNTS.Domain.Entities;
using System;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundPunctuationService
    {
        Task<bool> Add(RoundPunctuation roundPunctuation);
        Task<bool> Update(Guid id, RoundPunctuation roundPunctuation);
    }
}
