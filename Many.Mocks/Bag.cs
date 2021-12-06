using System.Collections.Generic;

namespace Many.Mocks
{
    /// <summary>
    /// Represent a bag of mocks
    /// </summary>
    public class Bag
    {
        /// <summary>
        /// Gets mock items
        /// </summary>
        public IEnumerable<MockItem> Mocks { get; internal set; }
    }
}
