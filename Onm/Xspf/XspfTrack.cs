namespace Onm.Xspf;

using System.Xml.Serialization;

public class XspfTrack
{
    [XmlElement("location")]
    public required string Location { get; init; }

    [XmlElement("title")]
    public required string Title { get; init; }

    [XmlElement("creator")]
    public required string Creator { get; init; }

    [XmlElement("album")]
    public required string Album { get; init; }

    [XmlElement("image")]
    public required string Image { get; init; }
}
