namespace Statistics.Core.Widgets.Designer
{
    public class DesignerControlCreatorService : IDesignerControlCreatorService
    {
        public DesignerControlCreatorService(INameCreationService namingService)
        {
            _namingService = namingService;
        }

        private readonly INameCreationService _namingService;

        public string InsertControlSnippet(ControlCreator controlCreator, string layoutDefinition, int currentPosition)
        {
            var control = controlCreator.Create(_namingService);

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
