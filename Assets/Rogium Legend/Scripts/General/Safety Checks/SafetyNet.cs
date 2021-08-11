using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RogiumLegend.Global.SafetyChecks
{
    public class SafetyNet
    {
        public static event Action<string> OnFireErrorMessage;

        #region String Checks
        /// /// <summary>
        /// Checks if a string is shorter that minLimit.
        /// </summary>
        /// <param name="stringObject">The string to check.</param>
        /// <param name="minLimit">Minimum characters allowed for the string.</param>
        /// <param name="variableName">Description of wronged variable.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureStringMinLimit(string stringObject, int minLimit, string variableName)
        {
            if (stringObject.Length <= minLimit)
            {
                string message = $"The string '{variableName}' cannot have less or equal to {minLimit} characters! ({stringObject.Length})";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        /// <summary>
        /// Checks if a string is not longer that maxLimit.
        /// </summary>
        /// <param name="stringObject">The string to check.</param>
        /// <param name="maxLimit">Maximum characters allowed for the string.</param>
        /// <param name="variableName">Description of wronged variable.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureStringMaxLimit(string stringObject, int maxLimit, string variableName)
        {
            if (stringObject.Length >= maxLimit)
            {
                string message = $"The string '{variableName}' cannot have more or equal to {maxLimit} characters! ({stringObject.Length})";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        /// <summary>
        /// Checks if a string's amount of characters is within a given range.
        /// </summary>
        /// <param name="stringObject">The string to check.</param>
        /// <param name="minLimit">Range minimum.</param>
        /// <param name="maxLimit">Range maximum.</param>
        /// <param name="variableName">Name of the wronged variable.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureStringInRange(string stringObject, int minLimit, int maxLimit, string variableName)
        {
            EnsureStringMinLimit(stringObject, minLimit, variableName);
            EnsureStringMaxLimit(stringObject, maxLimit, variableName);
        }
        #endregion

        #region List Checks

        /// <summary>
        /// Checks if a given list is not empty.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="CollectionSizeException"></exception>
        public static void EnsureListIsNotEmpty<T>(IList<T> list, string variableName)
        {
            if (list.Count <= 0)
            {
                string message = $"The list '{variableName}' cannot be empty.";
                OnFireErrorMessage?.Invoke(message);
                throw new CollectionSizeException(message);
            }
        }

        /// <summary>
        /// Checks if a given list is not empty or null.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="CollectionSizeException"></exception>
        public static void EnsureListIsNotNull<T>(IList<T> list, string variableName)
        {
            if (list == null)
            {
                string message = $"The list '{variableName}' cannot be null.";
                OnFireErrorMessage?.Invoke(message);
                throw new CollectionSizeException(message);
            }
        }

        /// <summary>
        /// Checks if a given list is not null.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="CollectionSizeException"></exception>
        public static void EnsureListIsNotEmptyOrNull<T>(IList<T> list, string variableName)
        {
            if (list == null || list.Count <= 0)
            {
                string message = $"The list '{variableName}' cannot be empty or null.";
                OnFireErrorMessage?.Invoke(message);
                throw new CollectionSizeException(message);
            }
        }

        /// <summary>
        /// Checks if a given list has a size lower than a specific size.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="maxAllowedSize">Max allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="CollectionSizeException"></exception>
        public static void EnsureListIsNotLongerOrEqualThan<T>(IList<T> list, int maxAllowedSize, string variableName)
        {
            if (list.Count <= maxAllowedSize)
            {
                string message = $"The list '{variableName}' cannot have more than {maxAllowedSize} elements.";
                OnFireErrorMessage?.Invoke(message);
                throw new CollectionSizeException(message);
            }
        }

        #endregion

    }
}