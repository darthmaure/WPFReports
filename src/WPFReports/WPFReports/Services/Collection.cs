using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace WPFReports.Services
{
    public interface ICollectectionCreatorService
    {
        ObservableCollection<T> Create<T>(IEnumerable<T> source);
    }

    public sealed class CollectionCreatorService : ICollectectionCreatorService
    {
        public ObservableCollection<T> Create<T>(IEnumerable<T> source)
        {
            return source == null || !source.Any()
                ? new ObservableCollection<T>()
                : new ObservableCollection<T>(source);
        }
    }
}
