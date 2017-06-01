using System;
using System.Windows;

namespace WPFReports.Services
{
    public interface IDialogService
    {
        void ShowChild(object context);
    }

    public class DialogService : IDialogService
    {
        public DialogService(Func<Window> windowFactory)
        {
            _windowFactory = windowFactory;
        }

        private readonly Func<Window> _windowFactory;

        public void ShowChild(object context)
        {
            var window = _windowFactory.Invoke();
            window.DataContext = context;
            window.Show();
        }
    }
}
