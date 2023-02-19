namespace WalletApi.Repository
{
    public interface IUnitOfWork
    {
        IWalletRepository WalletRepository { get; }
        IOperationRepository OperationRepository { get; }
        Task Commit();
    }
}
