using System.Collections;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WPFReports.Messages;

namespace WPFReports.ViewModels
{
    public class WidgetCompileResultViewModel : ViewModelBase
    {
        public WidgetCompileResultViewModel()
        {
            Initialize();
        }

        private IEnumerable errors;

        public IEnumerable Errors
        {
            get { return errors; }
            set
            {
                if (value == errors) return;
                errors = value;
                RaisePropertyChanged();
            }
        }

        private void Initialize()
        {
            Messenger.Default.Register<WidgetCompileResultMessage>(this, OnCompileResult);
        }

        private void OnCompileResult(WidgetCompileResultMessage results)
        {
            Errors = results?.CompileResult?.Errors;

#warning todo: finish
        }
    }
}
