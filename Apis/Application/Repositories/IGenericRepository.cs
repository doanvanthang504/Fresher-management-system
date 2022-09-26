using Domain.Entities;
using Global.Shared.Commons;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        Task<IList<TEntity>> GetAllAsync();

        Task<TEntity?> GetByIdAsync(Guid id);

        Task AddAsync(TEntity entity);

        void Update(TEntity entity);

        void UpdateRange(ICollection<TEntity> entities);

        void SoftRemove(TEntity entity);

        Task AddRangeAsync(ICollection<TEntity> entities);

        void SoftRemoveRange(ICollection<TEntity> entities);
        Task<bool> ExistAnyAsync(Expression<Func<TEntity, bool>> expression);

        Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10);

        Task<Pagination<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            SortingConditionQueue<TEntity>? sortConditions = null,
            int pageIndex = 0, int pageSize = 10);

        public Task<IList<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            SortingConditionQueue<TEntity>? sortConditions = null);

        Task<IList<TEntity>> FindAsync(
           Expression<Func<TEntity, bool>>? predicate = null,
           SortingConditionQueue<TEntity>? sortConditions = null,
           params Expression<Func<TEntity, object?>>[] includes);

        Task<TEntity?> FindAsync(Guid id, params Expression<Func<TEntity, object?>>[] includes);

        Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object?>>[] includes);
    }
}
