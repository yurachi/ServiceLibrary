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
    class FilterBlocksOfElementsTests
    {
        [Test]
        public void EmptyFiltersOnEmptyCollectionReturnsEmpty()
        {
            var collection = new List<int>();
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x=>x);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void EmptyFiltersOnNonEmptyCollectionReturnsEmpty()
        {
            var collection = new List<int>() { 1, 2, 3 };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void NonEmptyFiltersOnEmptyCollectionReturnsEmpty()
        {
            var collection = new List<int>();
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x, x => x==8);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void OneMatchFiltersReturnsOneMatch()
        {
            var collection = new List<int>() { 1, 2, 3 };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x, 
                x => x==1, 
                x => x!=0);
            Assert.AreEqual(1, actual.Single());
        }

        [Test]
        public void DifferentSequentKeysMatchesFilterReturnsAllMatches()
        {
            var collection = new List<int>() { 0, 1, 2, 3 };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x, x => x != 0);
            Assert.AreEqual(3, actual.Count());
        }

        [Test]
        public void TwoNonSequentMatchesFiltersReturnsTwoMatches()
        {
            var collection = new List<int>() { 1, 2, 3 };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x, 
                x => (x % 2) == 0, 
                x => x != 0);
            Assert.AreEqual(2, actual.Single());
        }

        [Test]
        public void TwoSequentMatchesFiltersReturnsTwoMatches()
        {
            var collection = new List<int>() { -1, 0, 1, 2, 3 };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x, 
                x => x > -1, 
                x => x != 0);
            Assert.AreEqual(3, actual.Count());
        }

        [Test]
        public void TwoNonSequentBlocksMatchFiltersReturnsTwoBlocksMatches()
        {
            var collection = new List<int>() { 1, 2, 2, 3, 3 };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x, 
                x => (x % 2) == 1,
                x => x != 0);
            Assert.AreEqual(3, actual.Count());
        }

        [Test]
        public void TwoBlocksWithSameKeysMatchFiltersReturnsTwoMatchesWithSameKeys()
        {
            var collection = new List<int>() {0, 0, 0, 1, 1, 2, 2, 3, 3, 3, 4 };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x, 
                x => (x % 2) == 1, 
                x => x != 0);
            Assert.AreEqual(5, actual.Count());
        }

        [Test]
        public void ThreeBlocksWithSameKeysEachHasAtLeastOneMismatchLineReturnsEmpty()
        {
            var collection = new List<Tuple<int,int>>() 
            { 
                new Tuple<int,int>(0, 0),
                new Tuple<int,int>(0, 1),
                new Tuple<int,int>(1, 2),
                new Tuple<int,int>(1, 3),
                new Tuple<int,int>(2, 1),
                new Tuple<int,int>(2, 0)
            };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x.Item1, 
                x => (x.Item1 % 2) == 0, 
                x => x.Item2 != 0);
            Assert.IsEmpty(actual);
        }

        [Test]
        public void ThreeBlocksWithSameKeysFirstMatchesFiltersReturnsMatchingBlock()
        {
            var collection = new List<Tuple<int, int>>() 
            { 
                new Tuple<int,int>(0, 0),
                new Tuple<int,int>(0, 1),
                new Tuple<int,int>(1, 2),
                new Tuple<int,int>(1, 3),
                new Tuple<int,int>(2, 1),
                new Tuple<int,int>(2, 3)
            };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x.Item1,
                x => (x.Item1 % 2) == 0, 
                x => x.Item2 < 2);
            Assert.AreEqual(2, actual.Count());
        }

        [Test]
        public void ThreeBlocksWithSameKeysLastMatchesFiltersReturnsMatchingBlock()
        {
            var collection = new List<Tuple<int, int>>() 
            { 
                new Tuple<int,int>(0, 0),
                new Tuple<int,int>(0, 1),
                new Tuple<int,int>(1, 2),
                new Tuple<int,int>(1, 3),
                new Tuple<int,int>(2, 1),
                new Tuple<int,int>(2, 3)
            };
            var actual = SortedListHelper.FilterBlocksOfElements(collection, x => x.Item1, 
                x => (x.Item1 % 2) == 0, 
                x => x.Item2 != 0, 
                x => (x.Item2 % 2) == 1);
            Assert.AreEqual(2, actual.Count());
        }
    }
}
