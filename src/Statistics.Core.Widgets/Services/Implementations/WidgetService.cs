using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statistics.Core.Widgets.Services
{
    public class WidgetService : IWidgetService
    {
        public WidgetService(IWidgetManagerService managementService, IWidgetCompiler compiler)
        {
            _managementService = managementService;
            _compiler = compiler;
        }

        private readonly IWidgetManagerService _managementService;
        private readonly IWidgetCompiler _compiler;

        public async Task<IList<WidgetItem>> LoadAsync()
        {
            var items = await _managementService.LoadAsync();

            await Refresh(items);

            return items;
        }

        public async Task Refresh(IList<WidgetItem> widgets)
        {
            await Task.Run(() =>
            {
                foreach (var item in widgets)
                {
                    var context = _compiler.Compile(item.Code);
                    item.Context = context.Result as IWidget;
                    item.Context?.Run();
                }
            });
        }

        public async Task<WidgetItem> RefreshAsync(WidgetItem widget)
        {
            return await Task.Run(() => 
            {
                var context = _compiler.Compile(widget.Code);
                widget.Context = context.Result as IWidget;
                widget.Context.Run();
                return widget;
            });
        }
    }
}
