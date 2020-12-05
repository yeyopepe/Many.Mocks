using Many.Mocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Many.Mocks.Bag;
using static Many.Mocks.Bag.MockItem;
using Moq;
using Many.Mocks.Utils;

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
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool UseToTryInstantiate<T>(this IEnumerable<Mock> mocks, out T result)
        {
            result = default(T);
            try
            {
                var ctor = typeof(T).GetConstructor(mocks.Select(p => p.GetType().GetGenericArguments().First()).ToArray());
                if (ctor == null) return false;
                {
                    var types = mocks.Select(p => p.Object);
                    result = (T)Activator.CreateInstance(typeof(T), types.ToArray());
                }
                return true;
            }
            catch { return false; }
        }
        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="customMocks">Custom mocks to be used during the operation replacing the originals</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool UseToTryInstantiate<T>(this IEnumerable<Mock> mocks, IEnumerable<Mock> customMocks, out T result)
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
                return mocks.UseToTryInstantiate<T>(out result);
            }
            else
            {
                return mocks.UseToTryInstantiate<T>(out result);
            }

        }
        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool UseToTryInstantiate<T>(this IEnumerable<MockDetail> mocks, out T result)
        {
            return mocks.Select(p => p.Instance).UseToTryInstantiate<T>(out result);
        }
        /// <summary>
        /// Tries to get an instance using the constructor fits with given ordered mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="customMocks">Custom mocks to be used during the operation replacing the originals</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool UseToTryInstantiate<T>(this IEnumerable<MockDetail> mocks, IEnumerable<Mock> customMocks, out T result)
        {
            return mocks.Select(p => p.Instance).UseToTryInstantiate<T>(customMocks, out result);
        }

        /// <summary>
        /// Invokes a method using mocks best fit
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <typeparam name="R">Result type</typeparam>
        /// <param name="mocks">Mocks to use in invocation</param>
        /// <param name="method">Method to invoke</param>
        /// <param name="obj">Object to use to invoke</param>
        /// <returns>Result of invocation</returns>
        /// <exception cref="MethodNotFoundException"></exception>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="TargetException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public static R Invoke<T, R>(this IEnumerable<MockDetail> mocks, string method, T obj)
        {
            var methods = typeof(T).GetMethods().Where(p => p.Name.Equals(method, StringComparison.InvariantCultureIgnoreCase));
            if (methods == null || !methods.Any())
                throw new MethodNotFoundException($"Method {method} was not found");

            var rightName = methods.First().Name;
            var toInvoke = typeof(T).GetMethod(rightName, mocks.Select(p => p.Type).ToArray());

            if (toInvoke == null)
                throw new MethodNotFoundException($"Given mocks do not match with {rightName}() parameters");
            return (R)toInvoke.Invoke(obj, mocks.Select(p => p.Instance.Object).ToArray());
        }
        /// <summary>
        /// Invokes a method using mocks best fit
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <param name="mocks">Mocks to use in invocation</param>
        /// <param name="method">Method to invoke</param>
        /// <param name="obj">Object to use to invoke</param>
        /// <exception cref="MethodNotFoundException"></exception>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="TargetException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public static void Invoke<T>(this IEnumerable<MockDetail> mocks, string method, T obj)
        {
            _ = mocks.Invoke<T, object>(method, obj);
        }
        

    }
}
