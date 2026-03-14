namespace Cli.Commands.Play;

using Spectre.Console.Cli;
using Spectre.Console;
using App;
using Sii;
using Onm.Xspf;

internal class PlaySeasonCommand : AsyncCommand<PlaySeasonCommand.Settings>
{
    public class Settings : PlaySettings
    {
    }

    private readonly int _maxRetry = 10;
    private readonly int _baseDelay = 10000;

    private readonly AppConfig _appConfig;
    private readonly AppData _appData;
    private readonly AppMapper _appMapper;

    public PlaySeasonCommand(
        AppConfig appConfig,
        AppData appData,
        AppMapper appMapper)
    {
        _appConfig = appConfig;
        _appData = appData;
        _appMapper = appMapper;
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

        var host = new Uri(settings.Url).Host;

        var seasonFetcher = new SeasonFetcher();
        var season = await seasonFetcher.FetchSeasonAsync(settings.Url);

        var inProcessEpisodes = season.Episodes.Where(e => !_appData.IsCacheFileExists($"{season.Id}_{e.Id}.m3u8")).ToList();
        var failedProcessEpisodes = new List<Episode>();

        var fetcher = new MediaFileFetcher();
        var decoder = new MediaFileDecoder();

        for (var attempt = 0; attempt < _maxRetry; attempt++)
        {
            AnsiConsole.MarkupLineInterpolated($"[green]Attempt:[/] {attempt} - [green]In Process:[/] {inProcessEpisodes.Count} - [green]Total:[/] {season.Episodes.Count()}");

            foreach (var episode in inProcessEpisodes)
            {

                AnsiConsole.MarkupLineInterpolated($"[blue]Process:[/] {episode.Url}");
                try
                {
                    var file = await fetcher.FetchMediaFile(episode);
                    var data = await decoder.DecodeAsync(
                        _appConfig.AESKeyBytes,
                        file
                    );

                    _appData.CreateCacheFile($"{season.Id}_{episode.Id}.m3u8", data);
                }
                catch(Exception e)
                {
                    failedProcessEpisodes.Add(episode);
                    AnsiConsole.MarkupLineInterpolated($"[red]Failed with error:[/] {e.Message}");
                }
            }

            if (failedProcessEpisodes.Count == 0)
            {
                break;
            }

            AnsiConsole.MarkupLineInterpolated($"[yellow]Waiting {(int)Math.Pow(2, attempt) * _baseDelay}ms before continue...[/]");
            await Task.Delay((int)Math.Pow(2, attempt) * _baseDelay);

            inProcessEpisodes = failedProcessEpisodes;
            failedProcessEpisodes = new();
        }

        if (failedProcessEpisodes.Count > 0)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]Failed to process {failedProcessEpisodes.Count} episode(s)[/]. Please run the command again to resolve them!");
        }

        var playlist = _appMapper.MapSiiSeasonToOnmPlaylist(season);
        var xspfPlaylist = XspfPlaylist.From(playlist);
        XspfPlaylistWriter.Write(Path.Combine(_appData.GetCachePath(), $"{season.Id}.xspf"), xspfPlaylist);

        var playlistLink = Path.Combine(
            _appConfig.DeeriaBaseUrl,
            _appConfig.DeeriaSeasonProxie,
            $"{season.Id}.xspf"
        );

        Console.WriteLine(playlistLink);

        PlayerCaller.Call(settings.Player, playlistLink);

        return 0;
    }
}
