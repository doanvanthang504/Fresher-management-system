using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Global.Shared.Commons
{
    public static class PaginationExtension
    {
        public static async Task<Pagination<TEntity>> PaginateAsync<TEntity>(
            this IQueryable<TEntity> query, int pageIndex, int pageSize) where TEntity : class
        {
            if (pageIndex < 0 || pageSize <= 0)
                throw new ArgumentException();
            if (query == null)
                throw new ArgumentNullException(nameof(query));

            var totalItemsCountTask = query
                                        .AsNoTracking()
                                        .DeferredCount()
                                        .FutureValue()
                                        .ValueAsync();

            var itemsTask = query
                                .AsNoTracking()
                                .Skip(pageIndex * pageSize)
                                .Take(pageSize)
                                .Future()
                                .ToListAsync();

            await Task.WhenAll(totalItemsCountTask, itemsTask);

            var result = new Pagination<TEntity>()
            {
                PageIndex = pageIndex,
                PageSize = pageSize,
                TotalItemsCount = await totalItemsCountTask,
                Items = await itemsTask,
            };

            return result;
        }
    }
}
