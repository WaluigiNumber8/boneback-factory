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
        private readonly bool essentialOnly;

        public SelectionInfoColumnPropertyBuilderPack(Transform contentMain, bool essentialOnly) : base(contentMain)
        {
            this.essentialOnly = essentialOnly;
        }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a pack.
        /// </summary>
        /// <param name="asset">The pack to build for.</param>
        public override void Build(PackAsset asset)
        {
            Clear();
            if (essentialOnly) BuildLite(asset);
            else BuildFull(asset);
        }

        private void BuildFull(PackAsset asset)
        {
            b.BuildPlainText("Palettes", asset.Palettes.Count.ToString(), contentMain);
            b.BuildPlainText("Sprites", asset.Sprites.Count.ToString(), contentMain);
            b.BuildPlainText("Weapons", asset.Weapons.Count.ToString(), contentMain);
            b.BuildPlainText("Projectiles", asset.Projectiles.Count.ToString(), contentMain);
            b.BuildPlainText("Enemies", asset.Enemies.Count.ToString(), contentMain);
            b.BuildPlainText("Rooms", asset.Rooms.Count.ToString(), contentMain);
            b.BuildPlainText("Tiles", asset.Tiles.Count.ToString(), contentMain);
        }
        
        private void BuildLite(PackAsset asset)
        {
            b.BuildPlainText("Weapons", asset.Weapons.Count.ToString(), contentMain);
            b.BuildPlainText("Enemies", asset.Enemies.Count.ToString(), contentMain);
            b.BuildPlainText("Rooms", asset.Rooms.Count.ToString(), contentMain);
        }
    }
}