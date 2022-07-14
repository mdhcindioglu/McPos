using Microsoft.JSInterop;

namespace McPos.Client.Extensions
{
    public static class IJSRuntimeExtensions
    {
        public static async Task SetFocus(this IJSRuntime JS) => await JS.InvokeVoidAsync("setFocus");
        public static ValueTask<bool> ShowConfirmation(this IJSRuntime js, string message, string title = "Question", string? confirmText = "Yes", string? cancelText = "No", SweetAlertMessageTypes type = SweetAlertMessageTypes.Question, string? confirmButtonColor = "#3085d6", string? cancelButtonColor = "#6e7d88")
            => js.InvokeAsync<bool>("showConfirmation", message, title, confirmText, cancelText, type.ToString().ToLower(), confirmButtonColor, cancelButtonColor);
    }
    public enum SweetAlertMessageTypes { Success, Error, Warning, Info, Question, }
}
