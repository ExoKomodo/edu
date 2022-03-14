using System.Net.Http;

namespace Client.Http
{
    public class LocalClient {
        public readonly HttpClient Client;
        public LocalClient(HttpClient client)
        {
            Client = client;
        }
    }
}