using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Many.Mocks.Utils
{
    internal static partial class Extensions
    {
        /// <summary>
        /// Checks whether two lists of types are equal or not
        /// </summary>
        /// <param name="obj1">List of types</param>
        /// <param name="obj2">List of types</param>
        /// <returns>TRUE if both lists has same ordered types. FALSE otherwise</returns>
        public static bool IsEquivalentTo(this IEnumerable<Type> obj1, IEnumerable<Type> obj2)
        {
            var result = true;

            if (obj1 == null && obj2 == null)
                return true;

            if (obj1.Count() != obj2.Count())
                return false;

            for (int i = 0; i < obj1.Count(); i++)
            {
                if (obj1.ElementAt(i) != obj2.ElementAt(i))
                {
                    result = false;
                    break;
                }
            }

            return result;
        }
        /// <summary>
        /// Clones an object
        /// </summary>
        /// <param name="source">Source object to serialize</param>
        /// <exception cref="ArgumentException"> </exception>
        /// <exception cref="Exception"></exception>
        /// <returns>New object</returns>
        public static T Clone<T>(T source)
        {
            if (!typeof(T).IsSerializable)
                throw new ArgumentException("La clase " + typeof(T).ToString() + " no es serializable");

            if (Object.ReferenceEquals(source, null))
                return default(T);

            IFormatter formatter = new BinaryFormatter();
            Stream stream = new MemoryStream();
            using (stream)
            {
                try
                {
                    formatter.Serialize(stream, source);
                    stream.Seek(0, SeekOrigin.Begin);
                    return (T)formatter.Deserialize(stream);
                }
                catch (SerializationException ex) { throw new ArgumentException(ex.Message, ex); }
                catch { throw; }
            }
        }

    }
}
