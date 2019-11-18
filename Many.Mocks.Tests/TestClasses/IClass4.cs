namespace Many.Mocks.Tests.TestClasses
{
    interface IClass4
    {
        public static int ValidMocksInMethod = 3;
        public static int DifferentMockTypes = 2;
        void Method(IClass1 p1, IClass2 p2, IClass1 p3);
    }
}
