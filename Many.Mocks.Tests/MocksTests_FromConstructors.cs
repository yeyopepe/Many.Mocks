using Many.Mocks.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public class ExtensionsMainTests_FromConstructors
    {
        [Test]
        public void GetMocks_FromConstructors_OK()
        {
            var result = typeof(TestClasses.ImplIClass3).GetMocksFromConstructors();
            
            Assert.AreEqual(TestClasses.ImplIClass3.ValidMocksInConstructor, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.ImplIClass3.NotValidMocksInConstructor, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");

            Assert.AreEqual(false, result.Mocks.Any(p => !p.Generated), "Some errors found");
            Assert.AreEqual(false, result.Mocks.Any(p => p.Error != null), "Some incorrect exceptions found");
        }
        [Test]
        public void GetMocks_FromConstructors_GivenSignature_OK()
        {
            var result = typeof(TestClasses.ImplIClass3).GetMocksFromConstructors(new List<Type> { typeof(TestClasses.IClass1), typeof(TestClasses.IClass2) });

            Assert.AreEqual(TestClasses.ImplIClass3.ValidMocksInConstructor, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.ImplIClass3.NotValidMocksInConstructor, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");

            Assert.AreEqual(false, result.Mocks.Any(p => !p.Generated), "Some errors found");
            Assert.AreEqual(false, result.Mocks.Any(p => p.Error != null), "Some incorrect exceptions found");
        }
        [Test]
        public void GetMocks_FromConstructors_GivenInvalidSignature_ReturnsException()
        {
            Assert.Throws<MethodNotFoundException>(() =>
            {
                _ = typeof(TestClasses.ImplIClass3).GetMocksFromConstructors(new List<Type> { typeof(TestClasses.IClass1) });
            });            
        }

        [Test]
        public void GetMocks_FromConstructors_WithNotValidClass_ReturnsError()
        {
            var result = typeof(TestClasses.ImplIClass2).GetMocksFromConstructors();

            Assert.AreEqual(TestClasses.ImplIClass2.ValidMocksInConstructor, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.ImplIClass2.NotValidMocksInConstructor, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");

            Assert.AreEqual(false, result.Mocks.Any(p => p.Generated), "Some errors found");
        }
        [Test]
        public void GetMocks_FromConstructors_OfPrivateClass_ReturnsError()
        {
            var result = typeof(TestClasses.PrivateClass).GetMocksFromConstructors();

            Assert.AreEqual(TestClasses.PrivateClass.ValidMocksInConstructor, result.Mocks.Count(p => p.Generated), "Number of valid mocks does not match");
            Assert.AreEqual(TestClasses.PrivateClass.NotValidMocksInConstructor, result.Mocks.Count(p => !p.Generated), "Number of invalid mocks does not match");

            Assert.AreEqual(false, result.Mocks.Any(p => p.Generated), "Some errors found");
        }
    }
    
}
