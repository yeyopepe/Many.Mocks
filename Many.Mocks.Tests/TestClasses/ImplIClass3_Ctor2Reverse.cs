using System;

namespace Many.Mocks.Tests.TestClasses
{
    public  class ImplIClass3_Ctor2Reverse :IClass3
    {
        public static int ValidMocksInConstructor = 2;
        public static int NotValidMocksInConstructor = 0;
        public ImplIClass3_Ctor2Reverse(IClass2 class1, IClass1 class2)
        { 
        }

        public bool Third(SealedClass sealedC)
        {
            throw new NotImplementedException();
        }
    }
}
