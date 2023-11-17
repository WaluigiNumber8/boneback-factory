using System;
using System.Collections.Generic;
using RedRats.Safety;
using UnityEngine;
using UnityEngine.Pool;

namespace RedRats.Core
{
    /// <summary>
    /// An <see cref="ObjectPool{T}"/> whose every item has a specific key.
    /// </summary>
    /// <typeparam name="T">The key of each value. Any type</typeparam>
    /// <typeparam name="TS">The objects stored in the pool.</typeparam>
    public class ObjectDictionaryPool<T,TS> where TS : Component
    {
        private readonly IDictionary<T, TS> items;
        private readonly ObjectPool<TS> pool;
        private readonly Action<TS> onGetAction;
        private readonly Action<TS> onReleaseAction;

        public ObjectDictionaryPool(Func<TS> createFunc, Action<TS> actionOnGet, Action<TS> actionOnRelease,
            Action<TS> actionOnDestroy, bool collectionCheck = true, int defaultCapacity = 10, int maxSize = 100)
        {
            items = new Dictionary<T, TS>();
            pool = new ObjectPool<TS>(createFunc, actionOnGet, actionOnRelease, actionOnDestroy, collectionCheck, defaultCapacity, maxSize);
            onGetAction = actionOnGet;
            onReleaseAction = actionOnRelease;
        }
        
        /// <summary>
        /// Get an item from the pool with a specific key. If the item doesn't exist, it will be created.
        /// </summary>
        /// <param name="key">The key of item.</param>
        /// <returns>The found/new item.</returns>
        public TS Get(T key)
        {
            if (items.TryGetValue(key, out TS value))
            {
                onGetAction(value);
                return value;
            }
            TS item = pool.Get();
            items.Add(key, item);
            return item;
        }

        /// <summary>
        /// Get a random item from the pool.
        /// </summary>
        /// <returns>The item itself.</returns>
        public TS Get() => pool.Get();
        
        /// <summary>
        /// Release the given item from the pool.
        /// </summary>
        /// <param name="item">The item to release.</param>
        public void Release(TS item) => pool.Release(item);

        /// <summary>
        /// Releases the item with the given key from the pool.
        /// </summary>
        /// <param name="key">The key under which the item is stored.</param>
        public void Release(T key)
        {
            if (!items.ContainsKey(key)) return;
            onReleaseAction(items[key]);
        }
        
        /// <summary>
        /// How many items are in the pool.
        /// </summary>
        public int Count => pool.CountAll;
    }
}