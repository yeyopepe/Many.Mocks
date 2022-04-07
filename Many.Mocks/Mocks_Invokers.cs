using Many.Mocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Many.Mocks.MockItem;

namespace Many.Mocks
{

    /// <summary>
    /// Represents extensions to handle large number of mocks
    /// </summary>
    public static partial class Mocks
    {

        /// <summary>
        /// Invokes a method using mocks best fit
        /// </summary>
        /// <typeparam name="T">Object type</typeparam>
        /// <typeparam name="R">Result type</typeparam>
        /// <param name="mocks">Bag of mocks to use in invocation</param>
        /// <param name="method">Method to invoke</param>
        /// <param name="obj">Object to use to invoke</param>
        /// <returns>Result of invocation</returns>
        /// <exception cref="MethodNotFoundException"></exception>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="TargetException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public static R Invoke<T, R>(this Bag mocks, string method, T obj)
            => mocks.Mocks.Select(p => p.Details).Invoke<T, R>(method, obj);

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
        /// <param name="mocks">Bag of mocks to use in invocation</param>
        /// <param name="method">Method to invoke</param>
        /// <param name="obj">Object to use to invoke</param>
        /// <exception cref="MethodNotFoundException"></exception>
        /// <exception cref="TargetInvocationException"></exception>
        /// <exception cref="TargetException"></exception>
        /// <exception cref="InvalidOperationException"></exception>
        /// <exception cref="Exception"></exception>
        public static void Invoke<T>(this Bag mocks, string method, T obj)
        {
            mocks.Mocks.Select(p => p.Details).Invoke<T>(method, obj);
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
