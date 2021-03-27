using PokerSNTS.Domain.Entities;
using System;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IDisposable where T : Entity
    {
        void Add(T entity);
        void Update(T entity);
    }
}
