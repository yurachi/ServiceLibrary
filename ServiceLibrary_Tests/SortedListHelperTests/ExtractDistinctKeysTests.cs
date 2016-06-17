using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using ServiceLibrary.utility;

namespace ServiceLibrary_Tests
{
    [TestFixture]
    public class ExtractDistinctKeysTests
    {
        [Test]
        public void Check0KeyCount()
        {
            var expected = 0;
            var collection = new Tuple<int, string>[0];
            var actual = SortedListHelper.ExtractDistinctKeys(collection, x => x.Item1).Count();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check1KeyCount()
        {
            var expected = 1;
            var collection = new[] { new Tuple<int, string>(1, "one"), new Tuple<int, string>(1, "two") };
            var actual = SortedListHelper.ExtractDistinctKeys(collection, x => x.Item1).Count();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check2KeysCount()
        {
            var expected = 2;
            var collection = new[] { new Tuple<int, string>(1, "one"), new Tuple<int, string>(1, "two"), new Tuple<int, string>(2, "three") };
            var actual = SortedListHelper.ExtractDistinctKeys(collection, x => x.Item1).Count();
            Assert.AreEqual(expected, actual);
        }
        [Test]
        public void Check3KeysCount()
        {
            var expected = 3;
            var collection = new[] { new Tuple<int, string>(1, "one"), new Tuple<int, string>(2, "two"), new Tuple<int, string>(2, "tree"), new Tuple<int, string>(3, "four"), new Tuple<int, string>(3, "five") };
            var actual = SortedListHelper.ExtractDistinctKeys(collection, x => x.Item1).Count();
            Assert.AreEqual(expected, actual);
        }
    }
}
