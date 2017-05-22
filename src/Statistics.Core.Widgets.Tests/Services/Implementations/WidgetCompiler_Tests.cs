using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statistics.Core.Widgets.Services.Tests
{
    [TestClass()]
    public class WidgetCompiler_Tests
    {
        private IWidgetCompiler CreateService()
        {
            return new WidgetCompiler(new string[] 
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
            });
        }

        [TestMethod]
        public void ShouldFailCompileWhenCodeIsEmpty()
        {
            //arrange
            var service = CreateService();
            var code = "";

            //act
            var result = service.Compile(code);

            //assert
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Success);
            Assert.IsTrue(result.Errors.Any());
            Assert.IsNull(result.Result);
        }

        [TestMethod()]
        public void ShouldCompileCode()
        {
            //arrange
            var service = CreateService();
            var code = "public void  Run() { }";

            //act
            var result = service.Compile(code);

            //assert
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Success);
            Assert.IsNotNull(result.Result);
        }

        [TestMethod]
        public void ShouldReturnCompiledWidgetObject()
        {
            //arrange
            var service = CreateService();
            var code = "public void Run() {  }";

            //act
            var result = service.Compile(code);
            var widget = result.Result as IWidget;

            //assert
            Assert.IsNotNull(widget);
        }

        [TestMethod]
        [ExpectedException(typeof(NotImplementedException), AllowDerivedTypes = false)]
        public void ShouldInvokeCustomCode()
        {
            //arrange
            var service = CreateService();
            var code = "public void Run() { throw new NotImplementedException(\"Test should throw exception.\"); }";

            //act
            var result = service.Compile(code);
            var widget = result.Result as IWidget;

            //assert
            Assert.IsNotNull(widget);
            widget.Run();
        }
    }
}