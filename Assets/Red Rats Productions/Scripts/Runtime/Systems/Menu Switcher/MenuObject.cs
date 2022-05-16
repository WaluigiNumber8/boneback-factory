using UnityEngine;

namespace RedRats.UI.MenuSwitching
{
    /// <summary>
    /// A Menu identifier. Should be placed on parent objects of all menus. Sends data to CanvasOverseer.
    /// </summary>
    public class MenuObject : MonoBehaviour
    {
        [SerializeField] private MenuType menuType;

        public MenuType MenuType { get => menuType; set => menuType = value; }
    }
}