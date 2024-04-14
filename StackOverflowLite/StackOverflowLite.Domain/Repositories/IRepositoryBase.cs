using System;
using StackOverflowLite.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace StackOverflowLite.Domain.Repositories
{
    public interface IRepositoryBase<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
        where TKey : IComparable
    {
        void Add(TEntity entity);
        Task AddAsync(TEntity entity);
        void Edit(TEntity entityToUpdate);
        Task EditAsync(TEntity entityToUpdate);
        IList<TEntity> GetAll();
        Task<IList<TEntity>> GetAllAsync();
        TEntity GetById(TKey id);
        Task<TEntity> GetByIdAsync(TKey id);
        int GetCount(Expression<Func<TEntity, bool>> filter = null);
        Task<int> GetCountAsync(Expression<Func<TEntity, bool>> filter = null);
        void Remove(Expression<Func<TEntity, bool>> filter);
        void Remove(TEntity entityToDelete);
        void Remove(TKey id);
        Task RemoveAsync(Expression<Func<TEntity, bool>> filter);
        Task RemoveAsync(TEntity entityToDelete);
        Task RemoveAsync(TKey id);

    }
}