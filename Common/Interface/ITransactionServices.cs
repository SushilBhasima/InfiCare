using InfiCare.Domain.Entities;
using System.Text.Json;

namespace InfiCare.Common.Interface;

public interface ITransactionServices
{
    TransactionListDto GetTransactionList(string userId, DateQueryParam? date, PaginationQueryParams pagination);
}

public class TransactionListDto
{
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public int TotalPages { get; set; }
    public IEnumerable<TransactionDetailDto>? Data { get; set; }
}

public record TransactionDetailDto
{
    public long TransactionId { get; set; }
    public required PersonDetails Receiver { get; set; }
    public required PaymentDetails Payment { get; set; }
    public DateTime Created { get; set; }
    public required string Status { get; set; }
    public string? Remarks { get; set; }

    public static TransactionDetailDto FromTransactionEntity(Transaction dto)
        => new()
        {
            TransactionId = dto.TransactionId,
            Created = dto.Created,
            Status = dto.Status,
            Remarks = dto.Remark,
            Receiver = PersonDetailsService.GetPersonDetails(dto.Receiver),
            Payment = PaymentDetailsService.GetPaymentDetails(dto.PaymentDetail)
        };

}
public static class PersonDetailsService
{

    public static PersonDetails GetPersonDetails(string json)
    {
        try
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            var personDetails = JsonSerializer.Deserialize<PersonDetails>(json, options);
            return personDetails;
        }
        catch (JsonException)
        {
            // Handle the exception
            throw;
        }
    }
}

public static class PaymentDetailsService
{

    public static PaymentDetails GetPaymentDetails(string json)
    {
        try
        {
            JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
            var payment = JsonSerializer.Deserialize<PaymentDetails>(json, options);
            return payment;
        }
        catch (JsonException)
        {
            // Handle the exception
            throw;
        }
    }
}
