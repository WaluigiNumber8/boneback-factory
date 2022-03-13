using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Extension methods for the <see cref="Component"/> type.
    /// </summary>
    public static class ComponentExtensions
    {
        /// <summary>
        /// Tries to get a component from objects children.
        /// </summary>
        /// <param name="obj">The object to search the component in.</param>
        /// <param name="component">The component to search for.</param>
        /// <typeparam name="T">Any type.</typeparam>
        /// <returns>Returns TRUE, if a component was found.</returns>
        public static bool TryGetComponentInChildren<T>(this Component obj, out T component)
        {
            component = obj.GetComponentInChildren<T>();
            return (component != null);
        }
    }
}