using Many.Mocks.Exceptions;
using NUnit.Framework;
using System.Linq;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public class MocksTests_Invoke
    {
        [Test]
        public void Invoke_FromMocks_ValidParameters_ReturnsResult()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("second")
                                                                .Mocks.Select(p => p.Details);
            
            Assert.IsTrue(mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("second", obj), "Invoked method has something wrong");
        }
        [Test]
        public void Invoke_FromMocks_InvalidMethod_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("second")
                                                                .Mocks.Select(p => p.Details);

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("notexist", obj);
            });
        }
        [Test]
        public void Invoke_FromMocks_SignatureDoesNotMatch_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("second")
                                                                .Mocks.Select(p => p.Details);
            mocks = mocks.Where(p => false); //empty list

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("second", obj);
            });
        }

        [Test]
        public void Invoke_FromBag_ValidParameters_ReturnsResult()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("second");

            Assert.IsTrue(mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("second", obj), "Invoked method has something wrong");
        }
        [Test]
        public void Invoke_FromBag_InvalidMethod_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("second");

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("notexist", obj);
            });
        }
        [Test]
        public void Invoke_FromBag_SignatureDoesNotMatch_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("second");
            mocks.Mocks = mocks.Mocks.Where(p =>  false); //empty list

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("second", obj);
            });
        }


        [Test]
        public void VoidInvoke_FromMocks_ValidParameters_EndsWithoutException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("voidsecond")
                                                                .Mocks.Select(p => p.Details);

            Assert.DoesNotThrow(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1>("voidsecond", obj);
            });
        }
        [Test]
        public void VoidInvoke_FromMocks_InvalidMethod_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("voidsecond")
                                                                .Mocks.Select(p => p.Details);

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("notexist", obj);
            });
        }
        [Test]
        public void VoidInvoke_FromMocks_SignatureDoesNotMatch_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("voidsecond")
                                                                .Mocks.Select(p => p.Details);
            mocks = mocks.Where(p => false); //empty list

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("voidsecond", obj);
            });
        }

        [Test]
        public void VoidInvoke_FromBag_ValidParameters_EndsWithoutException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("voidsecond");

            Assert.DoesNotThrow(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1>("voidsecond", obj);
            });
        }
        [Test]
        public void VoidInvoke_FromBag_InvalidMethod_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("voidsecond");

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("notexist", obj);
            });
        }
        [Test]
        public void VoidInvoke_FromBag_SignatureDoesNotMatch_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2_Ctor1(null);
            var mocks = typeof(TestClasses.ImplIClass2_Ctor1).GetMocksFrom("voidsecond");
            mocks.Mocks = mocks.Mocks.Where(p => false); //empty list

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2_Ctor1, bool>("voidsecond", obj);
            });
        }
    }
    
}
