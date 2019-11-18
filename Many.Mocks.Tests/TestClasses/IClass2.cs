using System;
using System.Collections.Generic;
using System.Text;

namespace Many.Mocks.Tests.TestClasses
{
    public  interface IClass2
    {
        public static int ValidMocksInMethod = 2;
        public static int NotValidMocksInMethod = 0;
        bool Second(IClass2 p1, IClass3 p2);
    }
}
