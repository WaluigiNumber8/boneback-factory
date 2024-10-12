using Rogium.Editors.Packs;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.NewAssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="PackAsset"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderPack : UIPropertyContentBuilderBaseColumn1<PackAsset>
    {
        public SelectionInfoColumnPropertyBuilderPack(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a pack.
        /// </summary>
        /// <param name="asset">The pack to build for.</param>
        public override void Build(PackAsset asset)
        {
            Clear();
            b.BuildPlainText("Palettes", asset.Palettes.Count.ToString(), contentMain);
        }
    }
}