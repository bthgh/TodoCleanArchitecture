﻿using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace TodoCleanArchitecture.Application.Models.Common;


public class PaginatedList<T>
{
    public IReadOnlyCollection<T> Items { get; }
    public int PageNumber { get; }
    public int TotalPages { get; }
    public int TotalCount { get; }
 

    [JsonConstructor]
    public PaginatedList(IReadOnlyCollection<T> items, int totalCount, int totalPages, int pageNumber, bool hasPreviousPage, bool hasNextPage)
    {
        PageNumber = pageNumber;
        TotalPages = totalPages;
        TotalCount = totalCount;
        Items = items;

    }
    
    public PaginatedList(IReadOnlyCollection<T> items, int totalCount, int pageNumber, int pageSize)
    {
        PageNumber = pageNumber;
        TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);
        TotalCount = totalCount;
        Items = items;
    }

    public bool HasPreviousPage => PageNumber > 1;

    public bool HasNextPage => PageNumber < TotalPages;

    public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageNumber, int pageSize)
    {
        var count = await source.CountAsync();
        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return new PaginatedList<T>(items, count, pageNumber, pageSize);
    }
    
}