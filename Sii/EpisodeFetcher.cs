namespace Sii;

using HtmlAgilityPack;

using System.Text.RegularExpressions;

public class EpisodeFetcher
{
	public async Task<Episode> FetchEpisodeAsync(string url)
	{
        string html = await Http.Client.GetStringAsync(url);

		var match = Regex.Match(html, @"filmID = parseInt\(\'(?<seasonId>\d+)\'\)");

		if (!match.Success)
		{
			throw new SiiException("Could not detect Season Id");
		}

		var seasonId = match.Groups["seasonId"].Value;

		var doc = new HtmlDocument();
		doc.LoadHtml(html);

		var playingNode = doc.DocumentNode
			.SelectSingleNode("//a[@data-movie='playing']");

		if (playingNode is null)
		{
			throw new SiiException("Could not detect Episode info");
		}

		var episodeId = playingNode.GetAttributeValue("data-id", "");
		var episodeTitle = playingNode.GetAttributeValue("title", "");
		var episodeUrl = playingNode.GetAttributeValue("href", "");
		var episodeToken = playingNode.GetAttributeValue("data-hash", "");

        return new Episode
		{
			Id = episodeId,
			SeasonId = seasonId,
			Title = episodeTitle,
			Url = episodeUrl,
			Token = episodeToken
		};
	}

	public async Task<IEnumerable<Episode>> FetchEpisodesAsync(string url)
    {
        string html = await Http.Client.GetStringAsync(url);

		var match = Regex.Match(html, @"filmID = parseInt\(\'(?<seasonId>\d+)\'\)");

		if (!match.Success)
		{
			throw new SiiException("Could not detect Season Id");
		}

		var seasonId = match.Groups["seasonId"].Value;

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var episodeListNode = doc.DocumentNode
            .SelectSingleNode("//ul[contains(concat(' ', normalize-space(@class), ' '), ' list-episode ')]");

        if (episodeListNode is null)
        {
            return Enumerable.Empty<Episode>();
        }

        var nodes = episodeListNode
            .SelectNodes(".//a[contains(concat(' ', normalize-space(@class), ' '), ' episode-link ')]");

        if (nodes is null)
        {
            return Enumerable.Empty<Episode>();
        }

        var episodes = new List<Episode>();

        foreach (var node in nodes)
        {
			var episodeId = node.GetAttributeValue("data-id", "");
			var episodeTitle = node.GetAttributeValue("title", "");
			var episodeUrl = node.GetAttributeValue("href", "");
			var episodeToken = node.GetAttributeValue("data-hash", "");

            episodes.Add(new Episode
            {
                Id = episodeId,
				SeasonId = seasonId,
                Title = episodeTitle,
                Url = episodeUrl,
                Token = episodeToken
            });
        }

        return episodes;
    }

}
