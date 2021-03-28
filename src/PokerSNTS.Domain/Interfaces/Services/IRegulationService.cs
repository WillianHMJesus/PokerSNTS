using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IRegulationService
    {
        Task<bool> Add(Regulation regulation);
        Task<bool> Update(Guid id, Regulation regulation);
        Task<IEnumerable<Regulation>> GetAll();
    }
}
