using InfiCare.Common.Interface;

namespace InfiCare.Models;

public class TransactionHistoryViewModel
{
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<TransactionDetailDto>? Data { get; set; }

    public static TransactionHistoryViewModel FromTransactionListDto(TransactionListDto dto)
        => new()
        {
            PageNumber = dto.PageNumber,
            PageSize = dto.PageSize,
            TotalPages = dto.TotalPages,
            Data = dto.Data
        };
}
