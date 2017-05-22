using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statistics.Core.Widgets.Designer
{
    [TestClass()]
    public class SnippetService_Tests
    {
        private ISnippetService CreateService() => new SnippetService();


        [TestMethod()]
        public void ShouldReturnUnchangedWhenNoSnippedPassed()
        {
            //arrange
            var service = CreateService();
            var code = "test code";

            //act
            var result = service.Insert(code, null, 0);

            //assert
            Assert.AreEqual(result, code);
        }

        [TestMethod()]
        public void ShouldReturnUnchangedWhenPositionIsIncorrect()
        {
            //arrange
            var service = CreateService();
            var code = "test code";

            //act
            var result = service.Insert(code, "test", -1);

            //assert
            Assert.AreEqual(result, code);
        }

        [TestMethod()]
        public void ShouldReturnUnchangedWhenCodeIsEmpty()
        {
            //arrange
            var service = CreateService();
            var code = string.Empty;

            //act
            var result = service.Insert(code, "test", 1);

            //assert
            Assert.AreEqual(result, code);
        }

        [TestMethod]
        public void ShouldReturnCodeWithSnippet()
        {
            //arrange
            var service = CreateService();
            #region Code definitions
            var code = @"public class TestReport 
{

}";
            var snippet = @"private int myVar;

public int MyProperty
{
    get { return myVar; }
    set
    {
        if (value == myVar) return;
        myVar = value;
        RaisePropertyChanged();
    }
}";
            var expected = @"public class TestReport 
{
private int myVar;

public int MyProperty
{
    get { return myVar; }
    set
    {
        if (value == myVar) return;
        myVar = value;
        RaisePropertyChanged();
    }
}
}";
            #endregion
            //act
            var result = service.Insert(code, snippet, 29);

            //assert
            Assert.AreEqual(result, expected);

        }
    }
}