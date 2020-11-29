namespace Many.Mocks.Tests.TestClasses
{
    public  interface IClass2
    {
        public static int ValidMocksInMethod = 2;
        public static int NotValidMocksInMethod = 0;

        bool First();
        bool Second(IClass2 p1, IClass3 p2);
    }
}
