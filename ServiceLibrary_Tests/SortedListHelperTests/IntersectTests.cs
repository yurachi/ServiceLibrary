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
    class IntersectTests
    {
        [Test]
        public void EmptyCollectionsIntersectNoElements()
        {
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void EmptyAndNonEmptyCollectionsInterSectNoElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionB.Add(element);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void NonEmptyAndnEmptyCollectionsInterSectNoElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void SameElementCollectionsInterSectElement()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionB.Add(element);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.Single());
        }

        [Test]
        public void DifferentElementCollectionIntersectNoElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionB.Add(element + 5);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void OneAndTwoElementsCollectionIntersectTheFirstElement()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionB.Add(element);
            collectionB.Add(element + 5);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.Single());
        }

        [Test]
        public void TwoAndTwoElementsCollectionIntersectElement12And21()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element - 5);
            collectionA.Add(element);
            collectionB.Add(element);
            collectionB.Add(element + 5);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.Single());
        }

        [Test]
        public void TwoAndTwoElementsCollectionIntersectElement11And22()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionA.Add(element + 2);
            collectionB.Add(element - 5);
            collectionB.Add(element);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.Single());
        }

        [Test]
        public void TwoAndTwoElementsCollectionIntersectBothElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionA.Add(element + 2);
            collectionB.Add(element);
            collectionB.Add(element + 2);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.First());
            Assert.AreEqual(element + 2, actual.Last());
        }

        [Test]
        public void LongCollectionIntersectTheTwoElementsInTheMiddle()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(1);
            collectionA.Add(10);
            collectionA.Add(element);
            collectionA.Add(element + 5);
            collectionA.Add(element + 10);
            collectionA.Add(100);
            collectionA.Add(101);
            collectionB.Add(element);
            collectionB.Add(element + 10);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.First());
            Assert.AreEqual(element + 10, actual.Last());
        }


        [Test]
        public void TwoLongCollectionsIntersectTheTwoElementsInTheMiddle()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(1);
            collectionA.Add(10);
            collectionA.Add(element);
            collectionA.Add(15);
            collectionA.Add(element + 10);
            collectionA.Add(101);
            collectionA.Add(105);
            collectionB.Add(5);
            collectionB.Add(element);
            collectionB.Add(13);
            collectionB.Add(14);
            collectionB.Add(16);
            collectionB.Add(element + 10);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.First());
            Assert.AreEqual(element + 10, actual.Last());
        }

        [Test]
        public void TwoLongCollectionsWithRepeatsIntersectTheTwoElementsInTheMiddle()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(1);
            collectionA.Add(10);
            collectionA.Add(10);
            collectionA.Add(element);
            collectionA.Add(element);
            collectionA.Add(15);
            collectionA.Add(element + 10);
            collectionA.Add(101);
            collectionA.Add(105);
            collectionB.Add(5);
            collectionB.Add(element);
            collectionB.Add(13);
            collectionB.Add(14);
            collectionB.Add(16);
            collectionB.Add(element + 10);
            collectionB.Add(element + 10);
            collectionB.Add(element + 20);
            var actual = SortedListHelper.Intersect(collectionA, collectionB, x => x);
            Assert.AreEqual(3, actual.Count);
            Assert.AreEqual(element, actual[0]);
            Assert.AreEqual(element, actual[1]);
            Assert.AreEqual(element + 10, actual[2]);
        }

    }
}
