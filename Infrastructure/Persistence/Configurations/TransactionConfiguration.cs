using InfiCare.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiCare.Infrastructure.Persistence.Configurations;

public class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable(nameof(Transaction));

        builder.HasKey(e => e.TransactionId);

        builder.Property(e => e.Sender)
            .HasJsonConversion();

        builder.Property(e => e.Receiver)
            .HasJsonConversion();

        builder.Property(e => e.PaymentDetail)
            .HasJsonConversion();

        builder.Property(e => e.Created)
            .UsesUtc();
    }
}

