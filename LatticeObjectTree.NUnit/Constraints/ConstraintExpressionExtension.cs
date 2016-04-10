using NUnit.Framework.Constraints;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LatticeObjectTree.NUnit.Constraints
{
    /// <summary>
    /// Extension methods for the NUnit <see cref="ConstraintExpression"/> class that use <see cref="ObjectTree"/> comparisons.
    /// </summary>
    public static class ConstraintExpressionExtension
    {
        /// <summary>
        /// Appends and returns a constraint that tests two items for object tree equality.
        /// </summary>
        public static ObjectTreeEqualConstraint ObjectTreeEqualTo(this ConstraintExpression constraintExpression, object expected)
        {
            return (ObjectTreeEqualConstraint)constraintExpression.Append(new ObjectTreeEqualConstraint(expected));
        }
    }
}
