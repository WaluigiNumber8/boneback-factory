using BoubakProductions.UI.Helpers;
using UnityEngine;

namespace Rogium.Global.ThemeSystem
{
    /// <summary>
    /// Contains information that can be changed for different themes.
    /// </summary>
    [System.Serializable]
    public struct ThemeStyleInfo
    {
        public string themeName;
        public Sprite[] elements;
        public InteractableInfo[] interactables;
        public FontInfo[] fonts;
    }
}