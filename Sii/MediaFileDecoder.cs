namespace Sii;

using System.Security.Cryptography;
using System.IO.Compression;
using System.Text.Json;
using System.Text;

public sealed class MediaFileDecoder
{
    public async Task<byte[]> DecodeAsync(byte[] key, byte[] data)
    {
        return Normalize(await TryDecompress(AESDecrypt(key, data)));
    }

    private byte[] Normalize(byte[] data)
    {
        try
        {
            var str = JsonSerializer.Deserialize<string>(data);
            if (str is not null)
            {
                return Encoding.UTF8.GetBytes(str);
            }
        }
        catch { }

        return data;
    }

    private byte[] AESDecrypt(byte[] key, byte[] data)
    {
        using var aes = Aes.Create();
        aes.Mode = CipherMode.CBC;
        aes.Padding = PaddingMode.PKCS7;
        aes.Key = key;
        aes.IV = data[..16];

        var content = aes.CreateDecryptor()
            .TransformFinalBlock(data, 16, data.Length - 16);

        return content;
    }

    private async Task<byte[]> TryDecompress(byte[] data)
    {
        using var outputMs = new MemoryStream();

        if (data.Length > 2 && data[0] == 0x1F && data[1] == 0x8B)
        {
            using var ms = new MemoryStream(data);
            using var gz = new GZipStream(ms, CompressionMode.Decompress);

            await gz.CopyToAsync(outputMs);
            return outputMs.ToArray();
        }

        if (data.Length > 2 &&
           data[0] == 0x78 &&
           (data[1] == 0x01 || data[1] == 0x9C || data[1] == 0xDA))
        {

            using var ms = new MemoryStream(data, 2, data.Length - 2);
            using var df = new DeflateStream(ms, CompressionMode.Decompress);

            await df.CopyToAsync(outputMs);
            return outputMs.ToArray();
        }

        
        try
        {
            using var ms = new MemoryStream(data);
            using var df = new DeflateStream(ms, CompressionMode.Decompress);

            await df.CopyToAsync(outputMs);
            return outputMs.ToArray();
        }
        catch { }

        return data;
    }
}
