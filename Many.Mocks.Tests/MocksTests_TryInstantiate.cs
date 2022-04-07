using Many.Mocks.Tests.TestClasses;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public class MocksTests_TryInstantiate
    {
        [Test]
        public void TryInstantiate_OrderedBag_ReturnsInstance()
        {
            var mocks = typeof(ImplIClass3_Ctor2).GetMocksFromConstructors();

            var parse = mocks.TryInstantiate(out ImplIClass3_Ctor2 result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
        [Test]
        public void TryInstantiate_OrderedBag_RepeatedDependencies_ReturnsInstance()
        {
            var mocks = typeof(ImplIClass3_Ctor2Repeated).GetMocksFromConstructors();

            var parse = mocks.TryInstantiate(out ImplIClass3_Ctor2Repeated result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
        [Test]
        public void TryInstantiate_ReplaceBag_ReturnsInstance()
        {
            var mocks = typeof(ImplIClass3_Ctor2).GetMocksFromConstructors();
            //To check differences because default implementation is false
            var replace = new Mock<IClass2>();
            replace
                .Setup(p => p.First())
                .Returns(false);

            var parse = mocks.TryInstantiate(new List<Mock>() { replace }, out ImplIClass3_Ctor2 result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");

        }

        [Test]
        public void TryInstantiate_OrderedMockDetails_ReturnsInstance()
        {
            var mocks = typeof(ImplIClass3_Ctor2).GetMocksFromConstructors()
                                                    .Mocks
                                                    .Select(p => p.Details);

            var parse = mocks.TryInstantiate(out ImplIClass3_Ctor2 result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
        [Test]
        public void TryInstantiate_OrderedMockDetails_RepeatedDependencies_ReturnsInstance()
        {
            var mocks = typeof(ImplIClass3_Ctor2Repeated).GetMocksFromConstructors()
                                                            .Mocks
                                                            .Select(p => p.Details);

            var parse = mocks.TryInstantiate(out ImplIClass3_Ctor2Repeated result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
        [Test]
        public void TryInstantiate_ReplaceMockDetails_ReturnsInstance()
        {
            var mocks = typeof(ImplIClass3_Ctor2).GetMocksFromConstructors()
                                                    .Mocks
                                                    .Select(p => p.Details);
            //To check differences because default implementation is false
            var replace = new Mock<IClass2>();
            replace
                .Setup(p => p.First())
                .Returns(false);

            var parse = mocks.TryInstantiate(new List<Mock>() { replace }, out ImplIClass3_Ctor2 result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");

            Assert.AreEqual(replace.Object.First(), result.FirstFromIClass2Dependency());
        }

        [Test]
        public void TryInstantiate_OrderedMocks_ReturnsInstance()
        {
            var mocks = new List<Mock>() //From ImplClass3
            {
                new Mock<IClass1>(),
                new Mock<IClass2>()
            };

            var parse = mocks.TryInstantiate(out ImplIClass3_Ctor2 result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");
        }
        [Test]
        public void TryInstantiate_ReplaceMocks_ReturnsInstance()
        {
            var mocks = new List<Mock>() //From ImplClass3
            {
                new Mock<IClass1>(),
                new Mock<IClass2>()
            };
            //To check differences because default implementation is false
            var replace = new Mock<IClass2>();
            replace
                .Setup(p => p.First())
                .Returns(false);

            var parse = mocks.TryInstantiate(new List<Mock>() { replace }, out ImplIClass3_Ctor2 result);

            Assert.IsTrue(parse, "Parse failed");
            Assert.IsNotNull(result, "No instantiation");

            Assert.AreEqual(replace.Object.First(), result.FirstFromIClass2Dependency());
        }

    }

}
