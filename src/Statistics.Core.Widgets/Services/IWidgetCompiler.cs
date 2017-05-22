using PKCode.Scripting;

namespace Statistics.Core.Widgets
{
    public interface IWidgetCompiler
    {
        ExecutionResult Compile(string code);
    }
}
