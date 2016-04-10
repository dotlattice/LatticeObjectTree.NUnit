LatticeObjectTree.NUnit [![Build status](https://ci.appveyor.com/api/projects/status/gxx5t39t85im98di?svg=true)](https://ci.appveyor.com/project/dotlattice/latticeobjecttree-nunit)
=======================

A .NET library of custom NUnit assertions for comparing objects recursively.

Quick Start
-----------
Here's a couple examples of using the `Assert` methods to compare objects:

```c#
ObjectTreeAssert.AreEqual(expected, actual);
Assert.That(actual, IsObjectTree.EqualTo(expected));
```

```c#
ObjectTreeAssert.AreNotEqual(expected, actual);
Assert.That(actual, IsObjectTree.NotEqualTo(expected));
Assert.That(actual, Is.Not.ObjectTreeEqualTo(expected));
```

You can also use [filters](#filtering) in the assert methods:

```c#
ObjectTreeAssert.AreEqual(expected, actual, new ObjectTreeNodeFilter
{
    ExcludedProperties = new[] 
    {
        ReflectionUtils.Property<Person>(x => x.FullName)
    }
});
Assert.That(actual, IsObjectTree.EqualTo(expected).WithFilter(new ObjectTreeNodeFilter
{
    ExcludedProperties = new[] 
    { 
        ReflectionUtils.Property<Person>(x => x.FullName)
    }
}));
```


Installation
------------
There are several ways to install this library:

* Install the [NuGet package](https://www.nuget.org/packages/LatticeObjectTree.NUnit/)
* Download the assembly from the [latest release](https://github.com/dotlattice/LatticeObjectTree.NUnit/releases/latest) and install it manually
* Copy the parts you want into your own project

This entire library is released into the [public domain](https://github.com/dotlattice/LatticeObjectTree.NUnit/blob/master/LICENSE).  So you can copy anything from this library into your own project without having to worry about attribution or any of that stuff.