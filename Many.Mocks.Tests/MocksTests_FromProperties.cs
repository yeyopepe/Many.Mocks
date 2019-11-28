using NUnit.Framework;
using System.Linq;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public class MocksTests_FromProperties
    {
        [Test]
        public void GetMocks_FromTypeProperties_OK()
        {
            var result = typeof(TestClasses.IClass1).GetMocksFromProperties();

            Assert.AreEqual(TestClasses.IClass1.ValidMocksInProperties, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass1.NotValidMocksInProperties, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");
        }
       
    }
    
}
