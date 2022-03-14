using Microsoft.JSInterop;

namespace Client.Helpers.Browser
{
    public class LocalStorage
    {
        #region Public

        #region Constructors
        public LocalStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime as IJSInProcessRuntime;
        }
        #endregion

        #region Member Methods
        public T Load<T>(string key)
        {
            return _jsRuntime.Invoke<T>("loadFromLocalStorage", key);
        }

        public void Save(string key, object obj)
        {
            _jsRuntime.InvokeVoid("saveToLocalStorage", key, obj);
        }
        #endregion

        #endregion

        #region Protected

        #region Members
        protected readonly IJSInProcessRuntime _jsRuntime;
        #endregion

        #endregion
    }
}
