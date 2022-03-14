using System.Net.Http;

namespace Client.Services
{
    public abstract class HttpResourceService<T> : Service
    {
        public abstract HttpClient GetHttpClient();
    }
}