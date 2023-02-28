using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Linq;
using System.Linq.Expressions;
using WalletApi.Context;
using WalletApi.Models;

namespace WalletApi.Repository
{
    public class WalletRepository : Repository<Wallet>, IWalletRepository
    {
        public WalletRepository(AppDbContext context) : base(context)
        {
        }

        public async Task<Wallet> GetWalletWithOperation(Expression<Func<Wallet, bool>> predicate)
        {
            return await Get().Include(p => p.Operations.OrderByDescending(p => p.Date)).FirstOrDefaultAsync(predicate);
        }
    }
}
