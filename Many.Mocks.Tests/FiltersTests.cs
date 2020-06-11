using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using Moq.Protected;
using Many.Mocks.Exceptions;

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
        [Test]
        public void FindFirst_ReturnsMock()
        {
            var mocks = typeof(TestClasses.IClass4).GetMocksFrom("Method");
            var result = mocks.First<TestClasses.IClass1>();

            Assert.IsNotNull(result, "Mock not found");
        }       
        [Test]
        public void FindFirst_NotFound_ThrowsException()
        {
            var mocks = typeof(TestClasses.IClass4).GetMocksFrom("Method");
            Assert.Throws<ValidMockNotFoundException>(() => mocks.First<TestClasses.IClass5>());
        }
        [Test]
        public void Count_ValidMocks_ReturnsCount()
        {
            var mocks = typeof(TestClasses.IClass4).GetMocksFrom("Method");
            var result2 = mocks.Count<TestClasses.IClass1>();
            var result1 = mocks.Count<TestClasses.IClass2>();
            var result0 = mocks.Count<TestClasses.IClass3>();

            Assert.AreEqual(1, result1);
            Assert.AreEqual(2, result2);
            Assert.AreEqual(0, result0);
        }
    }
}
