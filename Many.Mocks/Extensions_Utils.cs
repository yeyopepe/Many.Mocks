using System;
using System.Collections.Generic;
using System.Linq;

namespace Many.Mocks
{
    public static partial class Extensions
    {
        /// <summary>
        /// Checks whether two lists of types are equal or not
        /// </summary>
        /// <param name="obj1">List of types</param>
        /// <param name="obj2">List of types</param>
        /// <returns>TRUE if both lists has same ordered types. FALSE otherwise</returns>
        internal static bool IsEquivalentTo(this IEnumerable<Type> obj1, IEnumerable<Type> obj2)
        {
            var result = true;
            if (obj1 != null && obj2 != null &&
                obj1.Count() == obj2.Count())
            {
                for (int i = 0; i < obj1.Count(); i++)
                {
                    if (obj1.ElementAt(i) != obj2.ElementAt(i))
                    {
                        result = false;
                        break;
                    }
                }
            }
            else return false;

            return result;
        }
       
        
    }
}
