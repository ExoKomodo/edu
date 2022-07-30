using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Client.Http;
using Client.Models;
using Client.Services.Mixins;

namespace Client.Services
{
    public abstract class BlogService :
        HttpResourceService<string>,
        HttpResourceGetServiceMixin<BlogService, Blog, string>
    {
        private readonly LocalClient _client;
    
        public override HttpClient GetHttpClient() => _client.Client;

        protected BlogService(LocalClient client)
        {
            _client = client;
        }

        public abstract string Id { get; }

        public IEnumerable<Blog> Get() => GetAsync().Result;

        public IEnumerable<Blog> Get(string dataFilePath) => GetAsync(dataFilePath).Result;

        public async Task<IEnumerable<Blog>> GetAsync() => await this.GetAsync($"blogs/{Id}/blogs.json");
        
        public async Task<IEnumerable<Blog>> GetAsync(string dataFilePath) =>
            await this.GetAsync<BlogService, Blog, string>(
                $"data/{dataFilePath}"
            );

        public Blog GetById(string id) => GetByIdAsync(id).Result;

        public Blog GetById(string id, string dataFilePath) => GetByIdAsync(id, dataFilePath).Result;

        public async Task<Blog> GetByIdAsync(string id) => await GetByIdAsync(id, $"blogs/{Id}/blogs.json");

        public async Task<Blog> GetByIdAsync(string id, string dataFilePath)
        {
            var blog = (await GetAsync(dataFilePath)).SingleOrDefault(blog => blog.Id == id);
            return blog ?? throw new Exception($"Could not find blog with id: {id}");
        }
    }
}
