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
        /// <param name="value">Mocks bag</param>
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
        /// <param name="value">Mocks bag</param>
        /// <returns>List of unique mocks</returns>
        /// <exception cref="ArgumentException"></exception>
        public static HashSet<MockDetail> Extract(this Bag value)
        {
            var content = value.Mocks.Select(p => p.Details);
            return new HashSet<MockDetail>(content);
        }
        /// <summary>
        /// Tries to find mocks of given type
        /// </summary>
        /// <typeparam name="T">Mock type</typeparam>
        /// <param name="value">Mocks bag</param>
        /// <param name="result">Found mocks</param>
        /// <param name="onlyValid">TRUE if only valid mocks should be returned. FALSE to return all whether they are valid or not</param>
        /// <returns>TRUE if at least one mock was found. FALSE otherwise</returns>
        public static bool TryFind<T>(this Bag value, out IList<Moq.Mock<T>> result, bool onlyValid = true) 
            where T:class 
        {
            var returns = false;
            result = new List<Moq.Mock<T>>();
            try
            {
                var temp = value.Mocks.Where(p => p.Details.Type.Equals(typeof(T)));
                
                if (onlyValid) 
                    temp = temp.Where(p => p.Generated);

                if (temp.Any())
                {
                    result = temp.Select(p => (Moq.Mock<T>)p.Details.Instance).ToList();
                    returns = true;
                }
                
            }
            catch { }

            return returns;
        }

    }
}
