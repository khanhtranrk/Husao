namespace Onm;

public interface IOnmMedia
{
    string Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    string Image { get; set; }
    string Creator { get; set; }
    DateOnly ReleasseDate { get; set; }
    string Source { get; set; }
    List<IOnmMeta> Meta { get; set; }
}
