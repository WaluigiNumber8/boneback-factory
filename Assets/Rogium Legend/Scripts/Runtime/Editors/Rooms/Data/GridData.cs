using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Systems.GridSystem;
using Rogium.Systems.ItemPalette;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Contains data needed for grid drawing operations.
    /// </summary>
    public class GridData<T>
    {
        private readonly ObjectGrid<T> grid;
        private readonly ItemPaletteAsset palette;
        private readonly AssetType type;
        private AssetData paintValue;
        private Sprite paintSprite;

        public GridData(ObjectGrid<T> grid, ItemPaletteAsset palette, AssetType type, ParameterInfo defaultParams)
        {
            this.grid = grid;
            this.palette = palette;
            this.type = type;

            this.palette.OnSelect += (asset => UpdateUsedPaint(new AssetData(asset.ID, defaultParams), asset.Icon));
        }

        private void UpdateUsedPaint(AssetData newPaintValue, Sprite newPaintSprite)
        {
            paintValue = newPaintValue;
            paintSprite = newPaintSprite;
        }

        public ObjectGrid<T> Grid { get => grid; }
        public ItemPaletteAsset Palette { get => palette; }
        public AssetType Type { get => type; }
        public AssetData BrushValue { get => paintValue; }
        public Sprite BrushSprite { get => paintSprite; }
    }
}