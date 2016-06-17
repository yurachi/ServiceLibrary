using NUnit.Framework;
using ServiceLibrary.utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLibrary_Tests.SortedListHelperTests
{
    [TestFixture]
    class ExcludeTests
    {
        [Test]
        public void EmptyCollectionsReturnsNoElements()
        {
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void EmptyAndNonEmptyCollectionsReturnsNoElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionB.Add(element);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void NonEmptyAndnEmptyCollectionsReturnsAllElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.Single());
        }

        [Test]
        public void SameElementCollectionsReturnsNoElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionB.Add(element);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void DifferentElementCollectionReturnsFirstCollectionElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionB.Add(element + 5);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(element, actual.Single());
        }

        [Test]
        public void OneAndTwoElementsCollectionReturnsNoElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionB.Add(element);
            collectionB.Add(element + 5);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void TwoAndTwoElementsCollectionReturnsElement11()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element - 5);
            collectionA.Add(element);
            collectionB.Add(element);
            collectionB.Add(element + 5);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(element - 5, actual.Single());
        }

        [Test]
        public void TwoAndTwoElementsCollectionReturnsElement12()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionA.Add(element + 2);
            collectionB.Add(element - 5);
            collectionB.Add(element);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(element + 2, actual.Single());
        }

        [Test]
        public void TwoAndTwoElementsCollectionExcludeBothElements()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(element);
            collectionA.Add(element + 2);
            collectionB.Add(element);
            collectionB.Add(element + 2);
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(0, actual.Count());
        }

        [Test]
        public void LongCollectionExcludeTheTwoElementsInTheMiddle()
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
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(5, actual.Count());
            Assert.AreEqual(-1, actual.IndexOf(element));
        }


        [Test]
        public void TwoLongCollectionsExcludeTheTwoElementsInTheMiddle()
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
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(5, actual.Count());
            Assert.AreEqual(-1, actual.IndexOf(element + 10));
        }

        [Test]
        public void TwoLongCollectionsWithRepeatedExcludeTheTwoElementsInTheMiddle()
        {
            var element = 12;
            var collectionA = new List<int>();
            var collectionB = new List<int>();
            collectionA.Add(1);
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
            var actual = SortedListHelper.Exclude(collectionA, collectionB, x => x);
            Assert.AreEqual(5, actual.Count());
            Assert.AreEqual(-1, actual.IndexOf(element + 10));
        }
    }
}
