using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Turns a <see cref="ScriptableObject"/> class, that inherits this into a Singleton. <br/>
    /// The class must have the <see cref="ResourcesAssetPathAttribute"/> to know where to load the asset from.
    /// </summary>
    public abstract class ScriptableObjectSingleton<T> : ScriptableObject where T : Object
    {
        private static T instance;
        public static T Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = Resources.Load<T>(GetAssetPath());
                }
                return instance;
            }
        }
        
        private static string GetAssetPath()
        {
            object[] attributes = typeof(T).GetCustomAttributes(true);
            foreach (object attribute in attributes)
            {
                if (attribute is ResourcesAssetPathAttribute pathAttribute) return pathAttribute.AssetPath;
            }
            Debug.LogError($"{typeof(T)} does not have {nameof(ResourcesAssetPathAttribute)}.");
            return string.Empty;
        }
    }
}