namespace Cli.Commands.Play;

using Spectre.Console.Cli;

public class PlaySettings : CommandSettings
{
    [CommandArgument(0, "<url>")]
    public required string Url { get; init; }

    [CommandOption("-p|--player")]
    public Player Player { get; init; } = Player.VLC;
}
