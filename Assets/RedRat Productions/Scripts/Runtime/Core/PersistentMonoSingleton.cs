using UnityEngine;

namespace RedRats.Core
{
    /// <summary>
    /// Turns a <see cref="MonoBehaviour"/> that inherits this into a singleton that persists between scenes. 
    /// </summary>
    /// <typeparam name="T">Any Component type.</typeparam>
    public abstract class PersistentMonoSingleton<T> : MonoSingleton<T> where T : Component
    {
        public new static T Instance
        {
            get
            {
                T instance = MonoSingleton<T>.Instance;
                DontDestroyOnLoad(instance.gameObject);
                return instance;
            }
        }
    }
}