using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GringottsBank.Data.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task Add(TEntity entity);
        Task Update(TEntity entity);
        Task Delete(TEntity entity);
        IQueryable<TEntity> GetAll();
        IQueryable<TEntity> Get(Expression<Func<TEntity, bool>> where);
    }

}