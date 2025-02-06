using Rogium.Editors.Packs;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.AssetSelection
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
        public override void BuildInternal(PackAsset asset)
        {
            if (essentialOnly) BuildLite(asset);
            else BuildFull(asset);
        }

        private void BuildFull(PackAsset asset)
        {
            b.BuildPlainText("Palettes", asset.PaletteCount.ToString(), contentMain);
            b.BuildPlainText("Sprites", asset.SpriteCount.ToString(), contentMain);
            b.BuildPlainText("Weapons", asset.WeaponCount.ToString(), contentMain);
            b.BuildPlainText("Projectiles", asset.ProjectileCount.ToString(), contentMain);
            b.BuildPlainText("Enemies", asset.EnemyCount.ToString(), contentMain);
            b.BuildPlainText("Rooms", asset.RoomCount.ToString(), contentMain);
            b.BuildPlainText("Tiles", asset.TileCount.ToString(), contentMain);
        }
        
        private void BuildLite(PackAsset asset)
        {
            b.BuildPlainText("Weapons", asset.WeaponCount.ToString(), contentMain);
            b.BuildPlainText("Enemies", asset.EnemyCount.ToString(), contentMain);
            b.BuildPlainText("Rooms", asset.RoomCount.ToString(), contentMain);
        }
    }
}