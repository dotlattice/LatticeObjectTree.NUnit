using LatticeObjectTree.NUnit.Constraints;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LatticeObjectTree.NUnit
{
    /// <summary>
    /// An equivalent of the NUnit <see cref="Is"/> class that use <see cref="ObjectTree"/> comparisons.
    /// </summary>
    public static class IsObjectTree
    {
        /// <summary>
        /// Returns a constraint that tests two items for object tree equality.
        /// </summary>
        public static ObjectTreeEqualConstraint EqualTo(object expected)
        {
            return new ObjectTreeEqualConstraint(expected);
        }

        /// <summary>
        /// Returns a constraint that tests two items for object tree inequality.
        /// </summary>
        public static ObjectTreeEqualConstraint NotEqualTo(object expected)
        {
            return Is.Not.ObjectTreeEqualTo(expected);
        }
    }
}
