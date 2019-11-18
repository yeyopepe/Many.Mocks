using Many.Mocks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using static Many.Mocks.Bag.MockItem;

namespace Many.Mocks
{
    public static partial class Extensions
    {
        /// <summary>
        /// Filters the distinct mocks
        /// </summary>
        /// <param name="value">Mock bag</param>
        /// <returns>List of unique mocks</returns>
        /// <exception cref="ArgumentException"></exception>
        public static HashSet<MockDetail> Distinct(this IEnumerable<Bag.MockItem> value)
        {
            var content = value.Select(p => p.Details).Distinct<MockDetail>(new MockEqualityComparer());
            return new HashSet<MockDetail>(content);
        }
  
    }
}
