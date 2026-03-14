namespace Onm.Xspf;

using System.Xml.Serialization;
using Onm;

[XmlRoot("playlist", Namespace = "http://xspf.org/ns/0/")]
public class XspfPlaylist
{
    [XmlAttribute("version")]
    public string Version { get; init; } = "1";

    [XmlElement("title")]
    public required string Title { get; init; }

    [XmlElement("annotation")]
    public required string Annotation { get; init; }

    [XmlElement("creator")]
    public required string Creator { get; init; }

    [XmlElement("image")]
    public required string Image { get; init; }

    [XmlArray("trackList")]
    [XmlArrayItem("track")]
    public required List<XspfTrack> TrackList { get; init; }

    public static XspfPlaylist From(IOnmPlaylist playlist)
    {
        return new XspfPlaylist{
            Title = playlist.Title,
            Annotation = playlist.Description,
            Creator = playlist.Creator,
            Image = playlist.CoverImage,
            TrackList = playlist.Media.Select(m => new XspfTrack {
                Title = m.Title,
                Location = m.Source,
                Creator = m.Creator,
                Album = playlist.Title,
                Image = m.Image
            }).ToList()
        };
    }
}
