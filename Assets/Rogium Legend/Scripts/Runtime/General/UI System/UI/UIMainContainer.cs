using BoubakProductions.Core;
using UnityEngine;

namespace Rogium.Global.UISystem.UI
{
    /// <summary>
    /// Contains references for various UI components of the Main Menus.
    /// </summary>
    public class UIMainContainer : MonoSingleton<UIMainContainer>
    {
        [SerializeField] private GameObject backgroundMain;

        public GameObject BackgroundMain { get => backgroundMain; }
    }
}