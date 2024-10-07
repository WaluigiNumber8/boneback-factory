using RedRats.Systems.ObjectSwitching;
using UnityEngine;

namespace RedRats.UI.Tabs
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
            switcher = new ObjectSwitcher(tabGroup.DefaultTabIndex, tabGroup.GetButtonsAsArray());
            tabGroup.onTabSwitch += Switch;
        }

        private async void Switch(GameObject obj)
        {
            await Awaitable.EndOfFrameAsync();
            switcher.Switch(obj);
        }
    }
}