using BoubakProductions.UI.Core;
using UnityEngine;

namespace Rogium.Systems.ThemeSystem
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