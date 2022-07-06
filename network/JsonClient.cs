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
            //Apin endpoint url
            const string baseAddress = "https://rata.digitraffic.fi/api/v1";

            //Requestin url:n luonti
            string url = baseAddress + urlParams;

            //Kutsutaan Apia NetworkConnectionin HttpClientillä
            using (HttpResponseMessage response = await NetworkConnection.Client.GetAsync(url))
            {
                //Jos Api palauttaa koodin 200-299
                if (response.IsSuccessStatusCode)
                {
                    //luetaan Apilta saadun responsen sisältö byte[]:in
                    byte[] compressedResponse = await response.Content.ReadAsByteArrayAsync();

                    //Gzipin dekompressointi
                    try
                    {
                        using (var inputStream = new MemoryStream(compressedResponse))
                        using (var gZipStream = new GZipStream(inputStream, CompressionMode.Decompress))
                        using (var streamReader = new StreamReader(gZipStream))
                        {
                            var decompressed = streamReader.ReadToEnd();

                            //palautetaan dekompressoidusta jsonista olio
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
