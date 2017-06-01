using GalaSoft.MvvmLight.Messaging;
using PKCode.Scripting;
using Statistics.Core.Widgets;

namespace WPFReports.Messages
{
    public sealed class SelectedWidgetChangedMessage : MessageBase
    {
        public WidgetItem Widget { get; set; }
    }

    public sealed class WidgetCompileResultMessage : MessageBase
    {
        public ExecutionResult CompileResult { get; set; }
    }
}
