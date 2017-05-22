namespace Statistics.Core.Widgets.Designer
{
    public class DesignerControlCreatorService : IDesignerControlCreatorService
    {
        public string InsertControlSnippet(ControlCreator controlCreator, string layoutDefinition, int currentPosition)
        {
            var control = controlCreator.Create();

            var content = layoutDefinition.Insert(currentPosition, control.Control);

            var stylePosition = FindStylePosition(content);

            return content.Insert(stylePosition, control.Style);
        }

        private int FindStylePosition(string content)
        {
            var resourcesTag = ".Resources>";
            if (content.IndexOf(resourcesTag) < 0) resourcesTag = ".Resources >";
            var index = content.IndexOf(resourcesTag);
            return (index >0) 
                ? content.IndexOf(resourcesTag) + resourcesTag.Length
                : content.Length - 1;
        }
    }
}
