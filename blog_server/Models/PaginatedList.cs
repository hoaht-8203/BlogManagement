using System;

namespace blog_server.Models;

public class PaginatedList<T>(List<T> items, int pageNumber, int pageSize, int totalRecords)
{
    public List<T> Items { get; set; } = items;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
    public int TotalRecords { get; set; } = totalRecords;
    public int TotalPages => (int)Math.Ceiling((double)TotalRecords / PageSize);
    public bool HasPrevious => PageNumber > 1;
    public bool HasNext => PageNumber < TotalPages;
}
