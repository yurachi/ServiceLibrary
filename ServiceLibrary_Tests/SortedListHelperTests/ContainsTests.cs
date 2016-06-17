using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLibrary.utility;

namespace ServiceLibrary_Tests.SortedListHelperTests
{
    [TestFixture]
    class ContainsTests
    {
        [Test]
        public void EmptyCollectionContainsNoElements()
        {
            var emptyCollection = new List<int>();
            var actual = SortedListHelper.Contains(emptyCollection, 42);
            Assert.IsFalse(actual);
        }

        [Test]
        public void OneElementCollectionContainsTheElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsTrue(actual);
        }

        [Test]
        public void OneElementCollectionDoesNotContainsNonElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element + 1);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsFalse(actual);
        }

        [Test]
        public void TwoElementsCollectionContainsTheFirstElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element);
            collection.Add(element + 1);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsTrue(actual);
        }

        [Test]
        public void TwoElementsCollectionContainsTheSecondElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element - 1);
            collection.Add(element);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsTrue(actual);
        }

        [Test]
        public void SeveralElementsCollectionContainsOneOfElements()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(element);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsTrue(actual);
        }

        [Test]
        public void SeveralElementsCollectionDoesNotContainNonElementInTheMiddle()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsFalse(actual);
        }

        [Test]
        public void SeveralElementsCollectionDoesNotContainNonElementAfterEnd()
        {
            var element = 120;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsFalse(actual);
        }

        [Test]
        public void SeveralElementsCollectionDoesNotContainNonElementBeforeBegin()
        {
            var element = 2;
            var collection = new List<int>();
            collection.Add(3);
            collection.Add(10);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            var actual = SortedListHelper.Contains(collection, element);
            Assert.IsFalse(actual);
        }
    }
}
