using AutoMapper;
using AutoMapper.QueryableExtensions;
using Application.Models;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Mappings;
public static class MappingExtensions
{
    public static Task<PaginatedList<TDestination>> PaginatedListAsync<TDestination>(this IQueryable<TDestination> queryable, int pageNumber, int pageSize) where TDestination : class
        => PaginatedList<TDestination>.CreateAsync(queryable.AsNoTracking(), pageNumber, pageSize);

    public static Task<List<TDestination>> ProjectToListAsync<TDestination>(this IQueryable queryable, AutoMapper.IConfigurationProvider configuration) where TDestination : class
        => queryable.ProjectTo<TDestination>(configuration).AsNoTracking().ToListAsync();


    public static IQueryable<TDestination> Filter<TDestination>(this IQueryable<TDestination> queryable, List<FilterListItem> filters) where TDestination : class
        => DynamicFilter.ApplyFilter(queryable, filters);

    public static IQueryable<TDestination> Sort<TDestination>(this IQueryable<TDestination> queryable, SortItem sortBy) where TDestination : class
       => DynamicFilter.ApplySort(queryable, sortBy);

}
