using Many.Mocks.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using static Many.Mocks.Bag;
using static Many.Mocks.Bag.MockItem;
using Moq;

namespace Many.Mocks
{
    /// <summary>
    /// Represents extensions to handle large number of mocks
    /// </summary>
    public static partial class Extensions_Filter
    {
        /// <summary>
        /// Gets mocks from all constructors of a given type. Not proxiable classes will be ignored
        /// </summary>
        /// <param name="type">Type to extract mocks from</param>
        /// <param name="signature">Signature of specific constructor to scan. If null then every override is scanned for mocks</param>
        /// <returns>Mocks needed by constructors</returns>
        /// <exception cref="MethodNotFoundException"></exception>
        public static Bag GetMocksFromConstructors(this Type type, IEnumerable<Type> signature = null)
        {
            var result = new Bag();
            var mocks = new List<MockItem>();

            var ctors = type.GetConstructors().AsEnumerable();
            if (ctors == null || !ctors.Any())
                throw new MethodNotFoundException($"There is no constructor for {type.FullName}.");

            //Search specific ctor.
            if (signature != null &&
               signature.Any())
            {
                ctors = ctors.Where(p => p.GetParameters().Select(a => a.ParameterType).IsEquivalentTo(signature));
                if (ctors == null || !ctors.Any())
                    throw new MethodNotFoundException($"There is no constructor for {type.FullName} with signature ({string.Join(", ", signature.Select(p => p.Name))})");
            }

            //Scan for mocks
            foreach (var item in ctors)
            {
                var temp = GetMocksFrom(item);
                if (temp.Any())
                    mocks.AddRange(temp);
            }

            result.Mocks = mocks;
            return result;
        }
        /// <summary>
        /// Gets mocks of a given method. Not proxiable classes will be ignored
        /// </summary>
        /// <param name="type">Type to extract mocks from</param>
        /// <param name="methodName">Method name (case insensitive)</param>
        /// <param name="signature">Signature of specific method to scan. If null then every override is scanned for mocks</param>
        /// <returns>Mocks needed by method</returns>
        /// <exception cref="MethodNotFoundException"></exception>
        public static Bag GetMocksFrom(this Type type, string methodName, IEnumerable<Type> signature = null)
        {
            var result = new Bag();
            var mocks = new List<MockItem>();

            var methods = type.GetMethods().Where(p => p.Name.Equals(methodName, StringComparison.InvariantCultureIgnoreCase));
            if (methods == null || !methods.Any())
                throw new MethodNotFoundException($"Method {methodName} was not found.");
            
            //Search specific override
            if (signature != null &&
                signature.Any())
            {
                methods = methods.Where(p => p.GetParameters().Select(a => a.ParameterType).IsEquivalentTo(signature));
                if (methods == null || !methods.Any())
                    throw new MethodNotFoundException($"Method {methodName} with signature ({string.Join(", ",signature.Select(p => p.Name))}) was not found.");
            }

            //Scan for mocks
            foreach (var item in methods)
            {
                var temp = GetMocksFrom(item);
                if (temp.Any())
                    mocks.AddRange(temp);
            }

            result.Mocks = mocks;
            return result;
        }
        /// <summary>
        /// Tries to get an instance using a constructor best fits with given mocks
        /// </summary>
        /// <typeparam name="T">Type to instantiate</typeparam>
        /// <param name="mocks">Mocks to use in constructor</param>
        /// <param name="result">Instance if process is successful</param>
        /// <returns>TRUE if we can get an instance. FALSE otherwise</returns>
        public static bool UseToTryInstantiate<T>(this HashSet<MockDetail> mocks, out T result)
        {
            result = default(T);
            try
            {
                var ctor = typeof(T).GetConstructor(mocks.Select(p => p.Type).ToArray());
                if (ctor == null) return false;
                {
                    var types = mocks.Select(p => p.Instance);
                    result = (T)Activator.CreateInstance(typeof(T), types.ToArray());
                }
                return true;
            }
            catch { return false; }
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
        public static R Invoke<T, R>(this HashSet<MockDetail> mocks, string method, T obj)
        {
            var methods = typeof(T).GetMethods().Where(p => p.Name.Equals(method, StringComparison.InvariantCultureIgnoreCase));
            if (methods == null || !methods.Any())
                throw new MethodNotFoundException($"Method {method} was not found");

            var rightName = methods.First().Name;
            var toInvoke = typeof(T).GetMethod(rightName, mocks.Select(p => p.Type).ToArray());

            if (toInvoke == null)
                throw new MethodNotFoundException($"Given mocks do not match with {rightName}() parameters");
            return (R)toInvoke.Invoke(obj, mocks.Select(p => p.Instance).ToArray());
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
        public static void Invoke<T>(this HashSet<MockDetail> mocks, string method, T obj)
        {
            _ = mocks.Invoke<T, object>(method, obj);
        }
        /// <summary>
        /// Gets mocks from given parameters
        /// </summary>
        /// <param name="sourceMethod"></param>
        /// <returns>List of mocks</returns>
        private static IEnumerable<MockItem> GetMocksFrom(MethodBase sourceMethod)
        {
            var result = new List<MockItem>();
            foreach (var item in sourceMethod.GetParameters())
            {
                var temp = new MockItem();
                temp.Source = sourceMethod;

                var isInterface = item.ParameterType.IsInterface ? true : false;

                try
                {
                    if (isInterface)
                    {
                        var method = GetMockOfGeneric(item);
                        temp.Details = new MockItem.MockDetail()
                        {
                            IsInterface = isInterface,
                            Instance = method.Invoke(null, null),
                            Type = item.ParameterType
                        };
                    }
                    else
                    {
                        var method = GetMockOfGeneric(item);
                        temp.Details = new MockItem.MockDetail()
                        {
                            Instance = method.Invoke(null, null),
                            Type = item.ParameterType
                        };

                    }
                    temp.Generated = true;
                }
                catch (Exception ex)
                {
                    temp.Error = ex.InnerException ?? ex;
                }
                finally
                {
                    result.Add(temp);
                }
            }
            return result;
        }
        /// <summary>
        /// Gets the Mock.Of generic method
        /// </summary>
        /// <param name="parameter">Generic parameter</param>
        /// <returns>Method to get Mock</returns>
        /// <exception cref="Exception"></exception>
        private static MethodInfo GetMockOfGeneric(ParameterInfo parameter)
        {
            var methods = typeof(Mock).GetMethods().Where(p => p.Name == "Of");
            var method = methods.Where(p => !p.GetParameters().Any()).First();
            return method.MakeGenericMethod(parameter.ParameterType);
        }
     
    }
}
