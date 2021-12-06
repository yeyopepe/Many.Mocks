using Moq;
using System;
using System.Linq;
using static Many.Mocks.MockItem;

namespace Many.Mocks
{
    /// <summary>
    /// Mock's behaviors
    /// </summary>
    public enum Behavior
    {
        /// <summary>
        /// Will never throw exceptions, returning default values when necessary (null for reference types, zero for value types or empty enumerables and arrays).
        /// </summary>
        Loose,
        /// <summary>
        /// Causes the mock to always throw an exception for invocations that don't have a corresponding setup.
        /// </summary>
        Strict
    }
    /// <summary>
    /// Represents a group of mapping methods
    /// </summary>
    internal static class Mapper
    {
        /// <summary>
        /// Converts custom Mock's behavior to Moq's one
        /// </summary>
        /// <param name="behavior">Behavior</param>
        /// <returns>Moq's behavior</returns>
        /// <exception cref="NotImplementedException"></exception>
        public static MockBehavior Convert(this Behavior behavior)
        {
            switch (behavior)
            {
                case Behavior.Loose:
                    return MockBehavior.Loose;
                case Behavior.Strict:
                    return MockBehavior.Strict;
                default:
                    throw new NotImplementedException(nameof(behavior));
            }
        }
        /// <summary>
        /// Converts a Mock
        /// </summary>
        /// <param name="value">Original mock</param>
        /// <returns>Mock desatils</returns>
        /// <exception cref="Exception"></exception>
        public static MockDetail Convert(this Mock value)
        {
            var originalType = value.GetType().GetGenericArguments().First();
            return new MockDetail()
            {
                Instance = value, 
                IsInterface= originalType.IsInterface,
                Type = originalType
            };
        }

    }
}
