using PokerSNTS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task AddAsync(User user);
        Task UpdateAsync(Guid id, User user);
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(Guid id);
        Task<User> ValidateLoginAsync(string userName, string password);
    }
}
