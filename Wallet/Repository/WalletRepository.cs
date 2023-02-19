using WalletApi.Context;
using WalletApi.Models;

namespace WalletApi.Repository
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }
    }
}
