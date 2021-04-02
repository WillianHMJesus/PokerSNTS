using System.Threading.Tasks;

namespace PokerSNTS.Domain.Interfaces.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<bool> CommitAsync();
    }
}
