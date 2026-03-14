namespace App;

public class AppData
{
    public string GetDataPath()
    {
        var basePath = Environment.GetEnvironmentVariable("HUSAO_DATA_PATH");
        if (String.IsNullOrWhiteSpace(basePath))
        {
            basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        return Path.GetFullPath(Path.Combine(basePath, "husao"));
    }

    public string GetArchivePath()
    {

        var basePath = Environment.GetEnvironmentVariable("HUSAO_DATA_PATH");
        if (String.IsNullOrWhiteSpace(basePath))
        {
            basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        return Path.GetFullPath(Path.Combine(basePath, "husao", "archive"));
    }

    public string GetCachePath()
    {

        var basePath = Environment.GetEnvironmentVariable("HUSAO_DATA_PATH");
        if (String.IsNullOrWhiteSpace(basePath))
        {
            basePath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        }

        return Path.GetFullPath(Path.Combine(basePath, "husao", "cache"));
    }

    public bool IsDataFileExists(string fileName)
    {
        var dataPath = GetDataPath();
        var path = Path.Combine(dataPath, fileName);

        return File.Exists(path);
    }

    public bool IsArchiveFileExists(string fileName)
    {
        var archivePath = GetArchivePath();
        var path = Path.Combine(archivePath, fileName);

        return File.Exists(path);
    }

    public bool IsCacheFileExists(string fileName)
    {
        var cachePath = GetCachePath();
        var path = Path.Combine(cachePath, fileName);

        return File.Exists(path);
    }

    public void CreateDataFile(string fileName, byte[] bytes)
    {
        var dataPath = GetDataPath();
        var path = Path.Combine(dataPath, fileName);

        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        File.WriteAllBytes(path, bytes);
    }

    public void CreateArchiveFile(string fileName, byte[] bytes)
    {
        var archivePath = GetArchivePath();
        var path = Path.Combine(archivePath, fileName);

        if (!Directory.Exists(archivePath))
        {
            Directory.CreateDirectory(archivePath);
        }

        File.WriteAllBytes(path, bytes);
    }

    public void CreateCacheFile(string fileName, byte[] bytes)
    {
        var cachePath = GetCachePath();
        var path = Path.Combine(cachePath, fileName);

        if (!Directory.Exists(cachePath))
        {
            Directory.CreateDirectory(cachePath);
        }

        File.WriteAllBytes(path, bytes);
    }

    public void CreateDataFile(string fileName, string text)
    {
        var dataPath = GetDataPath();
        var path = Path.Combine(dataPath, fileName);

        if (!Directory.Exists(dataPath))
        {
            Directory.CreateDirectory(dataPath);
        }

        File.WriteAllText(path, text);
    }

    public void CreateArchiveFile(string fileName, string text)
    {
        var archivePath = GetArchivePath();
        var path = Path.Combine(archivePath, fileName);

        if (!Directory.Exists(archivePath))
        {
            Directory.CreateDirectory(archivePath);
        }

        File.WriteAllText(path, text);
    }

    public void CreateCacheFile(string fileName, string text)
    {
        var cachePath = GetCachePath();
        var path = Path.Combine(cachePath, fileName);

        if (!Directory.Exists(cachePath))
        {
            Directory.CreateDirectory(cachePath);
        }

        File.WriteAllText(path, text);
    }
}
