using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Many.Mocks.Tests
{
    [TestFixture]
    partial class ExtensionsTests
    {
        [Test]
        public void GetDistinctMocks_FromMethodWithDuplicates_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass4).GetMocksFrom("method");

            Assert.AreEqual(TestClasses.IClass4.ValidMocksInMethod, mocks.Mocks.Count(), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass4.DifferentMockTypes, mocks.Mocks.Distinct().Count(), "Number of distinct mocks is not valid");
        }
        [Test]
        public void GetDistinctMocks_FromMethodsWithSameTypes_ReturnsMocks()
        {
            var mocks = typeof(TestClasses.IClass5).GetMocksFrom("method");

            Assert.AreEqual(TestClasses.IClass5.ValidMocksInMethod, mocks.Mocks.Count(), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass5.DifferentMockTypes, mocks.Mocks.Distinct().Count(), "Number of distinct mocks is not valid");
        }
    }
}
