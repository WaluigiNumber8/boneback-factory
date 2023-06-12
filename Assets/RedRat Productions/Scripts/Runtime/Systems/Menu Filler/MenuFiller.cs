using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace RedRats.UI.MenuFilling
{
    /// <summary>
    /// Fills object children with menu parts.
    /// </summary>
    public class MenuFiller<T> where T : MonoBehaviour
    {
        private IList<T> list;
        private T prefab;
        private Transform parent;

        /// <summary>
        /// Updates a menu list by spawning/deactivating it's interactable components.
        /// </summary>
        /// <param name="list">The list to update.</param>
        /// <param name="newCount">The new size the list is going to have.</param>
        /// <param name="prefab">The prefab of the interactable component.</param>
        /// <param name="parent">The menu parent.</param>
        public void Update(IList<T> list, int newCount, T prefab, Transform parent)
        {
            this.list = list;
            this.parent = parent;
            this.prefab = prefab;
            
            //Prebuild objects if none are created.
            Build(list.Count, newCount);
        }

        /// <summary>
        /// Updates the object count based on list length change.
        /// </summary>
        /// <param name="currentLength">The current length of the list.</param>
        /// <param name="newLength">The new length the list is going to have.</param>
        private void Build(int currentLength, int newLength)
        {
            if (newLength == currentLength) return;
                
            //Length is bigger.
            if (newLength > currentLength)
            {
                BuildHolders(currentLength, newLength);
                return;
            }
            
            //Length is smaller.
            if (newLength < currentLength)
            {
                DestroyHolders(currentLength - Mathf.Abs(newLength - currentLength), currentLength);
                return;
            }
            
        }

        /// <summary>
        /// Fills the menu list with objects.
        /// </summary>
        /// <param name="startIndex">The position to start on.</param>
        /// <param name="endIndex">The position to end with.</param>
        private void BuildHolders(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                T holder = Object.Instantiate(prefab, parent);

                if (i < list.Count)
                    list[i] = holder;
                else list.Add(holder);
            }
        }
        
        /// <summary>
        /// Destroys objects in the menu list.
        /// </summary>
        /// <param name="startIndex">The position to start on.</param>
        /// <param name="endIndex">The position to end with.</param>
        private void DestroyHolders(int startIndex, int endIndex)
        {
            for (int i = endIndex - 1; i >= startIndex; i--)
            {
                Object.Destroy(list[i].gameObject);
                list.RemoveAt(i);
            }
        }
    }
}