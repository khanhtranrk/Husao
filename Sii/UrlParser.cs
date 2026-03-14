namespace Sii;

using System.Text.RegularExpressions;

public static class UrlParser
{
    public static string ParseSeasonId(string url)
    {
        var match = Regex.Match(url, @"^https:\/\/[^\/]+\/[A-Za-z]{4}\/[^\/\-][^\/]*\-a(?<id>\d+)(?:\/|$)");

        if (!match.Success)
        {
            throw new SiiException("Could not extract Season Id from Url");
        }

        return match.Groups["id"].Value;
    }

    public static string ParseEpisodeId(string url)
    {
        var match = Regex.Match(url, @"^https:\/\/[^\/]+\/[A-Za-z]{4}\/[^\/\-][^\/]*\-a(?<id>\d+)\/[^\/\-][^\/]*\-(?<id>\d+)\.[A-Za-z]{4}$");

        if (!match.Success)
        {
            throw new SiiException("Could not extract Episode Id from Url");
        }

        return match.Groups["id"].Value;
    }

    public static string ParsePath(string url)
    {
        var match = Regex.Match(url, @"^https:\/\/[^\/]+(?<path>(?:\/.*$|$))");

        if (!match.Success)
        {
            throw new SiiException("Could not extract path from Url");
        }

        return match.Groups["path"].Value;
    }

    public static (string SeasonId, string EpisodeId) ParseSeasonIdAndEpisodeId(string url)
    {
        var match = Regex.Match(url, @"^https:\/\/[^\/]+\/[A-Za-z]{4}\/[^\/\-][^\/]*\-a(?<season_id>\d+)\/[^\/\-][^\/]*\-(?<episode_id>\d+)\.[A-Za-z]{4}$");

        if (!match.Success)
        {
            throw new SiiException($"Could not extract Season Id And EpisodeId from Url: {url}");
        }

        return (SeasonId: match.Groups["season_id"].Value, EpisodeId: match.Groups["episode_id"].Value);
    }

    public static (string SeasonId, string Path) ParseSeasonIdAndPath(string url)
    {
        var match = Regex.Match(url, @"^https:\/\/[^\/]+(?<path>\/[A-Za-z]{4}\/[^\/-][^\/]*-a(?<id>\d+)(?:\/.*$|$))");

        if (!match.Success)
        {
            throw new SiiException("Could not extract entry Season Id and path from Url");
        }

        return (match.Groups["id"].Value, match.Groups["path"].Value);
    }

    public static (string EpisodeId, string Path) ParseEpisodeIdAndPath(string url)
    {
        var match = Regex.Match(url, @"^https:\/\/[^\/]+(?<path>\/[A-Za-z]{4}\/[^\/\-][^\/]*\-a(?<id>\d+)\/[^\/\-][^\/]*\-(?<id>\d+)\.[A-Za-z]{4}$)");

        if (!match.Success)
        {
            throw new SiiException("Could not extract entry Episode Id and path from Url");
        }

        return (match.Groups["id"].Value, match.Groups["path"].Value);
    }
}
