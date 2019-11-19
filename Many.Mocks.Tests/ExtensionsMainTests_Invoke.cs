using Many.Mocks.Exceptions;
using NUnit.Framework;

namespace Many.Mocks.Tests
{
    [TestFixture]
    public partial class ExtensionsTests
    {
        [Test]
        public void Invoke_ValidParameters_ReturnsResult()
        {
            var obj = new TestClasses.ImplIClass2(null);
            var mocks = typeof(TestClasses.ImplIClass2).GetMocksFrom("second").ExtractDistinct();

            Assert.IsTrue(mocks.Invoke<TestClasses.ImplIClass2, bool>("second", obj), "Invoked method has something wrong");
        }
        [Test]
        public void Invoke_InvalidMethod_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2(null);
            var mocks = typeof(TestClasses.ImplIClass2).GetMocksFrom("second").ExtractDistinct();

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2, bool>("notexist", obj);
            });
        }
        [Test]
        public void Invoke_SignatureDoesNotMatch_ReturnsException()
        {
            var obj = new TestClasses.ImplIClass2(null);
            var mocks = typeof(TestClasses.ImplIClass2).GetMocksFrom("second").ExtractDistinct();
            mocks.Clear();

            Assert.Throws<MethodNotFoundException>(() =>
            {
                mocks.Invoke<TestClasses.ImplIClass2, bool>("second", obj);
            });
        }
    }
    
}
