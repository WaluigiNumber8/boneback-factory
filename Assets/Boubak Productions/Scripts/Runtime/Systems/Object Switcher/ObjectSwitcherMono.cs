using UnityEngine;

namespace BoubakProductions.Systems.ObjectSwitching
{
    /// <summary>
    /// Switches between a given group of objects.
    /// </summary>
    public class ObjectSwitcherMono : MonoBehaviour
    {
        private ObjectSwitcher switcher;
        
        [SerializeField] private int defaultIndex;
        [SerializeField] private GameObject[] objects;

        private void Awake() => switcher = new ObjectSwitcher(objects, defaultIndex);

        /// <summary>
        /// Switches GameObjects.
        /// </summary>
        /// <param name="index">The index of the object to turn on.</param>
        public void Switch(int index) => switcher.Switch(index);
        /// <summary>
        /// Switches GameObjects.
        /// </summary>
        /// <param name="objectToActivate">The object to turn on.</param>
        public void Switch(GameObject objectToActivate) => switcher.Switch(objectToActivate);
        
    }
}