﻿namespace Many.Mocks.Benchmarks.TestClasses
{
    public class ImplIClass2 :IClass2
    {
        public static int ValidMocksInConstructor = 0;
        public static int NotValidMocksInConstructor = 1;
        public ImplIClass2(SealedClass sealedClass1)
        { 
        }

        public bool First()
        {
            return true;
        }
        public bool Second(IClass2 class2, IClass3 class3)
        {
            return true;
        }
        public void VoidSecond(IClass2 class2, IClass3 class3)
        {
            //Do nothing
        }
    }
}
