using RogiumLegend.Core;
using RogiumLegend.Editors.Core;
using UnityEngine.UI;

namespace RogiumLegend.Global.UISystem.UI
{
    public interface IAssetHolder
    {
        int ID { get; }

        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="id">This asset's position in the list.</param>
        /// <param name="asset">The Asset itself.</param>
        void Construct(AssetType type, int id, IAsset asset);
        /// <summary>
        /// Should be called after creating an Asset Card. Constructs basic variables.
        /// </summary>
        /// <param name="type">The type of asset this is.</param>
        /// <param name="id">This asset's position in the list.</param>
        /// <param name="asset">The Asset itself.</param>
        /// <param name="iconPosition">Image, on which the icon will be drawn.</param>
        void Construct(AssetType type, int id, IAsset asset, Image iconPos);
    }
}