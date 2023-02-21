using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using WalletApi.Context;

namespace WalletApi.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected AppDbContext _context;

        public Repository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(T context)
        {
            _context.Set<T>().Add(context); 
        }

        public void Delete(T context)
        {
            _context.Set<T>().Remove(context);
        }

        public IQueryable<T> Get()
        {
            return _context.Set<T>().AsNoTracking();
        }

        public async Task<T> GetByCode(Expression<Func<T, bool>> predicate)
        {
            return await _context.Set<T>().SingleOrDefaultAsync(predicate);
        }

        public void Update(T context)
        {
            _context.Entry(context).State = EntityState.Modified;
            _context.Set<T>().Update(context);
        }
    }
}
