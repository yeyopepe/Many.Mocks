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
    public static partial class Extensions
    {
        /// <summary>
        /// Gets mocks from all constructors of a given type. Not proxiable classes will be ignored
        /// </summary>
        /// <param name="type">Type to extract mocks from</param>
        /// <param name="signature">Signature of specific constructor to scan. If null then every override is scanned for mocks</param>
        /// <param name="behavior">Mock's behavior</param>
        /// <returns>Mocks needed by constructors</returns>
        /// <exception cref="MethodNotFoundException"></exception>
        public static Bag GetMocksFromConstructors(this Type type,
                                                    IEnumerable<Type> signature = null,
                                                    Behavior behavior = Behavior.Loose)
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
                var temp = GetMocksFrom(item, behavior);
                if (temp.Any())
                    mocks.AddRange(temp);
            }

            result.Mocks = mocks;
            return result;
        }
        /// <summary>
        /// Gets mocks from every property of a given type. Not proxiable classes will be ignored
        /// </summary>
        /// <param name="type">Type to extract mocks from</param>
        /// <param name="behavior">Mock's behavior</param>
        /// <returns>Mocks of properties types</returns>
        /// <exception cref="MethodNotFoundException"></exception>
        public static Bag GetMocksFromProperties(this Type type,
                                                    Behavior behavior = Behavior.Loose)
        {
            var result = new Bag();
            var mocks = new List<MockItem>();

            var properties = type.GetProperties().AsEnumerable();
            if (properties != null && properties.Any())
            {
                //Scan for mocks
                foreach (var item in properties)
                    mocks.Add(item.PropertyType.GetMockItem(null, behavior));
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
        /// <param name="behavior">Mock's behavior</param>
        /// <returns>Mocks needed by method</returns>
        /// <exception cref="MethodNotFoundException"></exception>
        public static Bag GetMocksFrom(this Type type,
                                        string methodName,
                                        IEnumerable<Type> signature = null,
                                        Behavior behavior = Behavior.Loose)
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
                    throw new MethodNotFoundException($"Method {methodName} with signature ({string.Join(", ", signature.Select(p => p.Name))}) was not found.");
            }

            //Scan for mocks
            foreach (var item in methods)
            {
                var temp = GetMocksFrom(item, behavior);
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
                    var types = mocks.Select(p => p.Instance.Object);
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
        public static void Invoke<T>(this HashSet<MockDetail> mocks, string method, T obj)
        {
            _ = mocks.Invoke<T, object>(method, obj);
        }
        /// <summary>
        /// Gets mocks from parameters of given method
        /// </summary>
        /// <param name="sourceMethod">Method to get parameters from</param>
        /// <param name="behavior">Mock's behavior</param>
        /// <returns>List of mocks</returns>
        private static IEnumerable<MockItem> GetMocksFrom(MethodBase sourceMethod, Behavior behavior = Behavior.Loose)
        {
            var result = new List<MockItem>();
            foreach (var item in sourceMethod.GetParameters())
                result.Add(item.ParameterType.GetMockItem(sourceMethod, behavior));
            
            return result;
        }
        /// <summary>
        /// Gets the Behaviour override of Mock.Of generic method 
        /// </summary>
        /// <param name="type">Type to mock</param>
        /// <param name="behavior">Mock's behavior</param>
        /// <returns>Method to get Mock</returns>
        /// <exception cref="Exception"></exception>
        private static Moq.Mock GetMock(this Type type, Behavior behavior = Behavior.Loose)
        {
            var mockOfMethods = typeof(Mock).GetMethods().Where(p => p.Name == "Of");
            var mockOfSelected = mockOfMethods.Where(p => p.GetParameters().Count() == 1 &&
                                            p.GetParameters().First().ParameterType == typeof(Moq.MockBehavior)).First();
            var mockOfMethod = mockOfSelected.MakeGenericMethod(type); //Mock.Of<>

            var mockedObject = mockOfMethod.Invoke(null, new object[] { behavior.Convert() });

            var mockGetMethods = typeof(Mock).GetMethods().Where(p => p.Name == "Get");
            var mockGetSelected = mockGetMethods.Where(p => p.GetParameters().Count() == 1).First();
            var mockGetMethod = mockGetSelected.MakeGenericMethod(type); //Mock.Get<>

            return (Moq.Mock)mockGetMethod.Invoke(null, new object[] { mockedObject });
        }
        /// <summary>
        /// Gets the mock item of a given type
        /// </summary>
        /// <param name="type">Type to mock</param>
        /// <param name="sourceMethod">Method that references the mock</param>
        /// <param name="behavior">Mock's behavior</param>
        /// <returns>Mock item</returns>
        private static MockItem GetMockItem(this Type type, MethodBase sourceMethod = null, Behavior behavior = Behavior.Loose)
        {
            try
            {
                return new MockItem
                {
                    Details = new MockDetail
                    {
                        IsInterface = type.IsInterface,
                        Type = type,
                        Instance = type.GetMock(behavior)
                    }, 
                    Generated = true,
                    Source = sourceMethod
                };
            }
            catch (Exception ex)
            {
                return new MockItem
                {
                    Details = new MockDetail
                    {
                        IsInterface = type.IsInterface,
                        Type = type
                    },
                    Error = ex.InnerException ?? ex,
                    Source = sourceMethod
                };
            }
        }

    }
}
