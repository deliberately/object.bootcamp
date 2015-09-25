using System.Collections.Generic;
using System.Linq;

namespace OO.Bootcamp.Graphs
{
    public static class ListExtensions
    {
        public static List<T> With<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static T Minimum<T>(this List<T> list, IComparer<T> comparer)
        {
            var copy = new List<T>(list);
            copy.Sort(comparer);
            return copy.First();
        }

        public static List<T> Copy<T>(this List<T> list)
        {
            return new List<T>(list);
        }
    }
}