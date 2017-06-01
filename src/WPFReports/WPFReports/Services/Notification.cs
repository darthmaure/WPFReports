using WPFReports.ViewModels;

namespace WPFReports.Services
{
    public interface INotificationService
    {
        void StartProgress();
        void EndProgress();
    }

    public class NotificationService : INotificationService
    {
        public NotificationService(ShellViewModel host)
        {
            _host = host;
        }

        private readonly ShellViewModel _host;

        public void EndProgress()
        {
            _host.IsBusy = false;
        }

        public void StartProgress()
        {
            _host.IsBusy = true;
        }
    }
}
