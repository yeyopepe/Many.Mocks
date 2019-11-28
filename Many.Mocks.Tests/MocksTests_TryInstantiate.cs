using NUnit.Framework;
using System.Linq;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public class MocksTests_TryInstantiate
    {
        [Test]
        public void TryInstantiate_OrderedMockSignature_ReturnsInstance()
        {
            var mocks = typeof(TestClasses.ImplIClass3).GetMocksFromConstructors()
                                                        .Distinct().Mocks
                                                        .Select(p => p.Details);
            
            var parse = mocks.UseToTryInstantiate(out TestClasses.ImplIClass3 result);
            
            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
        [Test]
        public void TryInstantiate_UnorderedMockSignature_ReturnsInstance()
        {
            var mocks = typeof(TestClasses.ImplIClass3Bis).GetMocksFromConstructors()
                                                        .Distinct().Mocks
                                                        .Select(p => p.Details);
            var parse = mocks.UseToTryInstantiate(out TestClasses.ImplIClass3Bis result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
    }
    
}
