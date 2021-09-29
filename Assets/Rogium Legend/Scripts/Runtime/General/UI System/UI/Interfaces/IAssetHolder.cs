using Rogium.Core;
using Rogium.Editors.Core;
using UnityEngine.UI;

namespace Rogium.Global.UISystem.UI
{
    public interface IAssetHolder
    {
        int ID { get; }

        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="id">This asset's position in the list.</param>
        /// <param name="assetBase">The Asset itself.</param>
        void Construct(AssetType type, int id, AssetBase assetBase);
        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="id">This asset's position in the list.</param>
        /// <param name="assetBase">The Asset itself.</param>
        /// <param name="iconPosition">Image, on which the icon will be drawn.</param>
        void Construct(AssetType type, int id, AssetBase assetBase, Image iconPos);
    }
}