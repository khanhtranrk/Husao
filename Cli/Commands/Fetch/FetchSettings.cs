namespace Cli.Commands.Fetch;

using Spectre.Console.Cli;

public class FetchSettings : CommandSettings
{
    [CommandArgument(0, "<url>")]
    public required string Url { get; init; }

    [CommandOption("-f|--format")]
    public FetchFormat Format { get; init; } = FetchFormat.Pretty;
}
