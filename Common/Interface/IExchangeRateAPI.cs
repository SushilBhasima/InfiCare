using Microsoft.AspNetCore.Mvc;
using Refit;

namespace InfiCare.Common.Interface;

public interface IExchangeRateAPI
{
    [Get("/api/forex/v1/rates")]
    Task<ApiResponse<ForexResponse>> GetExchangeRateData([Query] int page, [Query] int per_page, [Query] string from, [Query] string to);
}

public class ForexResponse
{
    public int Page { get; set; }
    public int PerPage { get; set; }
    public List<ForexRate>? Data { get; set; }
}
public class ForexRate
{
    public string? Currency { get; set; }
    public decimal Buy { get; set; }
    public decimal Sell { get; set; }
    public string? Date { get; set; }
}
