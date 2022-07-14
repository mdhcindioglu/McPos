using McPos.Client.Services;
using Microsoft.AspNetCore.Components;
using Timer = System.Timers.Timer;

namespace McPos.Client.Pages.Components
{
    public partial class Toasts
    {
        [Inject] public IToastService ToastService { get; set; }
        [Parameter] public string InfoClass { get; set; }
        [Parameter] public string InfoIconClass { get; set; }
        [Parameter] public string SuccessClass { get; set; }
        [Parameter] public string SuccessIconClass { get; set; }
        [Parameter] public string ErrorClass { get; set; }
        [Parameter] public string ErrorIconClass { get; set; }
        [Parameter] public string WarningClass { get; set; }
        [Parameter] public string WarningIconClass { get; set; }
        [Parameter] public ToastPosition Position { get; set; } = ToastPosition.TopLeft;
        [Parameter] public int TimeOut { get; set; } = 5;

        private string PositionClass { get; set; } = string.Empty;

        internal List<ToastInstance> ToastList { get; set; } = new();

        protected override void OnInitialized()
        {
            ToastService.OnShow += ShowToast;
            PositionClass = $"position-{Position.ToString().ToLower()}";
        }

        public void RemoveToast(Guid toastId)
        {
            InvokeAsync(() =>
            {
                var toastInstance = ToastList.SingleOrDefault(x => x.Id == toastId);
                ToastList.Remove(toastInstance);
                StateHasChanged();
            });
        }

        private ToastSettings BuildToastSettings(ToastLevel level, string message, string heading)
        {
            switch (level)
            {
                case ToastLevel.Info:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Info" : heading, message, "blazored-toast-info", InfoClass, InfoIconClass);
                case ToastLevel.Success:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Success" : heading, message, "blazored-toast-success", SuccessClass, SuccessIconClass);
                case ToastLevel.Warning:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Warning" : heading, message, "blazored-toast-warning", WarningClass, WarningIconClass);
                case ToastLevel.Error:
                    return new ToastSettings(string.IsNullOrWhiteSpace(heading) ? "Error" : heading, message, "blazored-toast-error", ErrorClass, ErrorIconClass);
            }

            throw new InvalidOperationException();
        }

        private void ShowToast(ToastLevel level, string message, string heading)
        {
            InvokeAsync(() =>
            {
                var settings = BuildToastSettings(level, message, heading);
                var toast = new ToastInstance
                {
                    Id = Guid.NewGuid(),
                    TimeStamp = DateTime.Now,
                    ToastSettings = settings,
                };
                ToastList.Add(toast);

                var timeOut = TimeOut * 1000;
                var toastTimer = new Timer(timeOut);
                toastTimer.Elapsed += (sender, args) => { RemoveToast(toast.Id); };
                toastTimer.AutoReset = false;
                toastTimer.Start();

                StateHasChanged();
            });
        }
    }
}
