namespace Sii;

public class Season
{
    public required string Id { get; init; }
    public required string Title { get; init; }
    public required string OtherTitles { get; init; }
    public required string Description { get; init; }
    public required string ReleaseDate { get; init; }
    public required string Score { get; init; }
    public required string Studio { get; init; }
    public required string Url { get; init; }
    public required string ImageUrl { get; init; }
    public required string CoverImageUrl { get; init; }
    public required IEnumerable<Episode> Episodes { get; init; }
}
