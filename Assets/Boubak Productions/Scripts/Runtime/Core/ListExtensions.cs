using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;

namespace BoubakProductions.Core
{
    public static class ListExtensions
    {
        /// <summary>
        /// Returns the amount of duplicate entries in a list.
        /// </summary>
        /// <typeparam name="T">List type.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>Amount of duplicate entries.</returns>
        public static int GetDuplicatesCount<T>(this IList<T> list)
        {
            SafetyNet.EnsureIsNotNull(list, $"{nameof(list)} - List to get duplicates from.");
            if (list.Count <= 0) return 0;
            
            ISet<T> set = new HashSet<T>();
            ISet<T> duplicates = new HashSet<T>();
            int count = 0;
            foreach (T item in list)
            {
                if (!set.Add(item))
                {
                    count++;
                    if (duplicates.Add(item)) count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Checks if a given value is already saved on the list.
        /// </summary>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value to check for.</param>
        /// <typeparam name="T">Any type.</typeparam>
        /// <returns>True if value is found on the list, otherwise returns false.</returns>
        public static bool IsOnList<T>(this IList<T> list, T value) where T : class
        {
            SafetyNet.EnsureIsNotNull(list, $"{nameof(list)} - List to Cast");
            return list.Any(listValue => listValue == value);
        }

    }
}