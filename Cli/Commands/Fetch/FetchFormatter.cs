namespace Cli.Commands.Fetch;

using Spectre.Console;
using System.Text.Json;
using Sii;

public enum FetchFormat
{
    Pretty,
    Plain,
    Json
}

internal static class FetchFormatter
{
    public static void Print(IEnumerable<Season> seasons, FetchFormat format = FetchFormat.Pretty)
    {
        switch (format)
        {
            case FetchFormat.Pretty:
                PrettyPrint(seasons);
                break;
            case FetchFormat.Plain:
                PlainPrint(seasons);
                break;
            case FetchFormat.Json:
                JsonPrint(seasons);
                break;
        }
    }

    
    public static void Print(IEnumerable<Episode> episodes, FetchFormat format = FetchFormat.Pretty)
    {
        switch (format)
        {
            case FetchFormat.Pretty:
                PrettyPrint(episodes);
                break;
            case FetchFormat.Plain:
                PlainPrint(episodes);
                break;
            case FetchFormat.Json:
                JsonPrint(episodes);
                break;
        }
    }

    public static void Print(Season season, FetchFormat format = FetchFormat.Pretty)
    {
        switch (format)
        {
            case FetchFormat.Pretty:
                PrettyPrint(season);
                break;
            case FetchFormat.Plain:
                PlainPrint(season);
                break;
            case FetchFormat.Json:
                JsonPrint(season);
                break;
        }
    }

    public static void Print(Episode episode, FetchFormat format = FetchFormat.Pretty)
    {
        switch (format)
        {
            case FetchFormat.Pretty:
                PrettyPrint(episode);
                break;
            case FetchFormat.Plain:
                PlainPrint(episode);
                break;
            case FetchFormat.Json:
                JsonPrint(episode);
                break;
        }
    }

    public static void JsonPrint(Season season)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(season);
        var stdout = Console.OpenStandardOutput();
        stdout.Write(json, 0, json.Length);
    }

    public static void JsonPrint(Episode episode)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(episode);
        var stdout = Console.OpenStandardOutput();
        stdout.Write(json, 0, json.Length);
    }

    public static void JsonPrint(IEnumerable<Season> seasons)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(seasons);
        var stdout = Console.OpenStandardOutput();
        stdout.Write(json, 0, json.Length);
    }

    public static void JsonPrint(IEnumerable<Episode> episodes)
    {
        var json = JsonSerializer.SerializeToUtf8Bytes(episodes);
        var stdout = Console.OpenStandardOutput();
        stdout.Write(json, 0, json.Length);
    }

    public static void PrettyPrint(IEnumerable<Season> seasons)
    {

        AnsiConsole.Write(new Rule());
        foreach (var season in seasons)
        {
            PrettyPrint(season);
            AnsiConsole.Write(new Rule());
        }
    }

    public static void PrettyPrint(IEnumerable<Episode> episodes)
    {

        AnsiConsole.Write(new Rule());
        foreach (var episode in episodes)
        {
            PrettyPrint(episode);
            AnsiConsole.Write(new Rule());
        }
    }

    public static void PlainPrint(IEnumerable<Season> seasons)
    {

        AnsiConsole.Write(new Rule());
        foreach (var season in seasons)
        {
            PlainPrint(season);
            AnsiConsole.Write(new Rule());
        }
    }

    public static void PlainPrint(IEnumerable<Episode> episodes)
    {

        AnsiConsole.Write(new Rule());
        foreach (var episode in episodes)
        {
            PlainPrint(episode);
            AnsiConsole.Write(new Rule());
        }
    }

    public static void PrettyPrint(Season season)
    {
        if (!string.IsNullOrWhiteSpace(season.Title))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Title[/]        {season.Title}");
        }

        if (!string.IsNullOrWhiteSpace(season.OtherTitles))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Other Titles[/] {season.OtherTitles}");
        }

        if (!string.IsNullOrWhiteSpace(season.ReleaseDate))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Release Date[/] {season.ReleaseDate}");
        }

        if (!string.IsNullOrWhiteSpace(season.Score))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Score[/]        {season.Score}");
        }

        if (!string.IsNullOrWhiteSpace(season.ImageUrl))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Image[/]        {season.ImageUrl}");
        }

        if (!string.IsNullOrWhiteSpace(season.Url))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Url[/]          {season.Url}");
        }

        if (!string.IsNullOrWhiteSpace(season.Description))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Description[/]  {season.Description}");
        }

        if (season.Episodes.Count() > 0)
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Episodes[/]");
            foreach (var episode in season.Episodes)
            {
                AnsiConsole.MarkupLineInterpolated($"  - {episode.Title}\n    {episode.Url}");
            }
        }
    }

    public static void PrettyPrint(Episode episode)
    {
        if (!string.IsNullOrWhiteSpace(episode.Id))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Id[/]    {episode.Id}");
        }
        
        if (!string.IsNullOrWhiteSpace(episode.Title))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Title[/] {episode.Title}");
        }

        if (!string.IsNullOrWhiteSpace(episode.Url))
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Url[/]   {episode.Url}");
        }
    }

    public static void PlainPrint(Season season)
    {
        Console.WriteLine(season.ToString());
    }

    public static void PlainPrint(Episode episode)
    {
        Console.WriteLine(episode.ToString());
    }
}
