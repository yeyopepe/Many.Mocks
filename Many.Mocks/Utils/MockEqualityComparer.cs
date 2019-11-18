using System;
using System.Collections.Generic;
using static Many.Mocks.Bag.MockItem;

namespace Many.Mocks.Utils
{
    class MockEqualityComparer : IEqualityComparer<MockDetail>
    {
        public bool Equals(MockDetail x, MockDetail y)
        {
            return x.Type == y.Type;
        }

        public int GetHashCode(MockDetail obj)
        {
            int hash = 13;
            hash = (hash * 7) + obj.Type.GetHashCode();
            return hash;
        }
    }
}
