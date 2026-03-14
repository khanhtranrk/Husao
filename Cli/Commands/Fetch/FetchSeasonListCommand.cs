namespace Cli.Commands.Fetch;

using Spectre.Console.Cli;
using Spectre.Console;
using Sii;

internal class FetchSeasonListCommand : AsyncCommand<FetchSeasonListCommand.Settings>
{
    public class Settings : FetchSettings
    {
    }

    public override async Task<int> ExecuteAsync(
        CommandContext context,
        Settings settings,
        CancellationToken cancellation)
    {
        if (!UrlValidator.IsSeasonsPageUrl(settings.Url))
        {
            AnsiConsole.MarkupLine("[bold red]Error:[/] Url is invalid");
            return 1;
        }

        var _fetcher = new SeasonFetcher();

        var seasons = await _fetcher.FetchSeasonsAsync(settings.Url);

        FetchFormatter.Print(seasons, settings.Format);

        return 0;
    }
}
