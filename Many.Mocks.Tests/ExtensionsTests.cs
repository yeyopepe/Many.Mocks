using NUnit.Framework;
using System.Linq;

namespace Many.Mocks.Tests
{
    [TestFixture]
    partial class ExtensionsTests
    {
        [Test]
        public void ExtractDistinctMocks_FromMethodWithDuplicates_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass4).GetMocksFrom("method");

            Assert.AreEqual(TestClasses.IClass4.ValidMocksInMethod, mocks.Mocks.Count(), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass4.DifferentMockTypes, mocks.ExtractDistinct().Count(), "Number of distinct mocks is not valid");
        }
        [Test]
        public void ExtractDistinctMocks_FromMethodsWithSameTypes_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");

            Assert.AreEqual(TestClasses.IClass5.ValidMocksInMethod, mocks.Mocks.Count(), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass5.DifferentMockTypes, mocks.ExtractDistinct().Count(), "Number of distinct mocks is not valid");
        }
        [Test]
        public void ExtractMocks_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");

            Assert.AreEqual(TestClasses.IClass5.ValidMocksInMethod, mocks.Mocks.Count(), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass5.ValidMocksInMethod, mocks.Extract().Count(), "Number of valid extracted mocks does not match");
        }
    }
}
