namespace Cli;

using Microsoft.Extensions.DependencyInjection;
using Spectre.Console.Cli;
using Spectre.Console.Cli.Extensions.DependencyInjection;
using App;
using Cli.Commands.Fetch;
using Cli.Commands.Play;

class Program
{
    static int Main(string[] args)
    {
        var config = AppConfig.Get();
        var services = new ServiceCollection();

        services.AddSingleton(config);
        services.AddSingleton<AppData>();
        services.AddSingleton<AppMapper>();
        services.AddSingleton<PlayEpisodeCommand>();
        services.AddSingleton<PlaySeasonCommand>();

        var registrar = new DependencyInjectionRegistrar(services);

        var app = new CommandApp(registrar);
        app.Configure(config =>
        {
            config.AddBranch("fetch", fetchConfig =>
            {
                fetchConfig.AddCommand<FetchSeasonCommand>("season");
                fetchConfig.AddCommand<FetchSeasonListCommand>("season-list");
                fetchConfig.AddCommand<FetchEpisodeCommand>("episode");
                fetchConfig.AddCommand<FetchEpisodeListCommand>("episode-list");
            });
            config.AddBranch("play", streamConfig =>
            {
                streamConfig.AddCommand<PlayEpisodeCommand>("episode");
                streamConfig.AddCommand<PlaySeasonCommand>("season");
            });
        });

        return app.Run(args);
    }
}
