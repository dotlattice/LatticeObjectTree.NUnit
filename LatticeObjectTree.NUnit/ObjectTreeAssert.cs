using LatticeObjectTree.Comparers;
using LatticeObjectTree.NUnit.Constraints;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace LatticeObjectTree.NUnit
{
    /// <summary>
    /// Provides methods for examining objects based on their object tree representations.
    /// </summary>
    public static class ObjectTreeAssert
    {
        #region AreEqual

        /// <summary>
        /// Verifies that two objects are equal based on their object trees. 
        /// If they are not, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        public static void AreEqual(object expected, object actual)
        {
            AreEqual(expected, actual, message: default(string), args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are equal based on their object trees. 
        /// If they are not, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="message">the message to display in case of failure</param>
        public static void AreEqual(object expected, object actual, string message)
        {
            AreEqual(expected, actual, message, args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are equal based on their object trees. 
        /// If they are not, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="message">the message to display in case of failure</param>
        /// <param name="args">objects to be used in formatting the message</param>
        public static void AreEqual(object expected, object actual, string message, params object[] args)
        {
            AreEqual(expected, actual, nodeFilter: null, message: message, args: args);
        }

        /// <summary>
        /// Verifies that two objects are equal based on their filtered object trees. 
        /// If they are not, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="nodeFilter">a filter on the nodes of the object trees to include in the comparison</param>
        public static void AreEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter)
        {
            AreEqual(expected, actual, nodeFilter, message: default(string), args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are equal based on their filtered object trees. 
        /// If they are not, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="nodeFilter">a filter on the nodes of the object trees to include in the comparison</param>
        /// <param name="message">the message to display in case of failure</param>
        public static void AreEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter, string message)
        {
            AreEqual(expected, actual, nodeFilter, message, args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are equal based on their filtered object trees. 
        /// If they are not, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="nodeFilter">a filter on the nodes of the object trees to include in the comparison</param>
        /// <param name="message">the message to display in case of failure</param>
        /// <param name="args">objects to be used in formatting the message</param>
        public static void AreEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter, string message, params object[] args)
        {
            Assert.That(actual, new ObjectTreeEqualConstraint(expected, nodeFilter), message, args);
        }

        #endregion

        #region AreNotEqual

        /// <summary>
        /// Verifies that two objects are not equal based on their object trees. 
        /// If they are equal, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        public static void AreNotEqual(object expected, object actual)
        {
            AreNotEqual(expected, actual, message: default(string), args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are not equal based on their object trees. 
        /// If they are equal, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="message">the message to display in case of failure</param>
        public static void AreNotEqual(object expected, object actual, string message)
        {
            AreNotEqual(expected, actual, message, args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are not equal based on their object trees. 
        /// If they are equal, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="message">the message to display in case of failure</param>
        /// <param name="args">objects to be used in formatting the message</param>
        public static void AreNotEqual(object expected, object actual, string message, params object[] args)
        {
            AreNotEqual(expected, actual, nodeFilter: null, message: message, args: args);
        }

        /// <summary>
        /// Verifies that two objects are not equal based on their filtered object trees. 
        /// If they are equal, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="nodeFilter">a filter on the nodes of the object trees to include in the comparison</param>
        public static void AreNotEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter)
        {
            AreNotEqual(expected, actual, nodeFilter, message: default(string), args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are not equal based on their filtered object trees. 
        /// If they are equal, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="nodeFilter">a filter on the nodes of the object trees to include in the comparison</param>
        /// <param name="message">the message to display in case of failure</param>
        public static void AreNotEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter, string message)
        {
            AreNotEqual(expected, actual, nodeFilter, message, args: default(object[]));
        }

        /// <summary>
        /// Verifies that two objects are not equal based on their filtered object trees. 
        /// If they are equal, an <see cref="AssertionException"/> is thrown.
        /// </summary>
        /// <param name="expected">the expected object</param>
        /// <param name="actual">the actual object</param>
        /// <param name="nodeFilter">a filter on the nodes of the object trees to include in the comparison</param>
        /// <param name="message">the message to display in case of failure</param>
        /// <param name="args">objects to be used in formatting the message</param>
        public static void AreNotEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter, string message, params object[] args)
        {
            Assert.That(actual, Is.Not.Matches(new ObjectTreeEqualConstraint(expected, nodeFilter)), message, args);
        }

        #endregion

        #region Equals and ReferenceEquals

        /// <summary>
        /// Always throws an <see cref="AssertionException"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new bool Equals(object a, object b)
        {
            throw new AssertionException($"{nameof(ObjectTreeAssert)}.{nameof(Equals)} should not be used for assertions");
        }

        /// <summary>
        /// Always throws an <see cref="AssertionException"/>.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public static new void ReferenceEquals(object a, object b)
        {
            throw new AssertionException($"{nameof(ObjectTreeAssert)}.{nameof(ReferenceEquals)} should not be used for assertions");
        }

        #endregion
    }
}
