using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq.Protected;
namespace Many.Mocks.Tests
{
    [TestFixture]
    partial class FiltersTests
    {
        [Test]
        public void ExtractDistinctMocks_FromOneMethod_WithDuplicates_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass4).GetMocksFrom("method");

            Assert.AreEqual(TestClasses.IClass4.ValidMocksInMethod, mocks.Mocks.Count(), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass4.DifferentMockTypes, mocks.Distinct().Mocks.Count(), "Number of distinct mocks is not valid");
        }
        [Test]
        public void ExtractDistinctMocks_FromSeveralMethods_WithDuplicates_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");
            Assert.AreEqual(TestClasses.IClass5.ValidMocksInMethod, mocks.Mocks.Count(), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass5.DifferentMockTypes, mocks.Distinct().Mocks.Count(), "Number of distinct mocks is not valid");
        } 
        [Test]
        public void ExtractDistinctMocks_OnlyValid_WithErrors_ReturnsNothing()
        {
            var mocks = typeof(TestClasses.IClass3).GetMocksFrom("Third");
            Assert.AreEqual(0, mocks.Distinct(true).Mocks.Count(), "Number of valid mocks does not match");
        }
        [Test]
        public void ExtractDistinctMocks_All_WithErrors_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass3).GetMocksFrom("Third");
            Assert.AreEqual(TestClasses.IClass3.NotValidMocksInMethod, mocks.Distinct(false).Mocks.Count(), "Number of valid mocks does not match");
        }
        [Test]
        public void Find_OnlyValidMocks_MocksExist_ReturnsMock()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");
            var result = mocks.TryFind(out IList<Moq.Mock<TestClasses.IClass1>> found);

            Assert.IsTrue(result, "Error trying finding mocks");
            Assert.AreEqual(TestClasses.IClass5.IClass1Mocks, found.Count(), "Number of found mocks is not right");
        }
        [Test]
        public void Find_OnlyValidMocks_MocksMissing_ReturnsNothing()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");
            var result = mocks.TryFind(out IList<Moq.Mock<TestClasses.IClass3>> found);

            Assert.IsFalse(result, "Error trying finding mocks");
        }
        [Test]
        public void Find_AllMocks_MocksMissing_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass3).GetMocksFrom("Third");
            var result = mocks.TryFind(out IList<Moq.Mock<TestClasses.SealedClass>> found, false);

            Assert.IsTrue(result, "Error trying finding mocks");
        }
    }
}
