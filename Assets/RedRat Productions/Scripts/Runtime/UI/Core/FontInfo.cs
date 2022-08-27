using Sirenix.OdinInspector;
using UnityEngine;
using TMPro;

namespace RedRats.UI.Core
{
    /// <summary>
    /// Contains all necessary data for a font.
    /// </summary>
    [System.Serializable]
    public struct FontInfo
    {
        [HideLabel]
        public TMP_FontAsset font;
        [HorizontalGroup(Width = 0.25f, MarginRight = 4),HideLabel]
        public int size;
        [HorizontalGroup, HideLabel]
        public Color color;
    }
}