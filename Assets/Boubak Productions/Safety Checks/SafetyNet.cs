using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BoubakProductions.Safety
{
    public class SafetyNet
    {
        public static event Action<string> OnFireErrorMessage;

        #region Object Checks

        /// <summary>
        /// Checks if a given object is not null.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureIsNotNull(object obj, string variableName)
        {
            if (obj == null)
            {
                string message = $"'{variableName}' cannot be null.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        #endregion

        #region Int Checks
        
        /// <summary>
        /// Checks if an integer is bigger than a specific size.
        /// </summary>
        /// <param name="integer">The INT to check.</param>
        /// <param name="minSize">Minimum size allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        public static void EnsureIntIsBiggerThan(int integer, int minSize, string variableName)
        {
            if (integer < minSize)
            {
                string message = $"Integer called '{variableName}' must be above {minSize}";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        /// <summary>
        /// Checks if an integer is bigger than or equal to a specific size.
        /// </summary>
        /// <param name="integer">The INT to check.</param>
        /// <param name="minSize">Minimum size allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        public static void EnsureIntIsBiggerOrEqualTo(int integer, int minSize, string variableName)
        {
            if (integer >= minSize)
            {
                string message = $"Integer called '{variableName}' must be above or equal to {minSize}";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        /// <summary>
        /// Checks if an integer is smaller than a specific size.
        /// </summary>
        /// <param name="integer">The INT to check.</param>
        /// <param name="maxSize">Maximum size allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        public static void EnsureIntIsSmallerThan(int integer, int maxSize, string variableName)
        {
            if (integer < maxSize)
            {
                string message = $"Integer called '{variableName}' must be below {maxSize}";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        /// <summary>
        /// Checks if an integer is smaller than or equal to a specific size.
        /// </summary>
        /// <param name="integer">The INT to check.</param>
        /// <param name="maxSize">Maximum size allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        public static void EnsureIntIsSmallerOrEqualTo(int integer, int maxSize, string variableName)
        {
            if (integer <= maxSize)
            {
                string message = $"Integer called '{variableName}' must be below or equal to {maxSize}";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        /// <summary>
        /// Checks if an integer is within a given range (both inclusive).
        /// </summary>
        /// <param name="integer">The INT to check.</param>
        /// <param name="lowBounds">Minimum size allowed.</param>
        /// <param name="highBounds">Maximum size allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        public static void EnsureIntIsInRange(int integer, int lowBounds, int highBounds, string variableName)
        {
            if (integer >= lowBounds && integer <= highBounds)
            {
                string message = $"Integer called '{variableName}' must be in range of {lowBounds} - {highBounds}.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        #endregion

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
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotEmpty<T>(IList<T> list, string variableName)
        {
            if (list.Count <= 0)
            {
                string message = $"The list '{variableName}' cannot be empty.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        /// <summary>
        /// Checks if a given list is not null.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotEmptyOrNull<T>(IList<T> list, string variableName)
        {
            if (list == null || list.Count <= 0)
            {
                string message = $"The list '{variableName}' cannot be empty or null.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        /// <summary>
        /// Checks if a given list has a size lower than a specific size.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="maxAllowedSize">Max allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotLongerThan<T>(IList<T> list, int maxAllowedSize, string variableName)
        {
            if (list.Count > maxAllowedSize)
            {
                string message = $"The list '{variableName}' cannot have more than {maxAllowedSize} elements.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        /// <summary>
        /// Checks if a given list has a size lower or equal to a specific size.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="maxAllowedSize">Max allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotLongerOrEqualThan<T>(IList<T> list, int maxAllowedSize, string variableName)
        {
            if (list.Count >= maxAllowedSize)
            {
                string message = $"The list '{variableName}' cannot have more or equal to {maxAllowedSize} elements.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        /// <summary>
        /// Checks if a given list has a size bigger than a specific value.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="minAllowedSize">Min allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotShorterThan<T>(IList<T> list, int minAllowedSize, string variableName)
        {
            if (list.Count < minAllowedSize)
            {
                string message = $"The list '{variableName}' cannot have less than {minAllowedSize} elements.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        /// <summary>
        /// Checks if a given list has a size bigger or equal to a specific value.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="minAllowedSize">Min allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotShorterOrEqualThan<T>(IList<T> list, int minAllowedSize, string variableName)
        {
            if (list.Count <= minAllowedSize)
            {
                string message = $"The list '{variableName}' cannot have less or equal to {minAllowedSize} elements.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        /// <summary>
        /// Checks if a given list has a specific size.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="size">The allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsLongExactly<T>(IList<T> list, int size, string variableName)
        {
            if (list.Count == size)
            {
                string message = $"The list '{variableName}' must have exactly {size} elements.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        /// <summary>
        /// Checks if List already conatins a given asset.
        /// </summary>
        /// <typeparam name="T">Asset type</typeparam>
        /// <param name="asset">The Asset we check the duplicity for.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListNotContains<T>(IList<T> list, T asset, string variableName)
        {
            if (list.Contains(asset))
            {
                string message = $"The list '{variableName}' already contains '{asset}'.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetCollectionException(message);
            }
        }

        #endregion

    }
}