using System.Collections.Generic;

namespace Many.Mocks.Utils
{
    class MockEqualityComparer : IEqualityComparer<MockItem>
    {
        public bool Equals(MockItem x, MockItem y)
        {
            return x.Details.Type == y.Details.Type;
        }

        public int GetHashCode(MockItem obj)
        {
            int hash = 13;
            hash = (hash * 7) + obj.Details.Type.GetHashCode();
            return hash;
        }
    }
}
