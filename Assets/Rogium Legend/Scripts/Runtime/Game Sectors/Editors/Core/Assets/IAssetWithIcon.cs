using UnityEngine;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Represents all assets which can have an icon.
    /// </summary>
    public interface IAssetWithIcon : IAsset
    {
        public void UpdateIcon(IAsset newSprite);
    }
}