using System.Net.Http;

namespace Blizzard.Api
{
    public abstract class ApiHandler
    {
        private const string ApiAgentName = "SnowtopiaModding/BlizzardInstaller";
        private const string ApiHost = "bamsestudio.dk";

        protected const string ApiEndpoint = "https://" + ApiHost + "/api/snowtopia/";

        protected static HttpClient CreateHttpClient()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Host", ApiHost);
            client.DefaultRequestHeaders.Add("User-Agent", ApiAgentName);
            return client;
        }
    }
}
