using System.Collections;
using UnityEngine;

namespace Rogium.Global.UISystem
{
    public interface IMenuSwitcher
    {
        /// <summary>
        /// Switches from the current menu to a new one.
        /// </summary>
        /// <param name="menu">The new menu to switch to.</param>
        public void SwitchTo(MenuType menu);
    }
}