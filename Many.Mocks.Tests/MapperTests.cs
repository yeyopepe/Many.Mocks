using Many.Mocks.Tests.TestClasses;
using Moq;
using NUnit.Framework;
using System;
using static Many.Mocks.MockItem;

namespace Many.Mocks.Tests
{
    [TestFixture]
    class MapperTests
    {
        [TestCase(true)]
        [TestCase(false)]
        public void Convert_Mock_ReturnsMockDetails(bool isInterface)
        {
            var result = default(MockDetail);
            Type mockType = null;
            Mock m = null;
            if (isInterface)
            {
                m = new Mock<IClass2>();
                mockType = typeof(IClass2);
            }
            else
            {
                m = new Mock<ImplIClass2_Ctor1>();
                mockType = typeof(ImplIClass2_Ctor1);
            }
            result = m.Convert();


            Assert.IsNotNull(result.Instance, "No instance");
            Assert.AreEqual(mockType, result.Type, "Invalid mock conversion");
            Assert.AreEqual(isInterface, result.IsInterface, "Invalid mock conversion");            
        }
    }
}
