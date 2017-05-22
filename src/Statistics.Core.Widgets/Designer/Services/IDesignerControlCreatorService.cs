namespace Statistics.Core.Widgets.Designer
{
    public interface IDesignerControlCreatorService
    {
        string InsertControlSnippet(ControlCreator controlCreator, string layoutDefinition, int currentPosition);
    }
}
