using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace RedRats.UI.Core.Cursors
{
    [Serializable]
    public struct CursorInfo
    {
        [HorizontalGroup("Base", 72, LabelWidth = 64, MarginRight = 24), HideLabel, PreviewField(64)]
        public Texture2D texture;
        [VerticalGroup("Base/Right")] public CursorType type;
        [VerticalGroup("Base/Right")] public Vector2 hotspot;

        /// <summary>
        /// Use this cursor.
        /// </summary>
        public void Use() => Cursor.SetCursor(texture, hotspot, CursorMode.Auto);
    }
}