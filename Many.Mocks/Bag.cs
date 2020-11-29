using System;
using System.Collections.Generic;
using System.Reflection;

namespace Many.Mocks
{
    /// <summary>
    /// Represent a bag of mocks
    /// </summary>
    public class Bag
    {
        /// <summary>
        /// Represents a Mock item
        /// </summary>
        public struct MockItem
        {
            /// <summary>
            /// Represents a Mock
            /// </summary>
            public struct MockDetail
            {
                /// <summary>
                /// Gets the Mock instance if it was successfully generated
                /// </summary>
                public Moq.Mock Instance { get; internal set; }
                /// <summary>
                /// Gets the base type
                /// </summary>
                public Type Type { get; internal set; }
                /// <summary>
                /// Gets a value indicating whether the System.Type is an interface; that is, not a class or a value type.
                /// </summary>
                public bool IsInterface { get; internal set; }
            }
            /// <summary>
            /// Gets a value indicating whether the mock is successfully generated or not
            /// </summary>
            public bool Generated { get; internal set; }
            /// <summary>
            /// Error information in case of mock was not be able to generate
            /// </summary>
            public Exception Error { get; internal set; }
            /// <summary>
            /// Method that references the mock
            /// </summary>
            public MethodBase Source { get; internal set; }
            /// <summary>
            /// Gets the details of current Mock
            /// </summary>
            public MockDetail Details { get; internal set; }
        }
        /// <summary>
        /// Gets mock items
        /// </summary>
        public IEnumerable<MockItem> Mocks { get; internal set; }

    }


}
