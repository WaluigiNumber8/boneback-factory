using System;
using System.Collections;
using UnityEngine;

namespace RogiumLegend.Global.SafetyChecks
{
    public class SafetyNet
    {
        public static event Action<string> OnFireErrorMessage;


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

    }
}