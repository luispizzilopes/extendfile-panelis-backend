using ExtendFile.Panelis.BuildingBlocks.Pagination;
using Microsoft.EntityFrameworkCore;

namespace ExtendFile.Panelis.Infrastructure.Extensions;

public static class QueryableExtension
{
    public static async Task<PaginedResult<T>> PaginationAsync<T>
    (this IQueryable<T> query,
        PaginationParams parameter,
        CancellationToken cancellationToken = default)
    {
        int totalRecords = await query.CountAsync(cancellationToken);

        var data = await query
            .Skip((parameter.PageNumber - 1) * parameter.FinalSize)
            .Take(parameter.FinalSize)
            .ToListAsync(cancellationToken);

        return new PaginedResult<T>
        {
            Data = data,
            TotalRecords = totalRecords,
            PageNumber = parameter.PageNumber,
            PageSize = parameter.PageSize
        };
    }
}