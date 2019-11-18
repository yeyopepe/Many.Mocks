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
        public void Compare_SameTypeLists_ReturnsTrue()
        {
            var l1 = new List<Type> { typeof(String), typeof(int), typeof(double) };
            var l2 = new List<Type> { typeof(String), typeof(int), typeof(double) };

            Assert.IsTrue(l1.IsEquivalentTo(l2));
        }
        [Test]
        public void Compare_DifferentTypeLists_SameTypesDifferentOrder_ReturnsFalse()
        {
            var l1 = new List<Type> { typeof(String), typeof(int), typeof(double) };
            var l2 = new List<Type> { typeof(int), typeof(double), typeof(String) };

            Assert.IsFalse(l1.IsEquivalentTo(l2));
        }
        [Test]
        public void Compare_DifferentTypeLists_DifferentTypes_ReturnsFalse()
        {
            var l1 = new List<Type> { typeof(String), typeof(int), typeof(double) };
            var l2 = new List<Type> { typeof(String), typeof(int), typeof(String) };

            Assert.IsFalse(l1.IsEquivalentTo(l2));
        }
        [Test]
        public void Compare_DifferentTypeLists_DifferentSized_ReturnsFalse()
        {
            var l1 = new List<Type> { typeof(String) };
            var l2 = new List<Type> { typeof(int), typeof(double), typeof(String) };

            Assert.IsFalse(l1.IsEquivalentTo(l2));
        }
       
    }
}
