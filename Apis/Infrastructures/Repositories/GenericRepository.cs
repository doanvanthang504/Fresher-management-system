using Application.Interfaces;
using Application.Repositories;
using Domain.Entities;
using Global.Shared.Commons;
using Infrastructures.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Infrastructures.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity
    {
        protected DbSet<TEntity> _dbSet;
        private readonly ICurrentTime _timeService;
        private readonly IClaimsService _claimsService;

        public GenericRepository(AppDbContext context, ICurrentTime timeService, IClaimsService claimsService)
        {
            _dbSet = context.Set<TEntity>();
            _timeService = timeService;
            _claimsService = claimsService;
        }

        public async Task<IList<TEntity>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task AddAsync(TEntity entity)
        {
            entity.CreationDate = _timeService.GetCurrentTime();
            entity.CreatedBy = _claimsService.CurrentUserId;
            await _dbSet.AddAsync(entity);
        }
        
        public void SoftRemove(TEntity entity)
        {
            entity.IsDeleted = true;
            entity.DeleteBy = _claimsService.CurrentUserId;
            _dbSet.Update(entity);
        }

        public void Update(TEntity entity)
        {
            entity.ModificationDate = _timeService.GetCurrentTime();
            entity.ModificationBy = _claimsService.CurrentUserId;
            _dbSet.Update(entity);
        }

        public async Task AddRangeAsync(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.CurrentUserId;
            }
            await _dbSet.AddRangeAsync(entities);
        }

        public void SoftRemoveRange(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.IsDeleted = true;
                entity.DeletionDate = _timeService.GetCurrentTime();
                entity.DeleteBy = _claimsService.CurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }

        public Task<Pagination<TEntity>> ToPagination(int pageIndex = 0, int pageSize = 10)
        {
            return _dbSet.PaginateAsync(pageIndex, pageSize);
        }

        public void UpdateRange(ICollection<TEntity> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreationDate = _timeService.GetCurrentTime();
                entity.CreatedBy = _claimsService.CurrentUserId;
            }
            _dbSet.UpdateRange(entities);
        }

        public Task<Pagination<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            SortingConditionQueue<TEntity>? sortConditions = null,
            int pageIndex = 0, int pageSize = 10)
        {
            return _dbSet
                    .WhereIfNotNull(predicate)
                    .OrderBy(sortConditions)
                    .PaginateAsync(pageIndex, pageSize);
        }
        public Task<Pagination<TEntity>> FindAsync(
        Expression<Func<TEntity, bool>>? predicate = null,
        SortingConditionQueue<TEntity>? sortConditions = null,
        int pageIndex = 0, int pageSize = 10,
        params Expression<Func<TEntity, object?>>[] includes
        )
        {
            IQueryable<TEntity> query = _dbSet.WhereIfNotNull(predicate);

            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object?>> include in includes)
                    query = query.Include(include);
            }
            return query.OrderBy(sortConditions)
                        .PaginateAsync(pageIndex, pageSize);
        }

        public async Task<IList<TEntity>> FindAsync(
            Expression<Func<TEntity, bool>>? predicate = null,
            SortingConditionQueue<TEntity>? sortConditions = null)
        {
            return await _dbSet
                            .WhereIfNotNull(predicate)
                            .OrderBy(sortConditions)
                            .ToListAsync();
        }

        public async Task<TEntity?> GetByIdAsync(Guid id, params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;

            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object?>> include in includes)
                    query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync();
        }


        public async Task<bool> ExistAnyAsync(Expression<Func<TEntity, bool>> expression)
        {
            return await _dbSet.AnyAsync(expression);
        }

        //Find list item with include,expression, sort
        public async Task<IList<TEntity>> FindAsync(
           Expression<Func<TEntity, bool>>? predicate = null,
           SortingConditionQueue<TEntity>? sortConditions = null,
           params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object?>> include in includes)
                    query = query.Include(include);
            }
            return await query
                        .WhereIfNotNull(predicate)
                        .OrderBy(sortConditions)
                        .ToListAsync();
        }

        public async Task<TEntity?> FindAsync(
           Guid id,
           params Expression<Func<TEntity, object?>>[] includes)
        {
            IQueryable<TEntity> query = _dbSet;
            query = query.Where(x => x.Id == id);
            if (includes != null)
            {
                foreach (Expression<Func<TEntity, object?>> include in includes)
                    query = query.Include(include);
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
