using System.Collections;
using System.Collections.Generic;

namespace Rogium.UserInterface.Core
{
    /// <summary>
    /// Controls toggle states on an array of toggles. 
    /// </summary>
    public class ToggleList : IList<IToggleable>
    {
        private readonly IList<IToggleable> toggles;

        public ToggleList()
        {
            toggles = new List<IToggleable>();
        }

        /// <summary>
        /// Sets all elements on the list to the same value.
        /// </summary>
        /// <param name="value">The state of toggles.</param>
        public void ToggleAll(bool value)
        {
            foreach (IToggleable toggle in toggles)
            {
                toggle.ChangeToggleState(value);
            }
        }

        #region Untouched Overrides
        public IEnumerator<IToggleable> GetEnumerator()
        {
            return toggles.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return toggles.GetEnumerator();
        }

        public void Add(IToggleable item)
        {
            toggles.Add(item);
        }

        public void Clear()
        {
            toggles.Clear();
        }

        public bool Contains(IToggleable item)
        {
            return toggles.Contains(item); 
        }

        public void CopyTo(IToggleable[] array, int arrayIndex)
        {
            toggles.CopyTo(array, arrayIndex);
        }

        public bool Remove(IToggleable item)
        {
            return toggles.Remove(item);
        }

        public int Count { get => toggles.Count; }
        public bool IsReadOnly { get => toggles.IsReadOnly; }
        public int IndexOf(IToggleable item)
        {
            return toggles.IndexOf(item);
        }

        public void Insert(int index, IToggleable item)
        {
            toggles.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            toggles.RemoveAt(index);
        }

        public IToggleable this[int index]
        {
            get => toggles[index];
            set => toggles[index] = value;
        }
        #endregion
    }
}
