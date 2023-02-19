using WalletApi.Context;
using WalletApi.Models;

namespace WalletApi.Repository
{
    public class OperationRepository : Repository<Operation>, IOperationRepository
    {
        public OperationRepository(AppDbContext context) : base(context)
        {
        }
    }
}
