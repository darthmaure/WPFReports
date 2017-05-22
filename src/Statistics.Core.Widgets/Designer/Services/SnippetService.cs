namespace Statistics.Core.Widgets.Designer
{
    public class SnippetService : ISnippetService
    {
        public string Insert(string code, string snippet, int position)
        {
            return (position > 0 && position >= 0 && position < code.Length)
                ? code.Insert(position, snippet)
                : code;
        }
    }
}
