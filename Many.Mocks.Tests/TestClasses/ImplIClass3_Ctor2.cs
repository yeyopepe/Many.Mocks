using System;

namespace Many.Mocks.Tests.TestClasses
{
    public  class ImplIClass3_Ctor2 :IClass3
    {
        public static int ValidMocksInConstructor = 2;
        public static int NotValidMocksInConstructor = 0;

        IClass1 _class1;
        IClass2 _class2;
        public ImplIClass3_Ctor2(IClass1 class1, IClass2 class2)
        {
            _class1 = class1;
            _class2 = class2;
        }
        public bool FirstFromIClass1Dependency()
        {
            return _class1.First();
        }
        public bool FirstFromIClass2Dependency()
        {
            return _class2.First();
        }
        public bool Third(SealedClass sealedC)
        {
            throw new NotImplementedException();
        }
    }
}
