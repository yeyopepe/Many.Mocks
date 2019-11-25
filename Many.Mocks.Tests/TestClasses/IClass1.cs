namespace Many.Mocks.Tests.TestClasses
{
    public interface IClass1
    {
        public static int ValidMocksInMethod = 0;
        public static int NotValidMocksInMethod = 0;
        public static int ValidMocksInProperties = 2;
        public static int NotValidMocksInProperties = 2;

        IClass2 PropIClass2 { get; }
        IClass3 PropIClass3 { get; }
        SealedClass PropSealedClass { get; }
        int PropInt { get;
        }
        void First();
    }
}
