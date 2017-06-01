using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Statistics.Core.Widgets;
using WPFReports.Services;

namespace WPFReports.ViewModels
{
    public class DisplayViewModel : ViewModelBase
    {
        public DisplayViewModel(
                    INavigationService navigationService,
                    INotificationService notificationService,
                    IWidgetService widgetService,
                    ICollectectionCreatorService collectionService)
        {
            _navigationService = navigationService;
            _notificationService = notificationService;
            _widgetService = widgetService;
            _collectionService = collectionService;

            InitCommands();
        }

        #region Fields
        private readonly INavigationService _navigationService;
        private readonly INotificationService _notificationService;
        private readonly IWidgetService _widgetService;
        private readonly ICollectectionCreatorService _collectionService;

        private IList<WidgetItem> widgets;
        private WidgetItem selectedWidget;

        #endregion
        #region Properties
        public IList<WidgetItem> Widgets
        {
            get { return widgets; }
            set
            {
                if (value == widgets) return;
                widgets = value;
                RaisePropertyChanged();
            }
        }
        public WidgetItem SelectedWidget
        {
            get { return selectedWidget; }
            set
            {
                if (value == selectedWidget) return;
                selectedWidget = value;
                RaisePropertyChanged();
            }
        }

        public ICommand LoadCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }
        #endregion
        #region Methods
        protected void InitCommands()
        {
            LoadCommand = new RelayCommand(async () => await OnLoad());
            RefreshCommand = new RelayCommand(async () => await OnRefresh());
            GoBackCommand = new RelayCommand(() => _navigationService.NavigateBack());
        }

        private async Task OnLoad()
        {
            _notificationService.StartProgress();

#if DEBUG
            await Task.Delay(2000); //dummy delay when debug to see if notification works fine
#endif

            var items = await _widgetService.LoadAsync();
            Widgets = _collectionService.Create(items);
            SelectedWidget = Widgets.FirstOrDefault();

            _notificationService.EndProgress();
        }
        private async Task OnRefresh()
        {
            if (SelectedWidget == null) return;

            _notificationService.StartProgress();

            await _widgetService.RefreshAsync(SelectedWidget);

            _notificationService.EndProgress();
        }
        #endregion
    }
}
