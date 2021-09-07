using BoubakProductions.Safety;
using UnityEngine;

namespace BoubakProductions.Core
{
    /// <summary>
    /// Switches between a given group of objects.
    /// </summary>
    public class ObjectSwitcher : MonoBehaviour
    {
        [SerializeField] private int defaultIndex = -1;
        [SerializeField] private GameObject[] objects;

        private void Start()
        {
            DeselectAllExcept(defaultIndex);
        }

        /// <summary>
        /// Deselects all objects except one.
        /// <param name="activatedObject">The object to activate.</param>
        /// </summary>
        public void DeselectAllExcept(GameObject activatedObject)
        {
            for (int i = 0; i < objects.Length; i++)
            {
                if (activatedObject == objects[i])
                {
                    DeselectAllExcept(i);
                    return;
                }
            }
            throw new System.InvalidOperationException($"'{activatedObject.name}' was not found in this array.");
        }
        /// <summary>
        /// Deselects all objects except one.
        /// <param name="index">The index of the object to activate.</param>
        /// </summary>
        public void DeselectAllExcept(int index)
        {
            if (index == -1) return;
            SafetyNet.EnsureIntIsInRange(defaultIndex, 0, objects.Length, "Default Tab Index");
            
            foreach (GameObject gObject in objects)
            {
                gObject.SetActive(false);
            }
            objects[index].SetActive(true);
        }


    }
}