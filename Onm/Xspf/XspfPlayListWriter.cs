namespace Onm.Xspf;

using System.Xml.Serialization;
using System.Xml;
using System.Text;

public static class XspfPlaylistWriter
{
	public static void Write(string path, XspfPlaylist playlist)
	{
        var serializer = new XmlSerializer(typeof(XspfPlaylist));
        var ns = new XmlSerializerNamespaces();
        ns.Add("", "http://xspf.org/ns/0/");
        var se = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  ",
            Encoding = Encoding.UTF8
        };

        using (XmlWriter writer = XmlWriter.Create(path, se))
        {
            serializer.Serialize(writer, playlist, ns);
        }
	}
}
