namespace Cli.Commands.Fetch;

using Spectre.Console.Cli;
using Spectre.Console;
using Sii;

internal class FetchSeasonCommand : AsyncCommand<FetchSeasonCommand.Settings>
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

        var _fetcher = new SeasonFetcher();
        var season = await _fetcher.FetchSeasonAsync(settings.Url);

        FetchFormatter.Print(season, settings.Format);

        return 0;
    }
}
