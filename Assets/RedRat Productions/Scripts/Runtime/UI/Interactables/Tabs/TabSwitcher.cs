using RedRats.Systems.ObjectSwitching;
using UnityEngine;

namespace RedRats.UI.Tabs
{
    /// <summary>
    /// An Addon for <see cref="TabGroupBase"/> that uses the <see cref="ObjectSwitcher"/>.
    /// </summary>
    [RequireComponent(typeof(TabGroupBase))]
    public class TabSwitcher : MonoBehaviour
    {
        private TabGroupBase tabGroup;
        private ObjectSwitcher switcher;

        private void Start()
        {
            tabGroup = GetComponent<TabGroupBase>();
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