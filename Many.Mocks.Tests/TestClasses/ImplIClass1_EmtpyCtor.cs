﻿using System;

namespace Many.Mocks.Tests.TestClasses
{
    public class ImplIClass1_EmtpyCtor : IClass1
    {
        public static int ValidMocksInConstructor = 0;
        public static int NotValidMocksInConstructor = 0;

        public IClass2 PropIClass2 => throw new NotImplementedException();

        public IClass3 PropIClass3 => throw new NotImplementedException();

        public SealedClass PropSealedClass => throw new NotImplementedException();

        public int PropInt => throw new NotImplementedException();

        public bool First()
        {
            return true;
        }
    }
}
