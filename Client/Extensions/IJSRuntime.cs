using Microsoft.JSInterop;

namespace McPos.Client.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task SetFocus(this IJSRuntime JS) => await JS.InvokeVoidAsync("setFocus");
    }
}
