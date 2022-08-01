using Microsoft.JSInterop;

namespace Client.Helpers.Browser
{
    public class LocalStorage
    {
        public LocalStorage(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime as IJSInProcessRuntime;
        }

        public T Load<T>(string key)
        {
            return _jsRuntime.Invoke<T>("loadFromLocalStorage", key);
        }

        public void Save(string key, object obj)
        {
            _jsRuntime.InvokeVoid("saveToLocalStorage", key, obj);
        }

        protected readonly IJSInProcessRuntime _jsRuntime;
    }
}
