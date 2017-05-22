using Microsoft.VisualStudio.TestTools.UnitTesting;
using Statistics.Core.Widgets.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Statistics.Core.Widgets.Services.Tests
{
    [TestClass()]
    public class WidgetManagerService_Tests
    {
        private string sourcesDirectory;
        //private string sourceFileName = "test_widget.wget";
        private const string _defaultCode = "public class TestWidget {}";
        private const string _defaultLayout = "<Grid><TextBlock Text=\"Test title.\"/></Grid>";


        private IWidgetManagerService CreateService()
        {
            return new WidgetManagerService(sourcesDirectory, _defaultCode, _defaultLayout);
        }


        [TestInitialize]
        public void Init()
        {
            var dir = Directory.GetCurrentDirectory();
            sourcesDirectory = Path.Combine(dir, "Sources");
        }

        [TestCleanup]
        public void Clean()
        {
            Directory
                .EnumerateFiles(sourcesDirectory)
                .ToList()
                .ForEach(d => File.Delete(d));

        }

        [TestMethod()]
        public void ShouldCreateNew()
        {
            //arrange
            var service = CreateService();

            //act
            var widget = service.CreateNew();

            //assert
            Assert.IsNotNull(widget);
            Assert.AreEqual(widget.Code, _defaultCode);
            Assert.AreEqual(widget.Layout, _defaultLayout);
        }

        [TestMethod()]
        public async Task ShouldDeleteItem()
        {
            //arrange
            var service = CreateService();
            var items = await service.LoadAsync();
            System.Diagnostics.Debug.WriteLine("Items loaded: " + items.Count);
            var count = items.Count;
            var itemToDelete = items.First();

            //act
            service.Delete(itemToDelete);

            await Task.Delay(10);

            var countAfterDelete = Directory.EnumerateFiles(sourcesDirectory).Count();

            //assert
            Assert.AreEqual(countAfterDelete, count - 1, $"Count: {count}, after delete: {countAfterDelete}");
        }

        [TestMethod()]
        public async Task ShouldLoadWidgetsAsync()
        {
            //arrange
            var service = CreateService();
            var count = Directory.EnumerateFiles(sourcesDirectory).Count();

            //act
            var items = await service.LoadAsync();

            //assert
            Assert.IsTrue(items != null);
            Assert.AreEqual(items.Count, count);
            Assert.IsTrue(items.All(d => !string.IsNullOrEmpty(d.Name)));
            Assert.IsTrue(items.All(d => !string.IsNullOrEmpty(d.Layout)));
        }

        [TestMethod()]
        public async Task ShouldSaveAsync()
        {
            //arrange
            var service = CreateService();
            var widget = new WidgetItem
            {
                Layout = _defaultLayout,
                Code = _defaultLayout,
                Name = "Test saving widget"
            };

            //act
            await service.SaveAsync(new[] { widget });

            await Task.Delay(10);

            var widgets = await service.LoadAsync();

            //assert
            Assert.IsTrue(widgets.Any());
            Assert.IsNotNull(widgets.Single(d => d.Name == widget.Name));
        }
    }
}