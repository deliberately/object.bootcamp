using System.Collections.Generic;

namespace OO.Bootcamp.Graphs
{
    public static class ListExtensions
    {
        public static List<T> With<T>(this List<T> list, T item)
        {
            list.Add(item);
            return list;
        }

        public static List<T> Copy<T>(this List<T> list)
        {
            return new List<T>(list);
        }
    }
}