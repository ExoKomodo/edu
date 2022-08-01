using System;
using Client.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace Client.Shared
{
    public partial class MainLayout : IDisposable
    {
        [Inject]
        protected IJSRuntime _jsRuntime { get; set; }

        protected override void OnInitialized()
        {
            AppState.OnFaviconUriChange += UpdateFavicon;
        }

        protected void UpdateFavicon()
        {
            _jsRuntime.InvokeVoidAsync("updateFavicon", AppState.FaviconUri);
            StateHasChanged();
        }

        public void Dispose()
        {
            AppState.OnFaviconUriChange -= UpdateFavicon;
        }
    }
}