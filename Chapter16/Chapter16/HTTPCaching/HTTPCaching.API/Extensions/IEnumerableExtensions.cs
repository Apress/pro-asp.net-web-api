using System;
using System.Linq;
using System.Web;

namespace System.Collections.Generic {

    internal static class IEnumerableExtensions {

        public static void ForEach<T>(this IEnumerable<T> enumerable, Action<T> action) {

            foreach (var item in enumerable)
                action(item);
        }
    }
}