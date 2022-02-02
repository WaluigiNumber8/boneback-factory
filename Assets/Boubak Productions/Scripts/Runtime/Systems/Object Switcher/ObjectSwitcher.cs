using BoubakProductions.Safety;
using UnityEngine;

namespace BoubakProductions.Systems.ObjectSwitching
{
    /// <summary>
    /// Switches between a given group of objects.
    /// </summary>
    public class ObjectSwitcher
    {
        private readonly GameObject[] objects;

        #region Constructors
        public ObjectSwitcher(GameObject[] objects)
        {
            this.objects = objects;
            Switch(0);
        }
        public ObjectSwitcher(GameObject[] objects, int defaultIndex)
        {
            this.objects = objects;
            Switch(defaultIndex);
        }
        
        #endregion

        /// <summary>
        /// Deselects all objects except one.
        /// <param name="activatedObject">The object to activate.</param>
        /// </summary>
        public void Switch(GameObject activatedObject)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (activatedObject != objects[i]) continue;
                
                Switch(i);
                return;
            }
            throw new System.InvalidOperationException($"'{activatedObject.name}' was not found in this array.");
        }
        /// <summary>
        /// Deselects all objects except one.
        /// <param name="index">The index of the object to activate.</param>
        /// </summary>
        public void Switch(int index)
        {
            if (index == -1) return;
            SafetyNet.EnsureIntIsInRange(index, 0, objects.Length, "Default Tab Index");
            
            foreach (GameObject gObject in objects)
            {
                gObject.SetActive(false);
            }
            objects[index].SetActive(true);
        }
    }
}