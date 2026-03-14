namespace Cli.Commands.Fetch;

using Spectre.Console.Cli;
using Spectre.Console;
using Sii;

internal class FetchEpisodeCommand : AsyncCommand<FetchEpisodeCommand.Settings>
{
	public class Settings : FetchSettings
	{
	}

	public override async Task<int> ExecuteAsync(
		CommandContext context,
		Settings settings,
		CancellationToken cancellation)
	{
		if (!UrlValidator.IsEpisodePlayerPageUrl(settings.Url))
		{
            AnsiConsole.MarkupLine("[bold red]Error:[/] Url is invalid");
            return 1;
		}

		var fetcher = new EpisodeFetcher();
		var episode = await fetcher.FetchEpisodeAsync(settings.Url);

		FetchFormatter.Print(episode, settings.Format);

		return 0;
	}
}
