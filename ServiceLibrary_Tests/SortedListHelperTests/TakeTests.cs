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
    class TakeTests
    {
        [Test]
        public void EmptyCollectionTakeNoElements()
        {
            var emptyCollection = new List<int>();
            var actual = SortedListHelper.Take(emptyCollection, 42, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void OneElementCollectionTakeTheElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void OneElementCollectionTakeNoElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(10);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void TwoElementsCollectionTakeTheFirstElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element);
            collection.Add(element + 1);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void TwoElementsCollectionTakeTheSecondElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element - 1);
            collection.Add(element);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void LongCollectionTakeTheTwoElementsAtTheEnd()
        {
            var element = 120;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(10);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            collection.Add(element);
            collection.Add(element);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(2, actual.Count());
        }


        [Test]
        public void LongCollectionTakeTheTwoElementsInTheMiddle()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(element);
            collection.Add(element);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void LongCollectionTakeTheThreeElementsInTheMiddle()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(10);
            collection.Add(element);
            collection.Add(element);
            collection.Add(element);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(3, actual.Count());
        }

        [Test]
        public void LongCollectionTakeTheElementAtTheEnd()
        {
            var element = 220;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(10);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            collection.Add(element);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(1, actual.Count());
        }

        [Test]
        public void LongCollectionDoesNotContainElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(1);
            collection.Add(10);
            collection.Add(100);
            collection.Add(101);
            collection.Add(101);
            var actual = SortedListHelper.Take(collection, element, x => x);
            Assert.AreEqual(0, actual.Count());
        }
    }
}
