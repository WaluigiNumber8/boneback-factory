using BoubakProductions.Safety;
using UnityEngine;

namespace BoubakProductions.ObjectSwitching
{
    /// <summary>
    /// Switches between a given group of objects.
    /// </summary>
    public class ObjectSwitcherMono : MonoBehaviour
    {
        private ObjectSwitcher switcher;
        
        [SerializeField] private int defaultIndex;
        [SerializeField] private GameObject[] objects;

        private void Awake()
        {
            switcher = new ObjectSwitcher(objects, defaultIndex);
        }

        public ObjectSwitcher Switcher { get => switcher; }

    }
}