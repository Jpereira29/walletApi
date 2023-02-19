using System.Linq.Expressions;

namespace WalletApi.Repository
{
    public interface IRepository<T>
    {
        IQueryable<T> Get();
        Task<T> GetByCode(Expression<Func<T, bool>> predicate);
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
