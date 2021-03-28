using PokerSNTS.Domain.DTOs;
using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRankingService
    {
        Task<bool> Add(Ranking ranking);
        Task<bool> Update(Guid id, Ranking ranking);
        Task<IEnumerable<RankingDTO>> GetAll();
    }
}
