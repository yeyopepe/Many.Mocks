using NUnit.Framework;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public partial class ExtensionsTests
    {
        [Test]
        public void TryInstantiate_OrderedMockSignature_ReturnsInstance()
        {
            var mocks = typeof(TestClasses.ImplIClass3).GetMocksFromConstructors().Mocks.Distinct();
            var parse = mocks.TryInstantiate(out TestClasses.ImplIClass3 result);
            
            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
        [Test]
        public void TryInstantiate_UnorderedMockSignature_ReturnsInstance()
        {
            var mocks = typeof(TestClasses.ImplIClass3Bis).GetMocksFromConstructors().Mocks.Distinct();
            var parse = mocks.TryInstantiate(out TestClasses.ImplIClass3Bis result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
    }
    
}
