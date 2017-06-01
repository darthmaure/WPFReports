using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using WPFReports.Services;

namespace WPFReports.ViewModels
{
    public class DesignerHostViewModel : ViewModelBase
    {
        public DesignerHostViewModel(
            INavigationService navigationService,
            DesignerViewModel designer,
            WidgetListViewModel list,
            WidgetCompileResultViewModel results)
        {
            _navigationService = navigationService;
            Designer = designer;
            List = list;
            Results = results;

            InitCommands();
        }

        private readonly INavigationService _navigationService;

        public DesignerViewModel Designer { get; private set; }
        public WidgetListViewModel List { get; private set; }
        public WidgetCompileResultViewModel Results { get; private set; }
        public ICommand GoBackCommand { get; private set; }

        protected void InitCommands()
        {
            GoBackCommand = new RelayCommand(() => _navigationService.NavigateBack());
        }
    }
}
