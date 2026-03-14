namespace Sii;

using HtmlAgilityPack;

public class SeasonFetcher
{
    public async Task<Season> FetchSeasonAsync(string url)
    {
        string html = await Http.Client.GetStringAsync(url);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var title = doc.DocumentNode
            .SelectSingleNode("//h1[contains(@class, 'Title')]")
            ?.InnerText ?? "";

        var otherTitles = doc.DocumentNode
            .SelectSingleNode("//h2[contains(@class, 'SubTitle')]")
            ?.InnerText ?? "";

        var description = doc.DocumentNode
            .SelectSingleNode("//div[contains(@class, 'Description')]")
            ?.InnerText.Trim() ?? "";

        var imageUrl = doc.DocumentNode
            .SelectSingleNode("//div[contains(@class, 'Image')]//img")
            ?.GetAttributeValue("src", "") ?? "";

        var coverImageUrl = doc.DocumentNode
            .SelectSingleNode("//img[contains(@class, 'TPostBg')]")
            ?.GetAttributeValue("src", "") ?? "";

        var date = doc.DocumentNode
            .SelectSingleNode("//span[contains(concat(' ', normalize-space(@class), ' '), ' Date ')]//a")
            ?.InnerText ?? "";

        var score = doc.DocumentNode
            .SelectSingleNode("//strong[contains(@id, 'average_score')]")
            ?.InnerText ?? "";

        var studio = doc.DocumentNode
            .SelectSingleNode("//li[contains(@class,'AAIco-adjust') and strong[contains(normalize-space(.),'Studio')]]/a")
            ?.InnerText.Trim() ?? "";

        var episodes_url = doc.DocumentNode
            .SelectSingleNode("//a[contains(@class, 'watch_button_more')]")
            ?.GetAttributeValue("href", "") ?? "";

        var episodes = episodes_url != ""
            ? await new EpisodeFetcher().FetchEpisodesAsync(episodes_url)
            : Enumerable.Empty<Episode>();

        return new Season
        {
            Id = UrlParser.ParseSeasonId(url),
            Title = title,
            OtherTitles = otherTitles,
            Description = description,
            ReleaseDate = date,
            Studio = studio,
            Score = score,
            Url = url,
            ImageUrl = imageUrl,
            CoverImageUrl = coverImageUrl,
            Episodes = episodes
        };
    }

    public async Task<IEnumerable<Season>> FetchSeasonsAsync(string url)
    {
        var seasons = new List<Season>();

        var html = await Http.Client.GetStringAsync(url);
        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var posts = doc.DocumentNode
            .SelectNodes("//*[contains(@class, 'TPostMv')]")
            ?? Enumerable.Empty<HtmlNode>();

        foreach (var post in posts)
        {
            seasons.Add(new Season
            {
                Id = UrlParser.ParseSeasonId(post.SelectSingleNode(".//a")?.GetAttributeValue("href", "") ?? ""),
                Title = post.SelectSingleNode(".//h2[contains(@class, 'Title')]")?.InnerText ?? "",
                OtherTitles = "",
                Description = post.SelectSingleNode(".//div[contains(@class, 'Description')]//p")?.InnerText.Trim() ?? "",
                Url = post.SelectSingleNode(".//a")?.GetAttributeValue("href", "") ?? "",
                ImageUrl = post.SelectSingleNode(".//img")?.GetAttributeValue("src", "") ?? "",
                CoverImageUrl = "",
                Studio = "",
                ReleaseDate = post.SelectSingleNode(".//span[contains(concat(' ', normalize-space(@class), ' '), ' Date ')]")?.InnerText ?? "",
                Score = post.SelectSingleNode(".//div[contains(@class, 'anime-avg-user-rating')]")?.InnerText ?? "",
                Episodes = Enumerable.Empty<Episode>()
            });
        }

        return seasons;
    }
}
