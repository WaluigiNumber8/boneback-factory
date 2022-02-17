using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using BoubakProductions.Safety;
using UnityEngine;

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
            SafetyNet.EnsureIsNotNull(list, "List to Check");
            
            if (list.Count <= 1) return 0;
            
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
        /// Checks if a list contains a specific object.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="value">The object to check for.</param>
        /// <typeparam name="T">Any class type.</typeparam>
        /// <returns>True if the list contains the given object.</returns>
        public static bool ContainsValue<T>(this IList<T> list, T value) where T : class
        {
            if (list == null || list.Count <= 0) return false;
            
            SafetyNet.EnsureIsNotNull(list, "List to check.");
            return list.Any(element => element == value);
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
        
    }
}