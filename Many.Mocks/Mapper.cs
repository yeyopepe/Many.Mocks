using Many.Mocks.Utils;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using static Many.Mocks.Bag.MockItem;

namespace Many.Mocks
{
    /// <summary>
    /// Mock's behaviors
    /// </summary>
    public enum Behavior
    {
        Loose,
        Strict
    }
    /// <summary>
    /// Represents a group of mapper methods
    /// </summary>
    public static class Mapper
    {
        /// <summary>
        /// Converts custom Mock's behavior to Moq's one
        /// </summary>
        /// <param name="behavior">Behavior</param>
        /// <returns>Moq's behavior</returns>
        internal static Moq.MockBehavior Convert(this Behavior behavior)
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

    }
}
