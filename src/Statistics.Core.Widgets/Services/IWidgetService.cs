using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statistics.Core.Widgets
{
    public interface IWidgetService
    {
        Task<IList<WidgetItem>> LoadAsync();
#warning todo: change to Task<IList<WidgetItem>> RefreshAsync(...)
        Task Refresh(IList<WidgetItem> widgets);
        Task<WidgetItem> RefreshAsync(WidgetItem widget);
    }
}
