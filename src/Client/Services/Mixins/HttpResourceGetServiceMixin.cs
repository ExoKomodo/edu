using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.Models;

namespace Client.Services.Mixins
{
    public interface HttpResourceGetServiceMixin<TService, TModel, TId>
        where TService : HttpResourceGetServiceMixin<TService, TModel, TId>
        where TModel : Model<TId>
    {
        HttpClient GetHttpClient();
        Task<IEnumerable<TModel>> GetAsync();
        IEnumerable<TModel> Get() => GetAsync().Result;
        Task<TModel> GetByIdAsync(TId id);
        TModel GetById(TId id) => GetByIdAsync(id).Result;
    }

    internal static class HttpResourceGetServiceExtensions
    {
        internal static async Task<IEnumerable<TModel>> GetAsync<TService, TModel, TId>(
            this TService service,
            string url
        ) where TService : HttpResourceGetServiceMixin<TService, TModel, TId>
          where TModel : Model<TId> =>
            await service.GetHttpClient().GetFromJsonAsync<IEnumerable<TModel>>(url)
                ?? new List<TModel>();
        
        internal static IEnumerable<TModel> Get<TService, TModel, TId>(
            this TService service,
            string url
        ) where TService : HttpResourceGetServiceMixin<TService, TModel, TId>
          where TModel : Model<TId> =>
            service.GetAsync<TService, TModel, TId>(url).Result;
    }
}
