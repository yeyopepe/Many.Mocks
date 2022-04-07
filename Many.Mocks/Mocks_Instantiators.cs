using System;
using System.Collections.Generic;
using System.Linq;
using Moq;
using static Many.Mocks.MockItem;

namespace Many.Mocks
{
    /// <summary>
    /// Represents extensions to handle large number of mocks
    /// </summary>
    public static partial class Mocks
    {
        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Bag of mocks to use in constructor</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool TryInstantiate<T>(this Bag mocks, out T result) 
            => mocks.Mocks
                    .Select(p => p.Details.Instance)
                    .TryInstantiate<T>(out result);

        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool TryInstantiate<T>(this IEnumerable<MockDetail> mocks, out T result)
            => mocks.Select(p => p.Instance).TryInstantiate<T>(out result);

        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool TryInstantiate<T>(this IEnumerable<Mock> mocks, out T result)
        {
            result = default(T);
            try
            {
                var ctor = typeof(T).GetConstructor(mocks.Select(p => p.GetType().GetGenericArguments().First()).ToArray());
                if (ctor == null) return false;
                
                var types = mocks.Select(p => p.Object);
                result = (T)Activator.CreateInstance(typeof(T), types.ToArray());
                
                return true;
            }
            catch { return false; }
        }

         /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Bag of mocks to use in constructor</param>
        /// <param name="customMocks">Custom mocks to be used during the operation replacing the originals</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool TryInstantiate<T>(this Bag mocks, IEnumerable<Mock> customMocks, out T result)
             => mocks.Mocks
                    .Select(p => p.Details.Instance)
                    .TryInstantiate<T>(customMocks, out result);

        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="customMocks">Custom mocks to be used during the operation replacing the originals</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool TryInstantiate<T>(this IEnumerable<MockDetail> mocks, IEnumerable<Mock> customMocks, out T result)
            => mocks.Select(p => p.Instance).TryInstantiate<T>(customMocks, out result);

        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="customMocks">Custom mocks to be used during the operation replacing the originals</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool TryInstantiate<T>(this IEnumerable<Mock> mocks, IEnumerable<Mock> customMocks, out T result)
        {
            result = default(T);
            if (customMocks != null &&
                customMocks.Any())
            {
                var filteredMocks = new List<Mock>();
                foreach (var mock in mocks)
                {
                    var replace = customMocks.FirstOrDefault(p => p.GetType().Equals(mock.GetType()));
                    if (replace != null)
                    {
                        filteredMocks.Add(replace);
                    }
                    else
                    {
                        filteredMocks.Add(mock);
                    }
                }
                return mocks.TryInstantiate<T>(out result);
            }
            else
            {
                return mocks.TryInstantiate<T>(out result);
            }

        }


    }
}
