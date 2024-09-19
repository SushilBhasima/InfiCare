using Microsoft.EntityFrameworkCore;

namespace InfiCare.Common;

public static class PaginationExtension
{
    public static PaginateResult<T> Paginate<T>(this IQueryable<T> query, int PageNumber, int PageSize)
    {
        var intermediate = query.Skip((PageNumber - 1) * PageSize).Take(PageSize);
        return new()
        {
            Page = PageNumber,
            PageSize = PageSize,
            TotalPages = query.Count(),
            Data = intermediate,
        };
    }
    public class PaginateResult<T>
    {
        public int Page { get; init; }
        public int PageSize { get; init; }
        public int TotalPages { get; init; }
        public required IQueryable<T> Data { get; init; }
    }

}

