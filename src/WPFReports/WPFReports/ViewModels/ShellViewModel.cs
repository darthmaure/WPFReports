using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

namespace WPFReports.ViewModels
{
    public class ShellViewModel : ViewModelBase
    {
        private bool isBusy;
        private ViewModelBase content;

        public bool IsBusy
        {
            get { return isBusy; }
            set
            {
                if (value == isBusy) return;
                isBusy = value;
                RaisePropertyChanged();
            }
        }

        public ViewModelBase Content
        {
            get { return content ?? (content = SimpleIoc.Default.GetInstance<MainViewModel>()); }
            set
            {
                if (value == content) return;
                content = value;
                RaisePropertyChanged();
            }
        }
    }
}
