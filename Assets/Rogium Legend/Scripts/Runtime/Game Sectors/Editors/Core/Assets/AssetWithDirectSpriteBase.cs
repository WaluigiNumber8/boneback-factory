using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// A base for all assets containing a unique sprite/icon.
    /// </summary>
    public abstract class AssetWithDirectSpriteBase : AssetBase
    {
        public void UpdateIcon(IAsset asset) => UpdateIcon(asset.Icon);
        public virtual void UpdateIcon(Sprite newIcon) => icon = newIcon;
    }
}