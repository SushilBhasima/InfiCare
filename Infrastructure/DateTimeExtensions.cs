using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InfiCare.Infrastructure;

public static class DateTimeExtensions
{
    public static PropertyBuilder<DateTime> UsesUtc(this PropertyBuilder<DateTime> builder)
        => builder.HasConversion(
            v => FromInternalToDbRepresentation(v),
            v => FromDbToInternalRepresentation(v)
            );

    private static DateTime FromInternalToDbRepresentation(this DateTime dateTime)
        => dateTime.Kind == DateTimeKind.Utc
            ? dateTime
            : DateTime.SpecifyKind(dateTime.ToUniversalTime(), DateTimeKind.Utc);

    private static DateTime FromDbToInternalRepresentation(this DateTime dateTime)
        => dateTime.Kind == DateTimeKind.Unspecified
            ? DateTime.SpecifyKind(dateTime, DateTimeKind.Utc)
            : dateTime.ToUniversalTime();
}
