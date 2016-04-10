using LatticeObjectTree.NUnit.Constraints;
using LatticeUtils;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace LatticeObjectTree.NUnit
{
    public class ObjectTreeAssertTest
    {
        [Test]
        public void AreEqual_CompareObjectToItself()
        {
            var obj = new object();
            ObjectTreeAssert.AreEqual(obj, obj);
        }

        #region Messages

        [Test]
        public void AreEqualFail_DifferentStrings()
        {
            var expected = "hello";
            var actual = "world";
            var expectedException = Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(expected, actual));

            var expectedMessage = "  Expected: <LatticeObjectTree.ObjectTree> with root \"hello\"" + Environment.NewLine
                + "  But was:  <LatticeObjectTree.ObjectTree> with root \"world\"" + Environment.NewLine
                + "  1 Differences:    < <<root>: expected value \"hello\" but was \"world\".> >";
            Assert.AreEqual(expectedMessage, expectedException.Message);
        }

        [Test]
        public void AreEqualFail_DifferentStrings_CustomMessage()
        {
            var expected = "hello";
            var actual = "world";
            var expectedException = Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(expected, actual, message: "This is a test message"));

            var expectedMessage = "  This is a test message" + Environment.NewLine
                + "  Expected: <LatticeObjectTree.ObjectTree> with root \"hello\"" + Environment.NewLine
                + "  But was:  <LatticeObjectTree.ObjectTree> with root \"world\"" + Environment.NewLine
                + "  1 Differences:    < <<root>: expected value \"hello\" but was \"world\".> >";
            Assert.AreEqual(expectedMessage, expectedException.Message);
        }

        [Test]
        public void AreEqualFail_DifferentAnonymousObjects()
        {
            var expected = new { id = 1, message = "hello" };
            var actual = new { id = 2, message = "world" };
            var expectedException = Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(expected, actual));

            var expectedMessage = "  Expected: <LatticeObjectTree.ObjectTree> with root <{ id = 1, message = hello }>" + Environment.NewLine
 + "  But was:  <LatticeObjectTree.ObjectTree> with root <{ id = 2, message = world }>" + Environment.NewLine
 + "  2 Differences:    < <<root>.id: expected value \"1\" but was \"2\".>, <<root>.message: expected value \"hello\" but was \"world\".> >";
            Assert.AreEqual(expectedMessage, expectedException.Message);
        }

        [Test]
        public void AreEqualFail_DifferentCustomObjects()
        {
            var expected = new Company { Id = 1, Name = "hello" };
            var actual = new Company { Id = 2, Name = "world" };
            var expectedException = Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(expected, actual));

            var expectedMessage = "  Expected: <LatticeObjectTree.ObjectTree> with root <LatticeObjectTree.NUnit.ObjectTreeAssertTest+Company>" + Environment.NewLine
 + "  But was:  <LatticeObjectTree.ObjectTree> with root <LatticeObjectTree.NUnit.ObjectTreeAssertTest+Company>" + Environment.NewLine
 + "  2 Differences:    < <<root>.Id: expected value \"1\" but was \"2\".>, <<root>.Name: expected value \"hello\" but was \"world\".> >";
            Assert.AreEqual(expectedMessage, expectedException.Message);
        }

        [Test]
        public void AreEqualFail_DifferentLists()
        {
            var expected = new[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 };
            var actual = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 };
            var expectedException = Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(expected, actual));

            var expectedMessage = "  Expected: <LatticeObjectTree.ObjectTree> with root < 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25 >" + Environment.NewLine
                + "  But was:  <LatticeObjectTree.ObjectTree> with root < 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26 >" + Environment.NewLine
                + "  26 Differences:    < <<root>[0]: expected value \"0\" but was \"1\".>, <<root>[1]: expected value \"1\" but was \"2\".>, <<root>[2]: expected value \"2\" but was \"3\".>, <<root>[3]: expected value \"3\" but was \"4\".>, <<root>[4]: expected value \"4\" but was \"5\".>, <<root>[5]: expected value \"5\" but was \"6\".>, <<root>[6]: expected value \"6\" but was \"7\".>, <<root>[7]: expected value \"7\" but was \"8\".>, <<root>[8]: expected value \"8\" but was \"9\".>, <<root>[9]: expected value \"9\" but was \"10\".>, <<root>[10]: expected value \"10\" but was \"11\".>, <<root>[11]: expected value \"11\" but was \"12\".>, <<root>[12]: expected value \"12\" but was \"13\".>, <<root>[13]: expected value \"13\" but was \"14\".>, <<root>[14]: expected value \"14\" but was \"15\".>, <<root>[15]: expected value \"15\" but was \"16\".>, <<root>[16]: expected value \"16\" but was \"17\".>, <<root>[17]: expected value \"17\" but was \"18\".>, <<root>[18]: expected value \"18\" but was \"19\".>, <<root>[19]: expected value \"19\" but was \"20\".>... >";
            Assert.AreEqual(expectedMessage, expectedException.Message);
        }

        #endregion

        [Test]
        public void EqualAddresses()
        {
            var a = CreateSampleAddress();
            var b = CreateSampleAddress();
            AssertAreEqual(a, b);
        }

        [Test]
        public void DifferentAddresses()
        {
            var a = new Address
            {
                Id = 1,
                Lines = new[] { "123 Fake ST", "Arlington, VA 22222" },
            };
            var b = new Address
            {
                Id = 1,
                Lines = new[] { "321 Fake ST", "Arlington, VA 22222" },
            };

            AssertAreNotEqual(a, b);
            AssertAreEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedProperties = new[] { ReflectionUtils.Property<Address>(x => x.Lines) },
            });

            var assertionException = Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(a, b));
            StringAssert.Contains("1 Difference", assertionException.ToString());
            StringAssert.Contains("123 Fake ST", assertionException.ToString());
            StringAssert.Contains("321 Fake ST", assertionException.ToString());
        }

        [Test]
        public void EqualCompanies()
        {
            var a = CreateSampleCompany();
            var b = CreateSampleCompany();
            AssertAreEqual(a, b);
        }

        [Test]
        public void DifferentCompanies()
        {
            var a = CreateSampleCompany();
            var b = CreateSampleCompany();
            b.Employees.First().FullName = "Robert Plant";

            Assert.AreNotEqual(a, b);

            // Try a bunch of different ways to filter out the difference so that they are equal.
            AssertAreEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedProperties = new[] { ReflectionUtils.Property<Company>(x => x.Employees) }
            });
            AssertAreEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedProperties = new[] { ReflectionUtils.Property<Employee>(x => x.FullName) }
            });
            AssertAreEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedProperties = new[] { ReflectionUtils.Property<Person>(x => x.FullName) }
            });
            AssertAreEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedPropertyNames = new[] { "FullName" }
            });
            AssertAreEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedPropertyPredicates = new Func<PropertyInfo, bool>[]
                {
                    (propertyInfo) => propertyInfo.Name.Contains("Name"),
                }
            });
            AssertAreEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedNodePredicates = new Func<ObjectTreeNode, bool>[]
                {
                    (node) =>
                        node.ParentNode != null && node.ParentNode.Value is Employee && ((Employee)node.ParentNode.Value).Id == 2
                        && node.EdgeFromParent != null && node.EdgeFromParent.Member.Name.Contains("Name")
                }
            });
            AssertAreNotEqual(a, b, new ObjectTreeNodeFilter
            {
                ExcludedNodePredicates = new Func<ObjectTreeNode, bool>[]
                {
                    (node) =>
                        node.ParentNode != null && node.ParentNode.Value is Employee && ((Employee)node.ParentNode.Value).Id == 1
                        && node.EdgeFromParent != null && node.EdgeFromParent.Member.Name.Contains("Name")
                }
            });

            var assertionException = Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(a, b));
            StringAssert.Contains("1 Difference", assertionException.ToString());
            StringAssert.Contains("Robert Paulson", assertionException.ToString());
            StringAssert.Contains("Robert Plant", assertionException.ToString());
        }

        [Test]
        public void EqualPeople_Cycle()
        {
            var a = CreateSamplePerson_OwnFatherAndMotherSomehow();
            var b = CreateSamplePerson_OwnFatherAndMotherSomehow();
            AssertAreEqual(a, b);
        }

        [Test]
        public void DifferentPeople_PartialCycle()
        {
            var a = CreateSamplePerson_OwnFatherAndMotherSomehow();
            var b = CreateSamplePerson_OwnFatherAndMotherSomehow();
            b.Mother = a;
            AssertAreNotEqual(a, b);
        }

        [Test]
        public void EqualPeople_OwnGrandpa()
        {
            var a = CreateSamplePerson_OwnGrandpa();
            var b = CreateSamplePerson_OwnGrandpa();
            AssertAreEqual(a, b);
        }

        [Test]
        public void DifferentPeople_OwnGrandpa()
        {
            var a = CreateSamplePerson_OwnGrandpa();
            var b = CreateSamplePerson_OwnGrandpa();
            b.Father.FullName = "Bob";

            AssertAreNotEqual(a, b);
        }

        [Test]
        public void ListTestObjects_EmptyObjectsWithSameType()
        {
            var obj1 = new ListTestObject<string>();
            var obj2 = new ListTestObject<string>();

            AssertAreEqual(obj1, obj2);
        }

        [Test]
        public void ListTestObjects_Samples_Equal()
        {
            var obj1 = new ListTestObject<string>()
            {
                Enumerable = new[] { "test" },
                ReadOnlyCollection = new[] { "test2", "test22" },
                Collection = new[] { "test3" },
                Array = new[] { "test4" },
                IList = new[] { "test5" },
                List = new List<string> { "test6" },
            };
            var obj2 = new ListTestObject<string>()
            {
                Enumerable = new[] { "test" },
                ReadOnlyCollection = new[] { "test2", "test22" },
                Collection = new[] { "test3" },
                Array = new[] { "test4" },
                IList = new[] { "test5" },
                List = new List<string> { "test6" },
            };

            AssertAreEqual(obj1, obj2);
        }

        [Test]
        public void ListTestObjects_Samples_NotEqual()
        {
            var obj1 = new ListTestObject<string>()
            {
                Enumerable = new[] { "test" },
                ReadOnlyCollection = new[] { "test2", "test22" },
                Collection = new[] { "test3" },
                Array = new[] { "test4" },
                IList = new[] { "test5" },
                List = new List<string> { "test6" },
            };
            var obj2 = new ListTestObject<string>()
            {
                Enumerable = new[] { "TEST" },
                ReadOnlyCollection = new[] { "TEST2", "TEST22" },
                Collection = new[] { "TEST3" },
                Array = new[] { "TEST4" },
                IList = new[] { "TEST5" },
                List = new List<string> { "TEST6" },
            };

            AssertAreNotEqual(obj1, obj2);
        }

        [Test]
        public void ListTestObjects_EmptyObjectsWithDifferentTypes()
        {
            var obj1 = new ListTestObject<string>();
            var obj2 = new ListTestObject<int>();

            AssertAreNotEqual(obj1, obj2);
        }

        [Test]
        public void ListTestObjects_EmptyObjectsWithDifferentCompatibleTypes()
        {
            var obj1 = new ListTestObject<Person>();
            var obj2 = new ListTestObject<Employee>();

            AssertAreNotEqual(obj1, obj2);
        }

        #region Helpers

        private void AssertAreEqual(object expected, object actual)
        {
            ObjectTreeAssert.AreEqual(expected, actual);
            Assert.That(actual, IsObjectTree.EqualTo(expected));

            Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreNotEqual(expected, actual));
            Assert.Throws<AssertionException>(() => Assert.That(actual, IsObjectTree.NotEqualTo(expected)));
            Assert.Throws<AssertionException>(() => Assert.That(actual, Is.Not.ObjectTreeEqualTo(expected)));
        }

        private void AssertAreEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter)
        {
            ObjectTreeAssert.AreEqual(expected, actual, nodeFilter);
            Assert.That(actual, IsObjectTree.EqualTo(expected).WithFilter(nodeFilter));

            Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreNotEqual(expected, actual, nodeFilter));
            Assert.Throws<AssertionException>(() => Assert.That(actual, IsObjectTree.NotEqualTo(expected).WithFilter(nodeFilter)));
            Assert.Throws<AssertionException>(() => Assert.That(actual, Is.Not.ObjectTreeEqualTo(expected).WithFilter(nodeFilter)));
        }

        private void AssertAreNotEqual(object expected, object actual)
        {
            ObjectTreeAssert.AreNotEqual(expected, actual);
            Assert.That(actual, IsObjectTree.NotEqualTo(expected));
            Assert.That(actual, Is.Not.ObjectTreeEqualTo(expected));

            Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(expected, actual));
            Assert.Throws<AssertionException>(() => Assert.That(actual, IsObjectTree.EqualTo(expected)));
        }

        private void AssertAreNotEqual(object expected, object actual, IObjectTreeNodeFilter nodeFilter)
        {
            ObjectTreeAssert.AreNotEqual(expected, actual, nodeFilter);
            Assert.That(actual, IsObjectTree.NotEqualTo(expected).WithFilter(nodeFilter));
            Assert.That(actual, Is.Not.ObjectTreeEqualTo(expected).WithFilter(nodeFilter));

            Assert.Throws<AssertionException>(() => ObjectTreeAssert.AreEqual(expected, actual, nodeFilter));
            Assert.Throws<AssertionException>(() => Assert.That(actual, IsObjectTree.EqualTo(expected).WithFilter(nodeFilter)));
        }

        #endregion

        #region Samples

        private Address CreateSampleAddress()
        {
            var address = new Address
            {
                Id = 1,
                Lines = new[] { "123 Fake ST", "Arlington, VA 22222" },
            };
            return address;
        }

        private Company CreateSampleCompany()
        {
            var company = new Company
            {
                Id = 1,
                Name = "The Company",
                Employees = new[]
                {
                    new Employee
                    {
                        Id = 2,
                        FullName = "Robert Paulson",
                        Addresses = new[]
                        {
                            new Address { Id = 3, AddressType = AddressType.Home, Lines = new[] { "123 Fake ST", "Arlington, VA 22222" } },
                            new Address { Id = 4, AddressType = AddressType.Work, Lines = new[] { "2 Company BLVD", "Arlington, VA 22222" } }
                        },
                        Phones = new[]
                        {
                            new Phone { Id = 5, PhoneType = PhoneType.Cell, Number = "555-555-5555", }
                        },
                    },
                    new Employee
                    {
                        Id = 6,
                        FullName = "Jenny Heath",
                        Phones = new[]
                        {
                            new Phone { Id = 7, PhoneType = PhoneType.Home, Number = "555-867-5309", }
                        },
                    },
                },
            };
            return company;
        }

        private Person CreateSamplePerson_OwnFatherAndMotherSomehow()
        {
            var person = new Person { FullName = "a" };
            person.Father = person;
            person.Mother = person;
            person.Children = new[] { person };
            return person;
        }

        private Person CreateSamplePerson_OwnGrandpa()
        {
            var protagonistFather = new Person { FullName = "Protagonist's Father" };
            var protagonistMother = new Person { FullName = "Protagonist's Mother" };
            var protagonist = new Person { FullName = "Protagonist", Father = protagonistFather, Mother = protagonistMother };

            var widow = new Person { FullName = "Widow" };
            var deadMan = new Person { FullName = "Dead Man " };
            var widowDaughter = new Person { FullName = "Widow's Daughter", Father = deadMan, Mother = widow };

            var protagonistBaby = new Person { FullName = "Protagonist's Baby", Father = protagonist, Mother = widow };
            var protagonistStepBrotherAndStepGrandchild = new Person { FullName = "Grandchild", Father = protagonistFather, Mother = widowDaughter };

            protagonist.Spouses = new[] { widow };
            widow.Spouses = new[] { deadMan, protagonist };
            deadMan.Spouses = new[] { widow };
            protagonistMother.Spouses = new[] { protagonistFather };
            protagonistFather.Spouses = new[] { protagonistMother, widowDaughter };
            widowDaughter.Spouses = new[] { protagonistFather };

            protagonist.Children = new[] { protagonistBaby };
            protagonistFather.Children = new[] { protagonist, protagonistStepBrotherAndStepGrandchild };
            protagonistMother.Children = new[] { protagonist };
            widow.Children = new[] { widowDaughter, protagonistBaby };
            widowDaughter.Children = new[] { protagonistStepBrotherAndStepGrandchild };

            return protagonist;
        }

        #endregion

        #region Test Classes

        private class Company
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public ICollection<Employee> Employees { get; set; }
        }

        private class Employee : Person
        {
            public string EmployeeIdentifier { get; set; }
        }

        private class Person
        {
            public int Id { get; set; }
            public string FullName { get; set; }
            public Address HomeAddress => Addresses?.FirstOrDefault(a => a.AddressType == AddressType.Home);
            public ICollection<Address> Addresses { get; set; }
            public Phone HomePhone => Phones?.FirstOrDefault(p => p.PhoneType == PhoneType.Home);
            public ICollection<Phone> Phones { get; set; }

            public Person Father { get; set; }
            public Person Mother { get; set; }
            public ICollection<Person> Spouses { get; set; }
            public ICollection<Person> Children { get; set; }
        }

        private class Address
        {
            public int Id
            {
                get; set;
            }
            public AddressType? AddressType { get; set; }
            public ICollection<string> Lines { get; set; }
        }

        private enum AddressType
        {
            Home, Work
        }

        private class Phone
        {
            public int Id { get; set; }
            public PhoneType? PhoneType { get; set; }
            public string Number { get; set; }
        }

        private enum PhoneType
        {
            Home, Cell, Work
        }

        private class ListTestObject<T>
        {
            public IEnumerable<T> Enumerable { get; set; } = new T[0];
            public IReadOnlyCollection<T> ReadOnlyCollection { get; set; } = new T[0];
            public ICollection<T> Collection { get; set; } = new T[0];
            public T[] Array { get; set; } = new T[0];
            public IList<T> IList { get; set; } = new T[0];
            public List<T> List { get; set; } = new List<T>();
        }

        #endregion
    }
}
