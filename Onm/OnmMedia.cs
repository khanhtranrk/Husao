namespace Onm;

public class OnmMedia : IOnmMedia
{
    public required string Id { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public required string Image { get; set; }
    public required string Creator { get; set; }
    public required DateOnly ReleasseDate { get; set; }
    public required string Source { get; set; }
    public required List<IOnmMeta> Meta { get; set; }
}
