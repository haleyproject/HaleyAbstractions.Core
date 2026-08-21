using System.Collections.Frozen;

namespace Haley.Models;

/// <summary>
/// Describes a one-based page request with the paging sizes supported by Haley consumers.
/// </summary>
/// <param name="Page">The requested one-based page number.</param>
/// <param name="PageSize">The requested number of items per page.</param>
/// <param name="Search">An optional search term interpreted by the consumer.</param>
public sealed record PageRequest(int Page = 1, int PageSize = 20, string? Search = null)
{
    public const int DefaultPageSize = 20;

    public static readonly IReadOnlySet<int> SupportedPageSizes =
        new[] { 10, DefaultPageSize, 40, 50 }.ToFrozenSet();

    public int NormalizedPage => Math.Max(1, Page);

    public int NormalizedPageSize =>
        SupportedPageSizes.Contains(PageSize) ? PageSize : DefaultPageSize;

    public int Offset => checked((NormalizedPage - 1) * NormalizedPageSize);
}

/// <summary>
/// Represents one page of a larger result set.
/// </summary>
public sealed record Page<T>(
    IReadOnlyCollection<T> Items,
    int PageNumber,
    int PageSize,
    long TotalCount)
{
    public long TotalPages =>
        TotalCount <= 0 || PageSize <= 0
            ? 0
            : ((TotalCount - 1) / PageSize) + 1;
}

/// <summary>
/// Represents an inclusive calendar-date range.
/// </summary>
public sealed record DateRange(DateOnly FromDate, DateOnly UntilDate)
{
    public const int DefaultMaximumDays = 3660;

    public bool IsValid(int maximumDays = DefaultMaximumDays) =>
        maximumDays >= 0 &&
        UntilDate >= FromDate &&
        UntilDate.DayNumber - FromDate.DayNumber <= maximumDays;
}
