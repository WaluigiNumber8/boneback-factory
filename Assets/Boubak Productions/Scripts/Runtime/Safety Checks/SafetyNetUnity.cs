using System;
using UnityEngine;

namespace BoubakProductions.Safety
{
    /// <summary>
    /// Contains various method for checking correctness of given parameters using Unity's Architecture.
    /// If a method fails to pass, it throws an exception.
    /// </summary>
    public static class SafetyNetUnity
    {
        public static event Action<string> OnFireErrorMessage;

        public static void EnsureGameObjectHasComponent(GameObject gameObject, System.Type type, string variableName)
        {
            if (gameObject.GetComponent(type) == null)
            {
                string message = $"GameObject called '{variableName}' must have the component of type {type}.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }

        public static void EnsureGameObjectHasComponentInChildren(GameObject gameObject, System.Type type, string variableName)
        {
            if (gameObject.GetComponentInChildren(type) == null)
            {
                string message = $"GameObject called '{variableName}' must have the component of type {type} in it's children.";
                OnFireErrorMessage?.Invoke(message);
                throw new SafetyNetException(message);
            }
        }
    }
}