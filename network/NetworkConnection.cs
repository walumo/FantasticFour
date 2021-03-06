using System.Net.Http;
using System.Net.Http.Headers;

namespace FantasticFour.network
{
    static class NetworkConnection
    {
        public static HttpClient Client { get; set; }

        public static void InitializeClient()
        {
            //Alustetaan Client ja annetaan määritykset GZip pakattua Jsonia varten.

            Client = new HttpClient();
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            Client.DefaultRequestHeaders.AcceptEncoding.Add(new StringWithQualityHeaderValue("gzip"));
        }
    }
}
