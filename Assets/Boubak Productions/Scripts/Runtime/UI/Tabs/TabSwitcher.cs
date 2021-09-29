using BoubakProductions.ObjectSwitching;
using UnityEngine;

namespace BoubakProductions.UI
{
    /// <summary>
    /// An Addon for <see cref="TabGroup"/> that uses the <see cref="ObjectSwitcher"/>.
    /// </summary>
    [RequireComponent(typeof(TabGroup))]
    public class TabSwitcher : MonoBehaviour
    {
        private TabGroup tabGroup;
        private ObjectSwitcher switcher;

        private void Start()
        {
            tabGroup = GetComponent<TabGroup>();
            switcher = new ObjectSwitcher(tabGroup.GetButtonsAsArray());
            tabGroup.onTabSwitch += switcher.DeselectAllExcept;
        }
    }
}