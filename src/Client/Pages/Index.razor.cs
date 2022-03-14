using Client.Config;
using Microsoft.AspNetCore.Components;
using Client.DTO;
using System.Threading.Tasks;
using Client.Services;
using System;

namespace Client.Pages
{
  public partial class Index
    {
        [Inject]
        private WeatherForecastService _weatherForecastService { get; set; }
        private WeatherForecast _weatherGuess { get; set; }
        public bool IsLoading { get; set; }
        public const bool IsDebug =
            #if DEBUG
            true
            #else
            false
            #endif
            ;
        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                AppState.Reset();
            }
        }

        protected override async Task OnInitializedAsync()
        {
            IsLoading = true;
            try
            {
                _weatherGuess = await _weatherForecastService.GetAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unable to retrieve weather guess:{ex.GetType()}:{ex.Message}:{ex.StackTrace}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}