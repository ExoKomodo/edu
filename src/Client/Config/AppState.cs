using System;

namespace Client.Config
{
    public static class AppState
    {
        public const string DEFAULT_FAVICON_URI = "favicons/favicon.ico";

        public static string FaviconUri
        {
            get => _faviconUri;
            set
            {
                _faviconUri = value;
                OnFaviconUriChange?.Invoke();
                NotifyStateChanged();
            }
        }
        public static event Action OnFaviconUriChange;
        public static event Action OnChange;

        public static void Reset()
        {
            FaviconUri = DEFAULT_FAVICON_URI;
        }

        private static string _faviconUri { get; set; } = DEFAULT_FAVICON_URI;

        private static void NotifyStateChanged() => OnChange?.Invoke();
    }
}