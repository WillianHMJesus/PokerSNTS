using PokerSNTS.Domain.Entities;
using PokerSNTS.Domain.Interfaces.UnitOfWork;
using System;

namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRepository<T> : IUnitOfWork, IDisposable where T : Entity
    {
        void Add(T entity);
        void Update(T entity);
    }
}
