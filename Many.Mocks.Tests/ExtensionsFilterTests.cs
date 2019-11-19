using NUnit.Framework;
using System.Collections.Generic;
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
        [Test]
        public void Find_OnlyValidMocks_MocksExist_ReturnsMock()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");
            var result = mocks.TryFind(out IList<TestClasses.IClass1> found);

            Assert.IsTrue(result, "Error trying finding mocks");
            Assert.AreEqual(TestClasses.IClass5.IClass1Mocks, found.Count(), "Number of found mocks is not right");
        }
        [Test]
        public void Find_OnlyValidMocks_MocksMissing_ReturnsNothing()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");
            var result = mocks.TryFind(out IList<TestClasses.IClass3> found);

            Assert.IsFalse(result, "Error trying finding mocks");
        }
        [Test]
        public void Find_AllMocks_MocksMissing_ReturnsNothing()
        {
            var mocks = typeof(TestClasses.IClass3).GetMocksFrom("Third");
            var result = mocks.TryFind(out IList<TestClasses.SealedClass> found, false);

            Assert.IsFalse(result, "Error trying finding mocks");
        }
    }
}
