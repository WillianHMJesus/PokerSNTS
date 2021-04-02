using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRegulationService
    {
        Task<bool> AddAsync(Regulation regulation);
        Task<bool> UpdateAsync(Guid id, Regulation regulation);
        Task<IEnumerable<Regulation>> GetAllAsync();
    }
}
