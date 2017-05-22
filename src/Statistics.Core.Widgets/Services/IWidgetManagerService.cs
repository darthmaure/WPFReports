using System.Collections.Generic;
using System.Threading.Tasks;

namespace Statistics.Core.Widgets
{
    public interface IWidgetManagerService
    {
        Task<IList<WidgetItem>> LoadAsync();
        Task SaveAsync(IEnumerable<WidgetItem> items);
        void Delete(WidgetItem item);
        WidgetItem CreateNew();
    }
}
