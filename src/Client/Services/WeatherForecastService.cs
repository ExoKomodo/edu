using System.Net.Http.Json;
using System.Threading.Tasks;
using Client.DTO;
using Client.Http;

namespace Client.Services
{
  public class WeatherForecastService {
        private readonly ServerApiClient _client;
        public WeatherForecastService(ServerApiClient client)
        {
            _client = client;
        }

        public async Task<WeatherForecast> GetAsync() =>
            await _client.Client.GetFromJsonAsync<WeatherForecast>("WeatherForecast");
        
        public WeatherForecast Get() =>
            GetAsync().Result;
    }
}