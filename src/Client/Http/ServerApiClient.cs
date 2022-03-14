using System.Net.Http;

namespace Client.Http
{
  public class ServerApiClient {
        public readonly HttpClient Client;
        public ServerApiClient(HttpClient client)
        {
            Client = client;
        }
    }
}