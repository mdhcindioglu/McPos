using McPos.Shared.Models;

namespace McPos.Client.Services
{
    public class ToastInstance
    {
        public Guid Id { get; set; }
        public DateTime TimeStamp { get; set; }
        public ToastSettings ToastSettings { get; set; }
    }

    public class ToastSettings
    {
        public ToastSettings(string Heading, string Message, string BaseClass, string AdditionalClasses, string IconClass)
        {
            this.Heading = Heading;
            this.Message = Message;
            this.BaseClass = BaseClass;
            this.AdditionalClasses = AdditionalClasses;
            this.IconClass = IconClass;
        }

        public string Heading { get; set; }
        public string Message { get; set; }
        public string BaseClass { get; set; }
        public string AdditionalClasses { get; set; }
        public string IconClass { get; set; }
    }

    public enum ToastPosition
    {
        TopLeft,
        TopRight,
        TopCenter,
        BottomLeft,
        BottomRight,
        BottomCenter,
    }

    public interface IToastService
    {
        event Action<ToastLevel, string, string> OnShow;

        void ShowInfo(string message, string heading = "");
        void ShowSuccess(string message, string heading = "");
        void ShowError(string message, string heading = "");
        void ShowError(string message, Exception ex, string heading = "");
        void ShowError(string message, Error err, string heading = "");
        void ShowWarning(string message, string heading = "");
        void ShowToast(ToastLevel level, string message, string heading = "");
    }

    public class ToastService : IToastService
    {
        public ToastService()
        {

        }

        public event Action<ToastLevel, string, string> OnShow;

        public void ShowError(string message, string heading = "")
        {
            ShowToast(ToastLevel.Error, message, heading);
        }

        public void ShowError(string message, Exception ex, string heading = "")
        {
            ShowToast(ToastLevel.Error, $"{message}/r/n{ex.Message}", heading);
        }

        public void ShowError(string message, Error err, string heading = "")
        {
            ShowToast(ToastLevel.Error, $"{message}/r/n{err.Message}", heading);
        }

        public void ShowInfo(string message, string heading = "")
        {
            ShowToast(ToastLevel.Info, message, heading);
        }

        public void ShowSuccess(string message, string heading = "")
        {
            ShowToast(ToastLevel.Success, message, heading);
        }

        public void ShowWarning(string message, string heading = "")
        {
            ShowToast(ToastLevel.Warning, message, heading);
        }

        public void ShowToast(ToastLevel level, string message, string heading = "")
        {
            OnShow?.Invoke(level, message, heading);
        }
    }

    public enum ToastLevel
    {
        Info, Success, Warning, Error,
    }
}
