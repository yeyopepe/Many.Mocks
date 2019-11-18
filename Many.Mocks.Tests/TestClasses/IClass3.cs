using System;
using System.Collections.Generic;
using System.Text;

namespace Many.Mocks.Tests.TestClasses
{
    public  interface IClass3
    {
        public static int ValidMocksInMethod = 0;
        public static int NotValidMocksInMethod = 1;

        bool Third(SealedClass p1);
    }
}
