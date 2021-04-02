using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRoundService
    {
        Task<bool> Add(Round round);
        Task<bool> Update(Guid id, Round round);
        Task<IEnumerable<RoundDTO>> GetRoundByRankingId(Guid rankingId);
    }
}
