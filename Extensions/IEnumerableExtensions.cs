using System.Collections.Generic;
using System.Linq;

namespace ReflectionDynamicSort.Extensions
{
    public static class IEnumerableExtensions
    {
        public static bool CheckNotNullAndAny<TSource>(this IEnumerable<TSource> input)
        {
            if (input != null && input.Any())
            {
                return true;
            }
            return false;
        }
    }
}
