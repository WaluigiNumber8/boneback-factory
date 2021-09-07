using RogiumLegend.Core;
using RogiumLegend.Editors;
using UnityEngine;

namespace RogiumLegend.Global.MenuSystem.UI
{
    public interface IAssetHolder
    {
        int ID { get; }

        /// <summary>
        /// Construct this asset holder object with correct data.
        /// </summary>
        void Construct(AssetType type, int id, IAsset asset);
    }
}