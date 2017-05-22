using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Statistics.Core.Widgets.Services.Tests
{
    [TestClass]
    public class NameCreationService_Tests
    {
        private INameCreationService CreateService() => new NameCreationService();


        [TestMethod]
        public void ShouldCreateSomeName()
        {
            //arrange
            var service = CreateService();
            var prefix = "test";

            //act
            var name = service.CreateName(prefix);

            //assert
            Assert.IsNotNull(name);
            Assert.IsTrue(name.Length > prefix.Length);
            Assert.IsTrue(name.Contains(prefix));
        }

        [TestMethod]
        public void ShouldCreateUniqueNames()
        {
            //arrange
            var service = CreateService();
            var prefix = "test";

            //act
            var name1 = service.CreateName(prefix);
            var name2 = service.CreateName(prefix);
            var name3 = service.CreateName(prefix);

            //assert
            Assert.AreNotEqual(name1, name2);
            Assert.AreNotEqual(name1, name3);
            Assert.AreNotEqual(name2, name3);

        }

        [TestMethod]
        public void ShouldCreateNameWhenPrefixIsEmpty()
        {
            //arrange
            var service = CreateService();
            string prefix = null;

            //act
            var name = service.CreateName(prefix);

            //assert
            Assert.IsNotNull(name);
            Assert.IsTrue(name.StartsWith("_"));
        }
    }
}
