using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.UserInterface.UI
{
    /// <summary>
    /// Contains references for various UI components of the Main Menus.
    /// </summary>
    public class UIMainContainer : MonoSingleton<UIMainContainer>
    {
        [SerializeField] private GameObject backgroundMain;
        [SerializeField] private GameObject backgroundGameplayMenus;

        public GameObject BackgroundMain { get => backgroundMain; }
        public GameObject BackgroundGameplayMenus { get => backgroundGameplayMenus; }
    }
}