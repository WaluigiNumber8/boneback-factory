using Rogium.Editors.Core.Defaults;
using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Represents an empty asset.
    /// </summary>
    public class EmptyAsset : IAsset
    {
        private readonly Sprite icon;
        
        public EmptyAsset()
        {
            Texture2D tex = new(1, 1);
            tex.SetPixel(0, 0, new Color(0, 0, 0, 0));
            tex.Apply();
            icon = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        
        public string ID { get => EditorConstants.EmptyAssetID; }
        public string Title { get => ""; }
        public Sprite Icon { get => icon; }
        public string Author { get => ""; }
    }
}