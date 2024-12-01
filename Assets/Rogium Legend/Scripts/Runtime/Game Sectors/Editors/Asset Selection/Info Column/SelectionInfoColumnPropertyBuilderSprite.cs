using Rogium.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Sprites;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.Editors.AssetSelection
{
    /// <summary>
    /// Builds the <see cref="SelectionInfoColumn"/> for a <see cref="Sprite"/>.
    /// </summary>
    public class SelectionInfoColumnPropertyBuilderSprite : UIPropertyContentBuilderBaseColumn1<SpriteAsset>
    {
        public SelectionInfoColumnPropertyBuilderSprite(Transform contentMain) : base(contentMain) { }

        /// <summary>
        /// Build <see cref="SelectionInfoColumn"/> properties for a sprite.
        /// </summary>
        /// <param name="asset">The sprite to build for.</param>
        public override void Build(SpriteAsset asset)
        {
            Clear();
            b.BuildAssetEmblemList("Palette", asset.AssociatedPaletteID.TryGetAsset(PackEditorOverseer.Instance.CurrentPack.Palettes).Icon, contentMain);
        }
    }
}