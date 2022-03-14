using System.Threading.Tasks;
using Microsoft.JSInterop;

namespace Client.Helpers.Browser
{
    public static class Dom
    {
        #region Public

        #region Methods
        public static void ScrollIntoView(IJSRuntime jsRuntime, string nodeId)
        {
            if (nodeId.StartsWith("#")) {
                nodeId = nodeId[1..];
            }
            jsRuntime.InvokeVoidAsync(
                "scrollIntoView",
                nodeId
            );
        }

        public static void UpdateFavicon(IJSRuntime jsRuntime, string faviconUri)
        {
            jsRuntime.InvokeVoidAsync("updateFavicon", faviconUri);
        }
        #endregion

        #endregion
    }
}
