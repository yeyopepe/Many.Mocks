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
        /// Extracts the distinct mocks from a given bag
        /// </summary>
        /// <param name="value">Mock bag</param>
        /// <returns>List of unique mocks</returns>
        /// <exception cref="ArgumentException"></exception>
        public static HashSet<MockDetail> ExtractDistinct(this Bag value)
        {
            var content = value.Extract().Distinct<MockDetail>(new MockEqualityComparer());
            return new HashSet<MockDetail>(content);
        }
        /// <summary>
        /// Extracts just the mocks from a given bag
        /// </summary>
        /// <param name="value">Mock bag</param>
        /// <returns>List of unique mocks</returns>
        /// <exception cref="ArgumentException"></exception>
        public static HashSet<MockDetail> Extract(this Bag value)
        {
            var content = value.Mocks.Select(p => p.Details);
            return new HashSet<MockDetail>(content);
        }


    }
}
