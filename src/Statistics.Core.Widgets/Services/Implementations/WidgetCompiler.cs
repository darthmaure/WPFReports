using PKCode.Scripting;

namespace Statistics.Core.Widgets.Services
{
    public sealed class WidgetCompiler : IWidgetCompiler
    {
        public WidgetCompiler(string[] assemblies)
        {
            _assemblies = assemblies;
        }

        private ICompiler _compiler;
        private readonly string[] _assemblies;

        private void Init()
        {
            if (_compiler != null) return;
            _compiler = Compiler
                .Init()
                .LoadAssemblies(_assemblies)
                .SetEnvironment(new ReportEnvironment())
                .Create();
        }

        public ExecutionResult Compile(string code)
        {
            Init();
            return _compiler.Run(code, new object());
        }
    }
}
