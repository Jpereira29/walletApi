using System.Linq.Expressions;
using WalletApi.Models;

namespace WalletApi.Repository
{
    public interface IWalletRepository : IRepository<Wallet>
    {
        Task<Wallet> GetWalletWithOperation(Expression<Func<Wallet, bool>> predicate);
    }
}
