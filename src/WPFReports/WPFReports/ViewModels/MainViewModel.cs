using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WPFReports.Services;

namespace WPFReports.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            InitCommands();
        }

        private readonly INavigationService _navigationService;
        public ICommand GoToDesignerCommand { get; private set; }
        public ICommand GoToDisplayCommand { get; private set; }

        private void InitCommands()
        {
            GoToDisplayCommand = new RelayCommand(() => _navigationService.Navigate(typeof(DisplayViewModel)));
            GoToDesignerCommand = new RelayCommand(() => _navigationService.Navigate(typeof(DesignerHostViewModel)));
        }
    }
}
