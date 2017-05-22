using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Statistics.Core.Widgets.Services
{
    public class WidgetManagerService : IWidgetManagerService
    {
        public WidgetManagerService(string widgetsDirectory, string defaultCode, string defaultLayout)
        {
            _widgetsDirectory = new DirectoryInfo(widgetsDirectory);
            _defaultCode = defaultCode;
            _defaultLayout = defaultLayout;
        }

        private readonly DirectoryInfo _widgetsDirectory;
        private readonly string _defaultCode;
        private readonly string _defaultLayout;
        private const string _fileExtension = ".wget";


        public WidgetItem CreateNew()
        {
            return new WidgetItem
            {
                CreateDate = DateTime.Now,
                Code = _defaultCode,
                Layout = _defaultLayout
            };
        }

        public void Delete(WidgetItem item)
        {
            File.Delete(GetFileName(item));
        }

        public async Task<IList<WidgetItem>> LoadAsync()
        {
            return await Task.Run(() =>
            {
                var files = _widgetsDirectory.GetFiles(GetSearchPattern());
                return DeserializeFromFiles(files);
            });
        }

        public async Task SaveAsync(IEnumerable<WidgetItem> items)
        {
            await Task.Run(() =>
            {
                var serializer = JsonSerializer.CreateDefault();
                foreach (var item in items)
                {
                    item.Context = null;
                    var file = GetFileName(item);
                    using (var writer = new StreamWriter(file, false))
                    using (var json = new JsonTextWriter(writer))
                    {
                        serializer.Serialize(json, item);
                    }
                }
            });
        }


        private IList<WidgetItem> DeserializeFromFiles(FileInfo[] files)
        {
            var widgets = new List<WidgetItem>();
            var serializer = JsonSerializer.CreateDefault();
            foreach (var file in files)
            {
                WidgetItem item = null;
                using (var reader = new StreamReader(file.FullName))
                using (var json = new JsonTextReader(reader))
                {
                    item = serializer.Deserialize(json, typeof(WidgetItem)) as WidgetItem;
                }
                if (item != null) widgets.Add(item);
            }
            return widgets;
        }
        private string GetFileName(WidgetItem widget) => $"{_widgetsDirectory.FullName}\\{widget.Name}{_fileExtension}";
        private string GetSearchPattern() => $"*{_fileExtension}";
    }
}
