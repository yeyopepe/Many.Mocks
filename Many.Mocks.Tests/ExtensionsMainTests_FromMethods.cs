using Many.Mocks.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public partial class ExtensionsTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void GetMocks_FromMethod_OK(bool caseSensitive)
        {
            var method = caseSensitive ? "First" : "first";
            var result = typeof(TestClasses.IClass1).GetMocksFrom(method);

            Assert.AreEqual(TestClasses.IClass1.ValidMocksInMethod, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass1.NotValidMocksInMethod, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");

            Assert.AreEqual(false, result.Mocks.Any(p => !p.Generated), "Some errors found");
            Assert.AreEqual(false, result.Mocks.Any(p => p.Error != null), "Some incorrect exceptions found");
        }
        [TestCase(true)]
        [TestCase(false)]
        public void GetMocks_FromMethod_WithNotValidClass_ReturnsError(bool caseSensitive)
        {
            var method = caseSensitive ? "Third" : "third";
            var result = typeof(TestClasses.IClass3).GetMocksFrom(method);

            Assert.AreEqual(TestClasses.IClass3.ValidMocksInMethod, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass3.NotValidMocksInMethod, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");

            Assert.AreEqual(false, result.Mocks.Any(p => p.Generated), "Some errors found");
        }
        [TestCase(true)]
        [TestCase(false)]
        public void GetMocks_FromMethod_GivenSignature_OK(bool caseSensitive)
        {
            var method = caseSensitive ? "Second" : "second";
            var result = typeof(TestClasses.IClass2).GetMocksFrom(method, new List<Type> { typeof(TestClasses.IClass2), typeof(TestClasses.IClass3) });

            Assert.AreEqual(TestClasses.IClass2.ValidMocksInMethod, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.IClass2.NotValidMocksInMethod, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");

            Assert.AreEqual(false, result.Mocks.Any(p => !p.Generated), "Some errors found");
            Assert.AreEqual(false, result.Mocks.Any(p => p.Error != null), "Some incorrect exceptions found");
        }
        [TestCase(true)]
        [TestCase(false)]
        public void GetMocks_FromMethod_GivenInvalidSignature_ReturnsException(bool caseSensitive)
        {
            var method = caseSensitive ? "Third" : "third";
            Assert.Throws<MethodNotFoundException>(() =>
            {
                _ = typeof(TestClasses.IClass3).GetMocksFrom(method, new List <Type> { typeof(TestClasses.IClass1) });
            });
        }
    }
    
}
