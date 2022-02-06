using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GringottsBank.Data.Repositories
{
    public class  Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private readonly GringottsBankDbContext _context;
        private readonly DbSet<TEntity> _dbSet;

        public Repository(GringottsBankDbContext context)
        {
            _context = context;
            _dbSet = _context.Set<TEntity>();
        }
        public async Task Add(TEntity entity)
        {
             await _dbSet.AddAsync(entity);
             await _context.SaveChangesAsync();
        }

        public async Task Update(TEntity entity)
        {
             _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(TEntity entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public IQueryable<TEntity> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> @where)
        {
            return _dbSet.Where(where);
        }

     
        
    }
}