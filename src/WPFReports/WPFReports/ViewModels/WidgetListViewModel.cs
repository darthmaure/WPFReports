using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Statistics.Core.Widgets;
using WPFReports.Messages;
using WPFReports.Services;

namespace WPFReports.ViewModels
{
    public class WidgetListViewModel : ViewModelBase
    {
        public WidgetListViewModel(
           IWidgetManagerService widgetManagerService,
           ICollectectionCreatorService collectionService,
           INotificationService notificationService)
        {
            _widgetManagerService = widgetManagerService;
            _collectionService = collectionService;
            _notificationService = notificationService;

            InitCommands();
        }

        private readonly IWidgetManagerService _widgetManagerService;
        private readonly ICollectectionCreatorService _collectionService;
        private readonly INotificationService _notificationService;

        private WidgetItem selectedWidget;
        private ObservableCollection<WidgetItem> widgets;

        public ObservableCollection<WidgetItem> Widgets
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
                OnSelectedItemChanged();
            }
        }

        public ICommand LoadAllCommand { get; private set; }
        public ICommand SaveAllCommand { get; private set; }
        public ICommand CreateNewCommand { get; private set; }
        public ICommand DeleteSelectedCommand { get; set; }

        private void InitCommands()
        {
            CreateNewCommand = new RelayCommand(Create);
            DeleteSelectedCommand = new RelayCommand(Delete);
            LoadAllCommand = new RelayCommand(async () => await LoadAsync());
            SaveAllCommand = new RelayCommand(async () => await SaveAsync());
        }

        private void Create()
        {
            _notificationService.StartProgress();

            var newItem = _widgetManagerService.CreateNew();
            Widgets.Add(newItem);
            SelectedWidget = newItem;

            _notificationService.EndProgress();
        }
        private void Delete()
        {
            if (SelectedWidget == null) return;
            _notificationService.StartProgress();
            _widgetManagerService.Delete(SelectedWidget);
            _notificationService.EndProgress();
        }
        private async Task LoadAsync()
        {
            _notificationService.StartProgress();

            var items = await _widgetManagerService.LoadAsync();
            Widgets = _collectionService.Create(items);

            _notificationService.EndProgress();
        }
        private async Task SaveAsync()
        {
            _notificationService.StartProgress();

            await _widgetManagerService.SaveAsync(Widgets);

            _notificationService.EndProgress();
        }

        private void OnSelectedItemChanged()
        {
            Messenger.Default.Send(new SelectedWidgetChangedMessage { Widget = SelectedWidget });
        }
    }
}
