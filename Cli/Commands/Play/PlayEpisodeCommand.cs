namespace Cli.Commands.Play;

using Spectre.Console.Cli;
using Spectre.Console;
using App;
using Sii;

internal class PlayEpisodeCommand : AsyncCommand<PlayEpisodeCommand.Settings>
{
    public class Settings : PlaySettings
    {
    }

    private readonly AppConfig _appConfig;
    private readonly AppData _appData;

    public PlayEpisodeCommand(AppConfig appConfig, AppData appData)
    {
        _appConfig = appConfig;
        _appData = appData;
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

        var host = new Uri(settings.Url).Host;
        var (seasonId, episodeId) = UrlParser.ParseSeasonIdAndEpisodeId(settings.Url);
        var fileName = $"{seasonId}_{episodeId}.m3u8";

        if (!_appData.IsCacheFileExists(fileName))
        {
            var episodeFetcher = new EpisodeFetcher();
            var episode = await episodeFetcher.FetchEpisodeAsync(settings.Url);

            var fetcher = new MediaFileFetcher();
            var file = await fetcher.FetchMediaFile(episode);

            var decoder = new MediaFileDecoder();
            var data = await decoder.DecodeAsync(
                _appConfig.AESKeyBytes,
                file
            );

            _appData.CreateCacheFile(fileName, data);
        }

        var playLink = Path.Combine(
            _appConfig.DeeriaBaseUrl,
            _appConfig.DeeriaEpisodeProxie,
            fileName
        );

        Console.WriteLine(playLink);

        PlayerCaller.Call(settings.Player, playLink);

        return 0;
    }
}
