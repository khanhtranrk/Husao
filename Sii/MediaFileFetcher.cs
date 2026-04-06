namespace Sii;

using System.Text.Json;

public sealed class MediaFileFetcher
{
    public async Task<byte[]> FetchMediaFile(Episode episode)
    {
        var form = new FormUrlEncodedContent(new Dictionary<string, string>
		{
            ["link"] = episode.Token,
            ["id"] = episode.SeasonId
        });

		var domain = new Uri(episode.Url).Host;
        var api = $"https://{domain}/ajax/player";
        var request = new HttpRequestMessage(HttpMethod.Post, api)
		{
            Content = form
        };

        request.Headers.TryAddWithoutValidation("origin", $"https://{domain}");
        request.Headers.TryAddWithoutValidation("referer", episode.Url);

        var response = await Http.Client.SendAsync(request);
        var body = await response.Content.ReadAsStringAsync();
        var json = JsonDocument.Parse(body);

        Console.WriteLine(body.ToString());

        if (!json.RootElement.TryGetProperty("link", out var links) ||
            links.ValueKind is not JsonValueKind.Array ||
            links.GetArrayLength() == 0 ||
            !links[0].TryGetProperty("file", out var file))
        {
            throw new SiiException("Could not fetch media file - The response structure is invalid");
        }

        var content = file.GetBytesFromBase64();

        return content;
    }
}
