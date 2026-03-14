namespace Cli.Commands.Fetch;

using Spectre.Console.Cli;
using Spectre.Console;
using Sii;

internal class FetchEpisodeListCommand : AsyncCommand<FetchEpisodeListCommand.Settings>
{
    public class Settings : FetchSettings
    {
    }

    public override async Task<int> ExecuteAsync(
        CommandContext context,
        Settings settings,
        CancellationToken cancellation)
    {
        if (!UrlValidator.IsSeasonPageUrl(settings.Url))
        {
            AnsiConsole.MarkupLine("[bold red]Error:[/] Url is invalid");
            return 1;
        }

        var fetcher = new EpisodeFetcher();

        var episodes = await fetcher.FetchEpisodesAsync(settings.Url);

        FetchFormatter.Print(episodes, settings.Format);

        return 0;
    }
}
