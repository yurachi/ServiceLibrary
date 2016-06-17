using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ServiceLibrary.utility
{
    public static class SortedListHelper
    {
        /// <summary>
        /// Extracts distinct keys from the sorted list. 
        /// </summary>
        /// <param name="collection">list sorted by the key</param>
        /// <param name="GetKey">key extractor e.g. x=>x.Key</param>
        /// <returns></returns>
        public static IEnumerable<TResult> ExtractDistinctKeys<TSource, TResult>(IEnumerable<TSource> collection, Func<TSource, TResult> GetKey)
        {
            var result = new List<TResult>();
            if(collection.Count() > 0)
            {
                var previousKey = GetKey(collection.First());
                result.Add(previousKey);
                foreach(var element in collection)
                {
                    if(! GetKey(element).Equals(previousKey))
                    {
                        previousKey = GetKey(element);
                        result.Add(previousKey);
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// binary search for the item in the collection
        /// </summary>
        /// <typeparam name="TSource">should implement int CompareTo(TSource)</typeparam>
        /// <param name="collection">sorted list with [int] accessor</param>
        /// <param name="item"></param>
        /// <returns>true if an equal element found, false otherwise</returns>
        public static bool Contains<TSource>(IList<TSource> collection, TSource item) where TSource:IComparable<TSource>
        {
            var leftBorder = 0;
            var rightBorder = collection.Count - 1;

            if (rightBorder < 0)//no elements
            {
                return false;
            }

            if (rightBorder == 0)//only 1 element
            {
                return collection[0].CompareTo(item) == 0;
            }

            if(collection[0].CompareTo(item) > 0) //item is less than the least collection element
            {
                return false;
            }

            if(collection[rightBorder].CompareTo(item) < 0)//item is greater than the highest collection element
            {
                return false;
            }

            while (rightBorder > leftBorder)
            {
                var middle = leftBorder + (rightBorder - leftBorder) / 2;
                var compareResult = collection[middle].CompareTo(item);

                if (compareResult == 0)
                {
                    return true;
                }
                else if(rightBorder == leftBorder)
                {
                    return false;
                }
                else if(compareResult < 0)
                {
                    if(middle == leftBorder) //only two elements
                    {
                        return collection[rightBorder].CompareTo(item) == 0;
                    }

                    leftBorder = middle;
                }
                else
                {
                    rightBorder = middle;
                }
            }
            return false;
        }

        /// <summary>
        /// binary search for the first element with the given key in the collection
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey">should implement int CompareTo(TKey)</typeparam>
        /// <param name="collection">sorted list with [int] accessor</param>
        /// <param name="key"></param>
        /// <param name="GetKey">key extractor e.g. x=>x.Key</param>
        /// <returns>0-based index of the first element with the key or -1 of not found. Any key is not found in an empty collection</returns>
        public static int IndexOf<TSource, TKey>(IList<TSource> collection, TKey key, Func<TSource, TKey> GetKey) where TKey:IComparable<TKey>
        {
            var leftBorder = 0;
            var rightBorder = collection.Count - 1;

            if (rightBorder == 0) //only 1 element
            {
                return (GetKey(collection[0]).CompareTo(key) == 0) ? 0 : -1;
            }

            while (rightBorder > leftBorder)
            {
                var middle = leftBorder + (rightBorder - leftBorder) / 2;

                var compareResult = GetKey(collection[middle]).CompareTo(key);

                if (compareResult == 0)
                {
                    if (rightBorder - leftBorder == 1) //only two elements
                    {
                        return middle;
                    }
                    else
                    {
                        rightBorder = middle; // shift left in order to find the very first element
                    }
                }
                else if (compareResult < 0)
                {
                    if (middle == leftBorder) //only two elements
                    {
                        if(GetKey(collection[rightBorder]).CompareTo(key) == 0)
                        {
                            return rightBorder;
                        }
                        else
                        {
                            return -1;
                        }
                    }
                    leftBorder = middle;
                }
                else
                {
                    rightBorder = middle;
                }
            }
            return -1;
        }

        /// <summary>
        /// retrieves the part of the collection that matches the key
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey">should implement int CompareTo(TKey)</typeparam>
        /// <param name="collection">sorted list with [int] accessor</param>
        /// <param name="key">key value</param>
        /// <param name="GetKey">key extractor e.g. x=>x.Key</param>
        /// <returns></returns>
        public static IList<TSource> Take<TSource, TKey>(IList<TSource> collection, TKey key, Func<TSource, TKey> GetKey) where TKey:IComparable<TKey>
        {
            var result = new List<TSource>();
            if (collection.Count() > 0)
            {
                var keyIndex = IndexOf(collection, key, GetKey);

                while (keyIndex >= 0 && keyIndex < collection.Count())
                {
                    var element = collection[keyIndex];
                    if (GetKey(element).CompareTo(key) == 0)
                    {
                        result.Add(element);
                        ++keyIndex;
                    }
                    else
                    {
                        keyIndex = -1;
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// creates a copy of the collection excluding the blocks of the elements with the same key where
        /// one of the elements doesn't match the criteria
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="collection"></param>
        /// <param name="GetKey">key extractor e.g. x=>x.Key</param>
        /// <param name="IsIncluded"></param>
        /// <returns></returns>
        public static IList<TSource> FilterBlocksOfElements<TSource, TKey>(IList<TSource> collection, Func<TSource, TKey> GetKey, params Func<TSource, bool> [] IsIncluded) where TKey : IComparable<TKey>
        {
            if (collection.Count() == 0)
            {
                return collection;
            }

            if(IsIncluded.Count() == 0)
            {
                return new List<TSource>();
            }

            var result = new List<TSource>(collection.Count());
            var buffer = new List<TSource>();
            var excludedLineDetected = false;
            var previousKey = GetKey(collection.First());

            foreach (var line in collection)
            {
                if (GetKey(line).CompareTo(previousKey) == 0)
                {
                    if (!excludedLineDetected)
                    {
                        if (IsIncluded.All(isIncluded => isIncluded(line)))
                        {
                            buffer.Add(line);
                        }
                        else
                        {
                            excludedLineDetected = true;
                        }
                    }
                }
                else  //line has a new key
                {
                    if ((!excludedLineDetected) && buffer.Count() > 0)
                    {
                        result.AddRange(buffer);
                    }

                    buffer.Clear();

                    if (IsIncluded.All(isIncluded => isIncluded(line))) 
                    {
                        buffer.Add(line);
                        excludedLineDetected = false;
                    }
                    else
                    {
                        excludedLineDetected = true;
                    }
                    previousKey = GetKey(line);
                }
            }
            if ( (!excludedLineDetected) && buffer.Count() > 0) //last buffer
            {
                result.AddRange(buffer);
            }
            return result;
        }

        /// <summary>
        /// creates a copy of the collection excluding the elements that don't match the criteria
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="collection"></param>
        /// <param name="IsIncluded"></param>
        /// <returns></returns>
        public static IList<TSource> FilterSingleElements<TSource>(IList<TSource> collection, Func<TSource, bool> IsIncluded)
        {
            var result = new List<TSource>(collection.Count());
            foreach (var line in collection)
            {
                if (IsIncluded(line))
                {
                    result.Add(line);
                }
            }
            return result;
        }

        /// <summary>
        /// compares two sorted lists and produces the sorted list with all elements from the first list that match an element from the second list by key
        /// </summary>
        /// <typeparam name="TSourceA"></typeparam>
        /// <typeparam name="TSourceB"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <param name="GetKey">key extractor e.g. x=>x.Key</param>
        /// <returns></returns>
        public static IList<TSourceA> Intersect<TSourceA, TSourceB, TKey>(IList<TSourceA> listA, IList<TSourceB> listB, Func<TSourceA, TKey> GetKeyA, Func<TSourceB, TKey> GetKeyB) where TKey : IComparable<TKey>
        {
            var result = new List<TSourceA>(listA.Count);

            var iA = 0;
            var iB = 0; 

            while((iA < listA.Count) && (iB < listB.Count)) // if any list contains more elements than the other, they will be excluded
            {
                var comparisonResult = 
                        GetKeyA(listA[iA])
                    .CompareTo(
                        GetKeyB(listB[iB]));

                if(comparisonResult < 0) //list A element is lesser than list B element
                {
                    ++iA;
                }
                else if(comparisonResult == 0)  
                {
                    result.Add(listA[iA]);
                    ++iA;
                }
                else //list B element is lesser than list A element
                {
                    ++iB;
                }
            }
            return result;
        }

        public static IList<TSource> Intersect<TSource, TKey>(IList<TSource> listA, IList<TSource> listB, Func<TSource, TKey> GetKey) where TKey : IComparable<TKey>
        {
            return Intersect(listA, listB, GetKey, GetKey);
        }

        /// <summary>
        /// compares two sorted lists and produces the sorted list with all elements from the first list that do not match an element from the second list by key
        /// </summary>
        /// <typeparam name="TSourceA"></typeparam>
        /// <typeparam name="TSourceB"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="listA"></param>
        /// <param name="listB"></param>
        /// <param name="GetKey">key extractor e.g. x=>x.Key</param>
        /// <returns></returns>
        public static IList<TSourceA> Exclude<TSourceA, TSourceB, TKey>(IList<TSourceA> listA, IList<TSourceB> listB, Func<TSourceA, TKey> GetKeyA, Func<TSourceB, TKey> GetKeyB) where TKey : IComparable<TKey>
        {
            var result = new List<TSourceA>(listA.Count);

            var iA = 0;
            var iB = 0;

            while (iA < listA.Count) 
            {
                var comparisonResult = -1; //by default copy element from listA
                if (iB < listB.Count)
                {
                    comparisonResult =
                            GetKeyA(listA[iA])
                        .CompareTo(
                            GetKeyB(listB[iB]));
                }
                if (comparisonResult < 0) //list A element is lesser than list B element
                {
                    result.Add(listA[iA]);
                    ++iA;
                }
                else if (comparisonResult == 0)
                {
                    ++iA;
                }
                else //list B element is lesser than list A element
                {
                    ++iB;
                }
            }
            return result;
        }

        public static IList<TSource> Exclude<TSource, TKey>(IList<TSource> listA, IList<TSource> listB, Func<TSource, TKey> GetKey) where TKey : IComparable<TKey>
        {
            return Exclude(listA, listB, GetKey, GetKey);
        }
    }
}
