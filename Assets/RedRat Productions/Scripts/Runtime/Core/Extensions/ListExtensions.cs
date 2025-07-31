using System;
using System.Collections.Generic;
using System.Linq;
using RedRats.Safety;

namespace RedRats.Core
{
    /// <summary>
    /// Contains extension methods for the the <see cref="IList{T}"/> type interface.
    /// </summary>
    public static class ListExtensions
    {
        /// <summary>
        /// Returns the amount of duplicate entries in a list.
        /// </summary>
        /// <typeparam name="T">List type.</typeparam>
        /// <param name="list">The list to search.</param>
        /// <returns>Amount of duplicate entries.</returns>
        public static int GetDuplicatesCount<T>(this IList<T> list, out IList<string> duplicateValues)
        {
            Preconditions.IsNotNull(list, "List to Check");
            duplicateValues = new List<string>();
            
            if (list.Count <= 1) return 0;
            
            ISet<T> set = new HashSet<T>();
            ISet<T> duplicates = new HashSet<T>();
            int count = 0;
            foreach (T item in list)
            {
                if (!set.Add(item))
                {
                    duplicateValues.Add(item.ToString());
                    count++;
                    if (duplicates.Add(item)) count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Checks if a list contains a specific object.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="value">The object to check for.</param>
        /// <typeparam name="T">Any class type.</typeparam>
        /// <returns>True if the list contains the given object.</returns>
        public static bool ContainsValue<T>(this IList<T> list, T value) where T : class
        {
            if (list == null || list.Count <= 0) return false;
            return list.Any(element => element == value);
        }
        
        /// <summary>
        /// Checks if a list contains a specific value.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="value">The object to check for.</param>
        /// <typeparam name="T">Any <see cref="IComparable"/> type.</typeparam>
        /// <returns>True if the list contains the given value.</returns>
        public static bool ContainsComparableValue<T>(this ICollection<T> list, T value) where T : IComparable
        {
            if (list == null || list.Count <= 0) return false;
            return list.Any(element => element.CompareTo(value) == 0);
        }

        /// <summary>
        /// Tries to get index of a specific value from a list.
        /// </summary>
        /// <param name="list">The list to search.</param>
        /// <param name="value">The value, whose index is needed.</param>
        /// <typeparam name="T">Any class type.</typeparam>
        /// <returns>The index of the value.</returns>
        /// <exception cref="ArgumentNullException">Is thrown when the value was not found.</exception>
        public static int GetIndexFirst<T>(this IList<T> list, T value) where T : class
        {
            if (list == null || list.Count <= 0) throw new ArgumentNullException($"No element with value '{value}' was found.");
            
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] == value) return i;
            }

            throw new ArgumentNullException($"No element with value '{value}' was found.");
        }
        
        /// <summary>
        /// Resize a list and keep it's existing values.
        /// </summary>
        /// <param name="list">The list to resize.</param>
        /// <param name="size">The new size.</param>
        /// <param name="element">Initial value of newly added positions.</param>
        /// <typeparam name="T">Any Type.</typeparam>
        public static void Resize<T>(this List<T> list, int size, T element = default(T))
        {
            int count = list.Count;

            if (count == size) return;
            if (size < count)
            {
                list.RemoveRange(size, count - size);
                return;
            }
            
            if (size > list.Capacity) list.Capacity = size;
            list.AddRange(Enumerable.Repeat(element, size - count));
        }

        /// <summary>
        /// Removes all elements from the list, if they pass a specific condition.
        /// </summary>
        /// <param name="list">The list to remove from.</param>
        /// <param name="condition">Each element for which this ends TRUE gets removed from the list.</param>
        /// <typeparam name="T">Any type.</typeparam>
        public static void RemoveAll<T>(this IList<T> list, Predicate<T> condition)
        {
            Preconditions.IsNotNull(list, nameof(list));
            if (list.Count <= 0) return;
            for (int i = 0; i < list.Count; i++)
            {
                if (!condition(list[i])) continue;
                list.RemoveAt(i);
            }
        }
    }
}