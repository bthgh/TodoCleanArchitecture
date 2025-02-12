 
using AutoMapper;
using TodoCleanArchitecture.Application.Models.Common;

namespace TodoCleanArchitecture.Application.Extensions;

public static class IMapperExtensions
{
    public static PaginatedList<TDestination> Map<TSource, TDestination>(
         this IMapper mapper,
         PaginatedList<TSource> source)
    {
        var items = mapper.Map<IReadOnlyCollection<TDestination>>(source.Items);
        return new PaginatedList<TDestination>(items, source.TotalCount, source.PageNumber, source.Items.Count);
    }

}
