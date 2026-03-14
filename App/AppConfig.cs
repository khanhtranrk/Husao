namespace App;

using System.Text.Json;
using Microsoft.Extensions.Configuration;

public class AppConfig
{
    public string AESKey { get; set; }
    public string DeeriaBaseUrl { get; set; }
    public string DeeriaSeasonProxie { get; set; }
    public string DeeriaEpisodeProxie { get; set; }

    public byte[] AESKeyBytes => Convert.FromBase64String(AESKey);

    public AppConfig() {
        AESKey = "Ips8csbHer6nDwTqKNquJdLuU6wxJtJghJw4A/PC9EY=";
        DeeriaBaseUrl = "http://127.0.0.1:4243";
        DeeriaSeasonProxie = "husao-season";
        DeeriaEpisodeProxie = "husao-episode";
    }

    public void Save(string path)
    {
        var json = JsonSerializer.Serialize(
            this,
            new JsonSerializerOptions { WriteIndented = true }
        );
        File.WriteAllText(path, json);
    }

    public static string GetConfigPath()
    {
        var configFile = Environment.GetEnvironmentVariable("HUSAO_CONFIG_FILE");
        if (configFile is not null)
        {
            return Path.GetFullPath(configFile);
        }

        var basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        return Path.Combine(basePath, "husao", "config.json");
    }

    public static string SetupConfigPath()
    {
        var configPath = GetConfigPath();

        if (!File.Exists(configPath))
        {
            var dir = Path.GetDirectoryName(configPath);
            if (!String.IsNullOrWhiteSpace(dir))
            {
                Directory.CreateDirectory(dir);
            }

            File.WriteAllText(
                configPath,
                JsonSerializer.Serialize(
                    new AppConfig(),
                    new JsonSerializerOptions { WriteIndented = true }
                )
            );
        }

        return configPath;
    }

    public static AppConfig Get()
    {
        var configPath = SetupConfigPath();

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(configPath, optional: false, reloadOnChange: true)
            .Build();
        return configuration.Get<AppConfig>() ?? new AppConfig();
    }
}
