using Many.Mocks.Exceptions;
using Many.Mocks.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using static Many.Mocks.Bag;
using static Many.Mocks.Bag.MockItem;

namespace Many.Mocks
{
    public static class Filters
    {
        /// <summary>
        /// Searches the distinct mocks from a given bag
        /// </summary>
        /// <param name="value">Mocks bag</param>
        /// <param name="onlyValid">TRUE if you want to get only valid mocks in given bag. FALSE to get valid and invalid ones</param>
        /// <returns>Bag with only distinct mocks</returns>
        /// <exception cref="ArgumentException"></exception>
        public static Bag Distinct(this Bag value, bool onlyValid=true)
        {
            if (onlyValid)
                value.Mocks = value.Mocks.Where(p => p.Generated);

            value.Mocks = value.Mocks.Distinct<MockItem>(new MockEqualityComparer());
            return value;
        }
        /// <summary>
        /// Gets the number of mocks in a bag
        /// </summary>
        /// <typeparam name="T">Mock type</typeparam>
        /// <param name="value">Mocks bag</param>
        /// <param name="onlyValid">TRUE if only valid mocks should be returned. FALSE to return all whether they are valid or not</param>
        /// <returns>Number of mocks</returns>
        public static int Count<T>(this Bag value, bool onlyValid = true)
            where T : class
        {
            if (value.TryFind<T>(out IList<Moq.Mock<T>> found, onlyValid))
            {
                return found.Count;
            }
            return 0;
        }
        /// <summary>
        /// Finds the first valid mock of given type
        /// </summary>
        /// <typeparam name="T">Mock type</typeparam>
        /// <param name="value">Mocks bag</param>
        /// <returns>Found mock</returns>
        /// <exception cref="ValidMockNotFoundException"></exception>
        public static Moq.Mock<T> First<T>(this Bag value)
            where T: class
        {
            if (value.TryFind<T>(out IList<Moq.Mock<T>> found, true))
            {
                return found.First();
            }
            throw new ValidMockNotFoundException($"Mock of {typeof(T)} not found");
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
