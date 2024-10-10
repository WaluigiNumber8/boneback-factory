using UnityEngine;

namespace RedRats.UI.Tabs
{
    /// <summary>
    /// A new type of UI Layout group, the Tab, allows for switching on and off different screens.
    /// </summary>
    public class TabGroup : TabGroupBase
    {
        [SerializeField] private TabPageButton[] buttons;
        
        protected override TabPageButton[] GetButtons() => buttons;
    }
}

