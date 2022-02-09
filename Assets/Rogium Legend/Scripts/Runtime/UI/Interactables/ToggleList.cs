using System.Collections;
using System.Collections.Generic;
using BoubakProductions.Safety;
using Rogium.UserInterface.AssetSelection;

namespace Rogium.UserInterface.Core
{
    /// <summary>
    /// Controls toggle states on an array of toggles. 
    /// </summary>
    public class ToggleList : IList<ToggleableBase>
    {
        private readonly IList<ToggleableBase> toggles;

        public ToggleList()
        {
            toggles = new List<ToggleableBase>();
        }

        /// <summary>
        /// Sets all elements on the list to the same value.
        /// </summary>
        /// <param name="value">The state of toggles.</param>
        public void ToggleAll(bool value)
        {
            foreach (ToggleableBase toggle in toggles)
            {
                toggle.SetToggle(value);
            }
        }
        
        /// <summary>
        /// Toggle the element on a specific position to a value and everything else to the opposite value.
        /// </summary>
        /// <param name="index">The position of the element.</param>
        /// <param name="value">The value to set it to.</param>
        public void ToggleExclusive(int index, bool value)
        {
            SafetyNet.EnsureIntIsInRange(index, 0, toggles.Count, "Toggle list");
            for (int i = 0; i < toggles.Count; i++)
            {
                if (i == index)
                {
                    toggles[i].SetToggle(value);
                    continue;
                }
                toggles[i].SetToggle(!value);
            }
        }

        #region Untouched Overrides
        public IEnumerator<ToggleableBase> GetEnumerator()
        {
            return toggles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return toggles.GetEnumerator();
        }

        public void Add(ToggleableBase item)
        {
            toggles.Add(item);
        }

        public void Clear()
        {
            toggles.Clear();
        }

        public bool Contains(ToggleableBase item)
        {
            return toggles.Contains(item); 
        }

        public void CopyTo(ToggleableBase[] array, int arrayIndex)
        {
            toggles.CopyTo(array, arrayIndex);
        }

        public bool Remove(ToggleableBase item)
        {
            return toggles.Remove(item);
        }

        public int Count { get => toggles.Count; }
        public bool IsReadOnly { get => toggles.IsReadOnly; }
        public int IndexOf(ToggleableBase item)
        {
            return toggles.IndexOf(item);
        }

        public void Insert(int index, ToggleableBase item)
        {
            toggles.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            toggles.RemoveAt(index);
        }

        public ToggleableBase this[int index]
        {
            get => toggles[index];
            set => toggles[index] = value;
        }
        #endregion
    }
}
