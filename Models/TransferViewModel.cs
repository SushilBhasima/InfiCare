using InfiCare.Domain.Entities;

namespace InfiCare.Models;

public class TransferViewModel
{
    public required PersonDetails Sender { get; set; }
    public required PersonDetails Receiver { get; set; }
    public required PaymentDetails PaymentDetails { get; set; }
    public required string Remarks { get; set; }
}
