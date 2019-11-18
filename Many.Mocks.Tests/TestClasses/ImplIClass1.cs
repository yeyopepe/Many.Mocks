using System;

namespace Many.Mocks.Tests.TestClasses
{
    public class ImplIClass1 : IClass1
    {
        public static int ValidMocksInConstructor = 0;
        public static int NotValidMocksInConstructor = 0;
        public void First()
        {
            throw new NotImplementedException();
        }
    }
}
