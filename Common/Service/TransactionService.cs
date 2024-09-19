using InfiCare.Common.Interface;
using InfiCare.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol;

namespace InfiCare.Common.Service;

public class TransactionService : ITransactionServices
{
    private readonly ILogger _logger;
    private readonly ApplicationDbContext _context;
    public TransactionService(
        ILogger<TransactionService> logger,
        ApplicationDbContext context)
    {
        _logger = logger;
        _context = context;
    }

    public TransactionListDto GetTransactionList(string userId, DateQueryParam date, PaginationQueryParams pagination)
    {
        try
        {
            var query = _context.Transactions
                                .AsNoTracking()
                                .Where(e => e.UserId == userId);

            if (date is not null)
                query = query.Where(e => e.Created >= date.StartDate && e.Created <= date.EndDate.Value.AddDays(1));

            query = query.OrderByDescending(e => e.Created);

            var res = query.Paginate(pagination.Page, pagination.PageSize);

            TransactionListDto result = new()
            {
                PageNumber = pagination.Page,
                PageSize = pagination.PageSize,
                TotalPages = res.TotalPages,
                Data = res.Data.Select(TransactionDetailDto.FromTransactionEntity)
            };
            return result;
        }
        catch (Exception)
        {

            throw;
        }
    }
}
