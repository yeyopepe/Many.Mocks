namespace Many.Mocks.Tests.TestClasses
{
    interface IClass5
    {
        public static int ValidMocksInMethod = 5;
        public static int DifferentMockTypes = 2;
        public static int IClass1Mocks = 3;
        public static int IClass2Mocks = 2;
        void Method(IClass1 p1, IClass2 p2);
        void Method(IClass1 p1, IClass2 p2, IClass1 p3);
    }
}
