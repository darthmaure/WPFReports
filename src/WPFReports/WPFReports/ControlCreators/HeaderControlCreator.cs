using Statistics.Core.Widgets;
using Statistics.Core.Widgets.Designer;

namespace WPFReports.ControlCreators
{
    public class HeaderControlCreator : ControlCreator
    {
        private const string typeName = "HeaderControl";

        public override DesignerControlDefinition Create(INameCreationService namingService)
        {
            var uniqueName = namingService.CreateName(typeName);

            return new DesignerControlDefinition
            {
                Control = string.Format(Properties.Snippets.HeaderControlControl, uniqueName),
                Style = string.Format(Properties.Snippets.HeaderControlStyle, uniqueName)
            };
        }
    }
}
