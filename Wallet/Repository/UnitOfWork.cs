using WalletApi.Context;

namespace WalletApi.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private IWalletRepository _walletRepository;
        private IOperationRepository _operationRepository;
        public AppDbContext _context;

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IWalletRepository WalletRepository { get { return _walletRepository ??= new WalletRepository(_context); } }
        public IOperationRepository OperationRepository { get { return _operationRepository ??= new OperationRepository(_context); } }


        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
