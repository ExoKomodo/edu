using System;

namespace Client.Config
{
    public static class AppState
    {
        #region Public

        #region Defaults
        public const string DEFAULT_FAVICON_URI = "favicons/favicon.ico";
        public const bool DEFAULT_IS_SIDENAV_HIDDEN = false;
        #endregion

        #region Fields
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
        public static bool IsSideNavHidden
        {
            get => _isSideNavHidden;
            set
            {
                _isSideNavHidden = value;
                OnIsSideNavHiddenChange?.Invoke();
                NotifyStateChanged();
            }
        }
        public static event Action OnFaviconUriChange;
        public static event Action OnIsSideNavHiddenChange;
        public static event Action OnChange;
        #endregion

        #region Methods
        public static void Reset()
        {
            FaviconUri = DEFAULT_FAVICON_URI;
            IsSideNavHidden = DEFAULT_IS_SIDENAV_HIDDEN;
        }
        #endregion

        #endregion

        #region Private

        #region Fields
        private static string _faviconUri { get; set; } = DEFAULT_FAVICON_URI;
        private static bool _isSideNavHidden { get; set; } = DEFAULT_IS_SIDENAV_HIDDEN;
        #endregion

        #region Methods
        private static void NotifyStateChanged() => OnChange?.Invoke();
        #endregion

        #endregion
    }
}