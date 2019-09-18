using System.Collections.Generic;
using System.Linq;

namespace Pickler.Infrastructure.Parsing.Gherkin.Extensions
{
    internal static class ArrayEx
    {
        public static IEnumerable<T> Add<T>(this IEnumerable<T> array, T element)
        {
            var list = (array ?? new T[0]).ToList();
            list.Add(element);
            return list;
        }
    }
}
