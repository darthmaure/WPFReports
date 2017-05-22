using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statistics.Core.Widgets.Services
{
    [TestClass()]
    public class WidgetService_Tests
    {
        private string sourcesDirectory;

        private IWidgetService CreateService()
        {
            return new WidgetService(
                new WidgetManagerService(sourcesDirectory, string.Empty, string.Empty),
                new WidgetCompiler(new string[]
            {
                "mscorlib.dll",
                "System.dll",
                "System.Core.dll",
                "System.Data.dll",
                "System.Data.DataSetExtensions.dll",
                "System.Xml.dll",
                "System.Xml.Linq.dll",
                "Microsoft.CSharp.dll",
                "Statistics.Core.Widgets.dll"
            }));
        }


        [TestInitialize]
        public void Init()
        {
            var dir = Directory.GetCurrentDirectory();
            sourcesDirectory = Path.Combine(dir, "Sources");
        }

        [TestMethod()]
        public async Task LoadAsyncTest()
        {
            //arrange
            var service = CreateService();

            //act
            var widgets = await service.LoadAsync();

            //assert

            Assert.IsNotNull(widgets);
            Assert.IsTrue(widgets.Any());
            Assert.IsTrue(widgets.All(d => !string.IsNullOrEmpty(d.Layout)));
            Assert.IsTrue(widgets.All(d => (d.Context as IWidget) != null));
        }

        [Ignore]
        [TestMethod()]
        public void RefreshTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod]
        public void RefreshSingleTest()
        {
            Assert.Fail();
        }

        [Ignore]
        [TestMethod()]
        public void CleanTest()
        {
            Assert.Fail();
        }
    }
}