namespace Cli.Commands.Play;

using System.Diagnostics;

public enum Player
{
    VLC,
    MPV,
    FFPLAY,
    WEB,
    NONE,
}

public static class PlayerCaller
{
    public static void Call(Player player, string url)
    {
        switch (player)
        {
            case Player.VLC:
                CallVlc(url);
                break;
            case Player.MPV:
                CallMpv(url);
                break;
            case Player.FFPLAY:
                CallFFPlay(url);
                break;
            case Player.WEB:
                CallWeb(url);
                break;
        }
    }

    public static void CallVlc(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "vlc",
            Arguments = $"--quiet {url}",
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Normal
        };

        Process.Start(psi);
    }

    public static void CallMpv(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "mpv",
            Arguments = $"{url}",
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Normal
        };

        Process.Start(psi);
    }

    public static void CallFFPlay(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "ffplay",
            Arguments = $"{url}",
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Normal
        };

        Process.Start(psi);
    }

    public static void CallWeb(string url)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "vlc",
            Arguments = $"--quiet {url}",
            UseShellExecute = true,
            CreateNoWindow = true,
            WindowStyle = ProcessWindowStyle.Normal
        };

        Process.Start(psi);
    }
}
