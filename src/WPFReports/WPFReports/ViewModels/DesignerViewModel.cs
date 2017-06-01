using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using Statistics.Core.Widgets;
using Statistics.Core.Widgets.Designer;
using WPFReports.Messages;
using WPFReports.Services;

namespace WPFReports.ViewModels
{
    public class DesignerViewModel : ViewModelBase
    {
        public DesignerViewModel(
                IDesignerControlCreatorService designerControlCreatorService,
                ISnippetService codeSnippetService,
                IWidgetCompiler compilerService,
                IWidgetService widgetService,
                IDialogService dialogService)
        {
            _designerControlCreatorService = designerControlCreatorService;
            _codeSnippetService = codeSnippetService;
            _widgetService = widgetService;
            _dialogService = dialogService;
            _compilerService = compilerService;

            Initialize();
            InitCommands();
        }

        private readonly IDesignerControlCreatorService _designerControlCreatorService;
        private readonly ISnippetService _codeSnippetService;
        private readonly IWidgetService _widgetService;
        private readonly IDialogService _dialogService;
        private readonly IWidgetCompiler _compilerService;

        private WidgetItem selectedWidget;
        private string selectedWidgetLayout;
        private string selectedWidgetCode;
        private int layoutCaretPosition;
        private int codeCaretPosition;

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

        public string SelectedWidgetLayout
        {
            get { return selectedWidgetLayout; }
            set
            {
                if (value == selectedWidgetLayout) return;
                selectedWidgetLayout = value;
                selectedWidget.Layout = value;
                RaisePropertyChanged();
            }
        }
        public string SelectedWidgetCode
        {
            get { return selectedWidgetCode; }
            set
            {
                if (value == selectedWidgetCode) return;
                selectedWidgetCode = value;
                selectedWidget.Code = value;
                RaisePropertyChanged();
            }
        }
        public int LayoutCaretPosition
        {
            get { return layoutCaretPosition; }
            set
            {
                if (value == layoutCaretPosition) return;
                layoutCaretPosition = value;
                RaisePropertyChanged();
            }
        }
        public int CodeCaretPosition
        {
            get { return codeCaretPosition; }
            set
            {
                if (value == codeCaretPosition) return;
                codeCaretPosition = value;
                RaisePropertyChanged();
            }
        }

        public ICommand RunSelectedWidgetCommand { get; private set; }
        public ICommand CompileSelectedCommand { get; private set; }
        public ICommand InsertControlCommand { get; private set; }
        public ICommand InsertCodeSnippetCommand { get; private set; }

        private void Initialize()
        {
            Messenger.Default.Register<SelectedWidgetChangedMessage>(this, OnSelectedWidgetChanged);
        }

        private void InitCommands()
        {
            InsertControlCommand = new RelayCommand<object>(o => OnInsertControl(o as ControlCreator));
            InsertCodeSnippetCommand = new RelayCommand<string>(s => OnInsertCodeSnippet(s));
            RunSelectedWidgetCommand = new RelayCommand(async () => await OnRun());
        }

        private void OnInsertControl(ControlCreator controlCreator)
        {
            if (controlCreator == null || selectedWidget == null) return;

            SelectedWidgetLayout = _designerControlCreatorService.InsertControlSnippet(controlCreator, SelectedWidgetLayout, LayoutCaretPosition);
        }
        private void OnInsertCodeSnippet(string snippet)
        {
            if (string.IsNullOrEmpty(snippet) || selectedWidget == null) return;

            SelectedWidgetCode = _codeSnippetService.Insert(SelectedWidgetCode, snippet, CodeCaretPosition);
        }
        private void OnCompile()
        {
            if (SelectedWidget == null) return;

            var compileCodeResult = _compilerService.Compile(SelectedWidgetCode);

            Messenger.Default.Send(new WidgetCompileResultMessage { CompileResult = compileCodeResult });
        }
        private async Task OnRun()
        {
            if (SelectedWidget == null) return;

            var widget = await _widgetService.RefreshAsync(SelectedWidget);

            _dialogService.ShowChild(widget);
        }

        private void OnSelectedWidgetChanged(SelectedWidgetChangedMessage message)
        {
            SelectedWidget = message.Widget;
            SelectedWidgetLayout = message.Widget.Layout;
            SelectedWidgetCode = message.Widget.Code;
        }
    }
}
