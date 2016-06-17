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
    class IndexOfTests
    {
        [Test]
        public void EmptyCollectionIndexOfNoElements()
        {
            var emptyCollection = new List<int>();
            var actual = SortedListHelper.IndexOf(emptyCollection, 42, x=>x);
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void OneElementCollectionIndexOfTheElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element);
            var actual = SortedListHelper.IndexOf(collection, element, x=>x);
            Assert.AreEqual(0, actual);
        }

        [Test]
        public void OneElementCollectionIndexOfNoElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(10);
            var actual = SortedListHelper.IndexOf(collection, element, x => x);
            Assert.AreEqual(-1, actual);
        }

        [Test]
        public void TwoElementsCollectionIndexOfTheFirstElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element);
            collection.Add(element + 1);
            var actual = SortedListHelper.IndexOf(collection, element, x=>x);
            Assert.AreEqual(0,actual);
        }

        [Test]
        public void TwoElementsCollectionIndexOfTheSecondElement()
        {
            var element = 12;
            var collection = new List<int>();
            collection.Add(element - 1);
            collection.Add(element);
            var actual = SortedListHelper.IndexOf(collection, element, x=>x);
            Assert.AreEqual(1, actual);
        }

        [Test]
        public void LongCollectionIndexOfTheFirstOfTwoElementsAtTheEnd()
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
            var actual = SortedListHelper.IndexOf(collection, element, x => x);
            Assert.AreEqual(6, actual);
        }


        [Test]
        public void LongCollectionIndexOfTheFirstOfTwoElementsInTheMiddle()
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
            var actual = SortedListHelper.IndexOf(collection, element, x=>x);
            Assert.AreEqual(2,actual);
        }

        [Test]
        public void LongCollectionIndexOfTheFirstOfThreeElementsInTheMiddle()
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
            var actual = SortedListHelper.IndexOf(collection, element, x => x);
            Assert.AreEqual(3, actual);
        }

        [Test]
        public void LongCollectionIndexOfTheElementAtTheEnd()
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
            var actual = SortedListHelper.IndexOf(collection, element, x => x);
            Assert.AreEqual(6, actual);
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
            var actual = SortedListHelper.IndexOf(collection, element, x=>x);
            Assert.AreEqual(-1, actual);
        }
    }
}
