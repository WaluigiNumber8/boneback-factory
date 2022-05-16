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
        public TMP_FontAsset font;
        public Color color;
        public int size;
    }
}