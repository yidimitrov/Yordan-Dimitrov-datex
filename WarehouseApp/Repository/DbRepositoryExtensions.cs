using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace WarehouseWebApi.Repository
{
    public static class DbRepositoryExtensions
    {
        public static async Task<TResult> GetFirstOrDefaultAsync<TEntity, TResult>(this WarehouseDbContext context, Expression<Func<TEntity, bool>> filter, AutoMapper.IConfigurationProvider configuration, CancellationToken cancellationToken)
            where TEntity : class
            => await Get<TEntity, TResult>(context, filter, configuration).FirstOrDefaultAsync(cancellationToken);

        public static async Task<TResult[]> GetAllAsync<TEntity, TResult>(this WarehouseDbContext context, Expression<Func<TEntity, bool>> filter, AutoMapper.IConfigurationProvider configuration, CancellationToken cancellationToken)
            where TEntity : class
            => await Get<TEntity, TResult>(context, filter, configuration).ToArrayAsync(cancellationToken);

        public async static Task Delete<TEntity>(this WarehouseDbContext context, Expression<Func<TEntity, bool>> filter, CancellationToken cancellationToken)
            where TEntity : class
        {
            var items = context.Set<TEntity>().Where(filter);

            context.Set<TEntity>().RemoveRange(items);

            await context.SaveChangesAsync(cancellationToken);
        }

        private static IQueryable<TResult> Get<TEntity, TResult>(WarehouseDbContext context, Expression<Func<TEntity, bool>> filter, AutoMapper.IConfigurationProvider configuration)
            where TEntity : class
            => context.Set<TEntity>()
            .AsNoTracking()
            .Where(filter)
            .ProjectTo<TResult>(configuration);
    }
}
