namespace PokerSNTS.Domain.Interfaces.Repositories
{
    public interface IRepository<T>
    {
        void Add(T entity);
        void Update(T entity);
    }
}
