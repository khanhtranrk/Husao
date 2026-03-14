namespace Onm;

public interface IOnmPlaylist
{
    string Id { get; set; }
    string Title { get; set; }
    string Description { get; set; }
    string Image { get; set; }
    string CoverImage { get; set; }
    string Creator { get; set; }
    DateOnly ReleaseDate { get; set; }
    string Source { get; set; }
    List<IOnmMeta> Meta { get; set; }
    List<IOnmMedia> Media { get; set; }
}
