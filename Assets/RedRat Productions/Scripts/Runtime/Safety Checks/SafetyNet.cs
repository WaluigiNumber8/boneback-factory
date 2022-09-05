using RedRats.Core;
using System;
using System.Collections.Generic;

namespace RedRats.Safety
{
    /// <summary>
    /// Contains various method for checking correctness of given parameters. If a method fails to pass, it throws an exception.
    /// </summary>
    public static class SafetyNet
    {
        public static event Action<string> OnFireErrorMessage;

        #region Object Checks

        /// <summary>
        /// Checks if a given object is not null.
        /// </summary>
        /// <param name="obj">The object to check.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureIsNotNull(object obj, string variableName, string customMessage = "")
        {
            if (obj == null)
                ThrowException(s => new SafetyNetException(s), customMessage, $"'{variableName}' cannot be null.");
        }

        #endregion

        #region Int Checks

        /// <summary>
        /// Checks if an number is not equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="allowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureIntIsEqual(int number, int allowedValue, string variableName, string customMessage = "")
        {
            if (number != allowedValue)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must only equal to {allowedValue.ToString()}. ({number.ToString()})");
        }
        
        /// <summary>
        /// Checks if an number is not equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="disallowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureIntIsNotEqual(int number, int disallowedValue, string variableName, string customMessage = "")
        {
            if (number == disallowedValue)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must not equal to {disallowedValue.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is bigger than a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureIntIsBiggerThan(int number, int minSize, string variableName, string customMessage = "")
        {
            if (number <= minSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be above {minSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is bigger than or equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureIntIsBiggerOrEqualTo(int number, int minSize, string variableName, string customMessage = "")
        {
            if (number < minSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be above or equal to {minSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is smaller than a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureIntIsSmallerThan(int number, int maxSize, string variableName, string customMessage = "")
        {
            if (number >= maxSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be below {maxSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is smaller than or equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureIntIsSmallerOrEqualTo(int number, int maxSize, string variableName, string customMessage = "")
        {
            if (number > maxSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be below or equal to {maxSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is within a given range (both inclusive).
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="lowBounds">Minimum value allowed.</param>
        /// <param name="highBounds">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureIntIsInRange(int number, int lowBounds, int highBounds, string variableName, string customMessage = "")
        {
            if (number < lowBounds && number > highBounds)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be in range of {lowBounds.ToString()} - {highBounds.ToString()}. ({number.ToString()})");
        }

        #endregion

        #region Float Checks

        /// <summary>
        /// Checks if a number is not equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="allowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureFloatIsEqual(float number, float allowedValue, string variableName, string customMessage = "")
        {
            if (Math.Abs(number - allowedValue) > 0.01f)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must only equal to {allowedValue.ToString()}. ({number.ToString()})");
        }
        
        /// <summary>
        /// Checks if a number is not equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="disallowedValue">The value it cannot equal to.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureFloatIsNotEqual(float number, float disallowedValue, string variableName, string customMessage = "")
        {
            if (Math.Abs(number - disallowedValue) < 0.01f)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must not equal to {disallowedValue.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is bigger than a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureFloatIsBiggerThan(float number, float minSize, string variableName, string customMessage = "")
        {
            if (number <= minSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be above {minSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is bigger than or equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="minSize">Minimum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureFloatIsBiggerOrEqualTo(float number, float minSize, string variableName, string customMessage = "")
        {
            if (number < minSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be above or equal to {minSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is smaller than a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureFloatIsSmallerThan(float number, float maxSize, string variableName, string customMessage = "")
        {
            if (number >= maxSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be below {maxSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is smaller than or equal to a specific value.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="maxSize">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureFloatIsSmallerOrEqualTo(float number, float maxSize, string variableName, string customMessage = "")
        {
            if (number > maxSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be below or equal to {maxSize.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is within a given range (both inclusive).
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="lowBounds">Minimum value allowed.</param>
        /// <param name="highBounds">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureValueIsInRange(float number, float lowBounds, float highBounds, string variableName, string customMessage = "")
        {
            if (number < lowBounds && number > highBounds)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be in range of {lowBounds.ToString()} - {highBounds.ToString()}. ({number.ToString()})");
        }

        /// <summary>
        /// Checks if an number is within a given range (both inclusive).
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <param name="lowBounds">Minimum value allowed.</param>
        /// <param name="highBounds">Maximum value allowed.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureFloatIsInRange(float number, float lowBounds, float highBounds, string variableName, string customMessage = "")
        {
            if (number < lowBounds && number > highBounds)
                ThrowException(s => new SafetyNetException(s), customMessage, $"Number called '{variableName}' must be in range of {lowBounds.ToString()} - {highBounds.ToString()}. ({number.ToString()})");
        }
        #endregion
        
        #region String Checks
        /// <summary>
        /// Checks if a given string is not null or empty.
        /// </summary>
        /// <param name="stringObject">The string to check.</param>
        /// <param name="variableName">Name of the variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureStringNotNullOrEmpty(string stringObject, string variableName, string customMessage = "")
        {
            if (string.IsNullOrEmpty(stringObject))
                ThrowException(s => new SafetyNetException(s), customMessage, $"The string '{variableName}' cannot be null nor empty.");
        }
        
        /// <summary>
        /// Makes sure that a string is longer than minLimit.
        /// </summary>
        /// <param name="stringObject">The string to check.</param>
        /// <param name="minLimit">Minimum characters allowed for the string.</param>
        /// <param name="variableName">Description of wronged variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureStringMinLimit(string stringObject, int minLimit, string variableName, string customMessage = "")
        {
            if (stringObject.Length < minLimit)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The string '{variableName}' with value '{stringObject}' cannot have less than or equal to {minLimit.ToString()} characters! ({stringObject.Length.ToString()})");
        }

        /// <summary>
        /// Makes sure that a string is shorter than maxLimit.
        /// </summary>
        /// <param name="stringObject">The string to check.</param>
        /// <param name="maxLimit">Maximum characters allowed for the string.</param>
        /// <param name="variableName">Description of wronged variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureStringMaxLimit(string stringObject, int maxLimit, string variableName, string customMessage = "")
        {
            if (stringObject.Length > maxLimit)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The string '{variableName}' with value '{stringObject}' cannot have more than or equal to {maxLimit.ToString()} characters! ({stringObject.Length.ToString()})");
        }

        /// <summary>
        /// Checks if a string's amount of characters is within a given range.
        /// </summary>
        /// <param name="stringObject">The string to check.</param>
        /// <param name="minLimit">Range minimum.</param>
        /// <param name="maxLimit">Range maximum.</param>
        /// <param name="variableName">Name of the wronged variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetException"></exception>
        public static void EnsureStringInRange(string stringObject, int minLimit, int maxLimit, string variableName, string customMessage = "")
        {
            EnsureStringMinLimit(stringObject, minLimit, variableName, customMessage);
            EnsureStringMaxLimit(stringObject, maxLimit, variableName, customMessage);
        }
        #endregion

        #region List Checks

        /// <summary>
        /// Checks if a given list is not empty.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotEmpty<T>(IList<T> list, string variableName, string customMessage = "")
        {
            if (list.Count <= 0)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' cannot be empty.");
        }

        /// <summary>
        /// Checks if a given list is not null.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotNullOrEmpty<T>(IList<T> list, string variableName, string customMessage = "")
        {
            if (list == null || list.Count <= 0)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' cannot be empty or null.");
        }

        /// <summary>
        /// Checks if a given list has a size lower than a specific size.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="maxAllowedSize">Max allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotLongerThan<T>(IList<T> list, int maxAllowedSize, string variableName, string customMessage = "")
        {
            if (list.Count > maxAllowedSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' cannot have more than {maxAllowedSize.ToString()} elements. ({list.Count.ToString()})");
        }

        /// <summary>
        /// Checks if a given list has a size lower or equal to a specific size.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="maxAllowedSize">Max allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotLongerOrEqualThan<T>(IList<T> list, int maxAllowedSize, string variableName, string customMessage = "")
        {
            if (list.Count >= maxAllowedSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' cannot have more or equal to {maxAllowedSize.ToString()} elements. ({list.Count.ToString()})");
        }

        /// <summary>
        /// Checks if a given list has a size bigger than a specific value.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="minAllowedSize">Min allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotShorterThan<T>(IList<T> list, int minAllowedSize, string variableName, string customMessage = "")
        {
            if (list.Count < minAllowedSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' cannot have less than {minAllowedSize.ToString()} elements. ({list.Count.ToString()})");
        }

        /// <summary>
        /// Checks if a given list has a size bigger or equal to a specific value.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="minAllowedSize">Min allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsNotShorterOrEqualThan<T>(IList<T> list, int minAllowedSize, string variableName, string customMessage = "")
        {
            if (list.Count <= minAllowedSize)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' cannot have less or equal to {minAllowedSize.ToString()} elements. ({list.Count.ToString()})");
        }

        /// <summary>
        /// Checks if a given list has a specific size.
        /// </summary>
        /// <param name="list">The list to check.</param>
        /// <param name="size">The allowed size for the list.</param>
        /// <param name="variableName">The name of the list</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListIsLongExactly<T>(IList<T> list, int size, string variableName, string customMessage = "")
        {
            if (list.Count == size)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' must have exactly {size.ToString()} elements. ({list.Count.ToString()})");
        }

        /// <summary>
        /// Ensures a list does not contain a specific object.
        /// </summary>
        /// <typeparam name="T">Any object type.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="value">The object we check the duplicity for.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListNotContains<T>(IList<T> list, T value, string variableName, string customMessage = "")
        {
            if (list.Contains(value))
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' already contains '{value.ToString()}'.");
        }
        
        /// <summary>
        /// Ensures that list contains a specific object.
        /// </summary>
        /// <typeparam name="T">Any object type.</typeparam>
        /// <param name="list">The list to check.</param>
        /// <param name="value">The object we check the duplicity for.</param>
        /// <param name="variableName">Name of the checked variable.</param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        /// <exception cref="SafetyNetCollectionException"></exception>
        public static void EnsureListContains<T>(IList<T> list, T value, string variableName, string customMessage = "") where T : class
        {
            if (!list.ContainsValue(value))
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' does not contain '{value}'.");
        }

        /// <summary>
        /// Checks if a given list does not have any duplicates.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="variableName"></param>
        /// <param name="customMessage">The message of the error. If blank will use default.</param>
        public static void EnsureListDoesNotHaveDuplicities<T>(IList<T> list, string variableName, string customMessage = "")
        {
            int foundDuplicates = list.GetDuplicatesCount();
            if (foundDuplicates > 0)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The list '{variableName}' cannot have any duplicit values.");
        }

        public static void EnsureIndexWithingCollectionRange<T>(int index, ICollection<T> collection, string collectionName, string customMessage = "")
        {
            if (index < 0 || index > collection.Count - 1)
                ThrowException(s => new SafetyNetException(s), customMessage, $"The index doesn't fit on the '{collectionName}' collection. Length is {collection.Count.ToString()}. ({index.ToString()})");
        }
        #endregion

        /// <summary>
        /// Throw a custom error message, without raising an exception.
        /// </summary>
        /// <param name="message">The message to throw.</param>
        public static void ThrowMessage(string message) => OnFireErrorMessage?.Invoke($"Safety Net - {message}");

        /// <summary>
        /// Throws an exception with appropriate events.
        /// </summary>
        /// <param name="exception">The type of the thrown exception</param>
        /// <param name="customMessage"></param>
        /// <param name="defaultMessage"></param>
        /// <exception cref="Exception"></exception>
        private static void ThrowException<T>(Func<string, T> exception, string customMessage, string defaultMessage) where T : Exception
        {
            string message = (customMessage != "") ? customMessage : defaultMessage;
            OnFireErrorMessage?.Invoke(message);
            throw exception(message);
        }
    }
}