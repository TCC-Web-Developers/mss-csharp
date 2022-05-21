using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using StarterAPI.Commons.SharedModels;

namespace StarterAPI.Commons.Extensions
{

    public static class EFCoreQueryExtensions
    {
        public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, AutoMapper.IConfigurationProvider configuration)
           => queryable.ProjectTo<TDestination>(configuration).ToListAsync();

        public static async Task<PaginatedResult<IEnumerable<TOutput>>> ToPaginatedQueryResultAsync<TEntity, TOutput>(this IQueryable<TEntity> query, 
            AutoMapper.IConfigurationProvider configuration,
            int page, int perPage)
        {
            int totalCount = await query.CountAsync();


            IEnumerable<TOutput> data = await query
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ProjectTo<TOutput>(configuration)
                .ToListAsync();

            return new PaginatedResult<IEnumerable<TOutput>>(data, new PagingQuery { page = page, perPage = perPage }, totalCount);
        }

        public static async Task<PaginatedResult<IEnumerable<TOutput>>> ToPaginatedQueryResultAsync<TEntity, TOutput>(this IQueryable<TEntity> query,
            AutoMapper.IConfigurationProvider configuration,
            int page, int perPage, Func<IQueryable<TEntity>, IQueryable<TEntity>> DelegatedMethod )
        {
            int totalCount = await query.CountAsync();

            var changedQuery = DelegatedMethod(query);

            IEnumerable<TOutput> data = await changedQuery
                .Skip((page - 1) * perPage)
                .Take(perPage)
                .ProjectTo<TOutput>(configuration)
                .ToListAsync();

            return new PaginatedResult<IEnumerable<TOutput>>(data, new PagingQuery { page = page, perPage = perPage }, totalCount);
        }


    }
}
