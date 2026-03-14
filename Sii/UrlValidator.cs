namespace Sii;

using System.Text.RegularExpressions;

public static class UrlValidator
{
    public static bool IsSeasonPageUrl(string url)
    {
        return Regex.IsMatch(url, @"^https:\/\/[^\/]+\/[A-Za-z]{4}\/[^\/\-][^\/]*\-a(?<id>\d+)(?:\/|$)");
    }

    public static bool IsSeasonsPageUrl(string url)
    {
        return Regex.IsMatch(url, @"^https:\/\/[^\/]+\/.*$");
    }

    public static bool IsEpisodePlayerPageUrl(string url)
    {
        return Regex.IsMatch(url, @"^https:\/\/[^\/]+\/[A-Za-z]{4}\/[^\/\-][^\/]*\-a(?<id>\d+)\/[^\/\-][^\/]*\-(?<id>\d+)\.[A-Za-z]{4}$");
    }
}
