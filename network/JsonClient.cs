using System;
using System.IO;
using System.IO.Compression;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace FantasticFour.network
{
    class JsonClient
    {
        public async Task<T> GetDataAsync<T>(string urlParams)
        {
            const string baseAddress = "https://rata.digitraffic.fi/api/v1";

            string url = baseAddress + urlParams;

            using (HttpResponseMessage response = await NetworkConnection.Client.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    byte[] compressedResponse = await response.Content.ReadAsByteArrayAsync();

                    try
                    {
                        using (var inputStream = new MemoryStream(compressedResponse))
                        using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                        using (var streamReader = new StreamReader(gZipStream))
                        {
                            var decompressed = streamReader.ReadToEnd();
                            return JsonSerializer.Deserialize<T>(decompressed);
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadKey();
                        return default(T);
                    }
                }
                else
                {
                    Console.WriteLine("Api query failed: " + response.StatusCode);
                    return default(T);
                }
            }
        }
    }
}
