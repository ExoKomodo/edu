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
    public class BlogService :
        HttpResourceService<string>,
        HttpResourceGetServiceMixin<BlogService, Blog, string>
    {
        private readonly LocalClient _client;
    
        public override HttpClient GetHttpClient() => _client.Client;

        public BlogService(LocalClient client)
        {
            _client = client;
        }

        public string Id { get; set; } = "general";

        public IEnumerable<Blog> Get() => GetAsync().Result;

        public IEnumerable<Blog> Get(string dataFilePath) => GetAsync(dataFilePath).Result;

        public async Task<IEnumerable<Blog>> GetAsync() => await this.GetAsync($"blogs/{Id}/blogs.json");
        
        public async Task<IEnumerable<Blog>> GetAsync(string dataFilePath)
        {
            var blogs = await this.GetAsync<BlogService, Blog, string>(
                $"data/{dataFilePath}"
            );
            if (blogs is null)
            {
                throw new Exception($"Could not find blogs from data file: {dataFilePath}");
            }
            foreach (var blog in blogs)
            {
                blog.Content = await this.GetHtmlAsync(blog.Id);
            }
            return blogs;
        }

        public Blog GetById(string id) => GetByIdAsync(id).Result;

        public Blog GetById(string id, string dataFilePath) => GetByIdAsync(id, dataFilePath).Result;

        public async Task<Blog> GetByIdAsync(string id) => await GetByIdAsync(id, $"blogs/{Id}/blogs.json");

        public async Task<Blog> GetByIdAsync(string id, string dataFilePath)
        {
            var blog = (await GetAsync(dataFilePath)).SingleOrDefault(blog => blog.Id == id);
            if (blog is null) {
                throw new Exception($"Could not find blog with id: {id}");
            }
            blog.Content = await this.GetHtmlAsync(id);
            return blog;
        }

        protected string GetHtml(string id) =>
            this.GetHttpClient().GetStringAsync($"/data/blogs/{Id}/{id}.html").Result;

        protected async Task<string> GetHtmlAsync(string id) =>
            await this.GetHttpClient().GetStringAsync($"/data/blogs/{Id}/{id}.html");
    }
}
