namespace InfiCare.Domain.Entities;

public class Transaction
{
    public long TransactionId { get; set; }
    public DateTime Created { get; set; }
    public required string UserId { get; set; }
    public required string Status { get; set; }
    public required string Remark { get; set; }
    public required string Sender { get; set; }
    public required string Receiver { get; set; }
    public required string PaymentDetail { get; set; }
}

public class PersonDetails
{
    public required string FirstName { get; set; }
    public string? MiddleName { get; set; }
    public required string LastName { get; set; }
    public required string Address { get; set; }
    public required string Country { get; set; }
}

public class PaymentDetails
{
    public required string BankName { get; set; }
    public required string AccountNumber { get; set; }
    public decimal TransferAmount { get; set; }
    public decimal ExchangeRate { get; set; }
    public decimal PayoutAmount { get; set; }
}