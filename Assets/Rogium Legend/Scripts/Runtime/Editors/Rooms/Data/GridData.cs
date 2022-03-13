using Rogium.Systems.GridSystem;
using Rogium.Systems.ItemPalette;
using UnityEngine;

namespace Rogium.Editors.Rooms
{
    /// <summary>
    /// Contains data needed for grid drawing operations.
    /// </summary>
    public class GridData
    {
        private readonly ObjectGrid<string> grid;
        private readonly ItemPaletteAsset palette;
        private string paintValue;
        private Sprite paintSprite;

        public GridData(ObjectGrid<string> grid, ItemPaletteAsset palette)
        {
            this.grid = grid;
            this.palette = palette;

            this.palette.OnSelect += (slot => UpdateUsedPaint(slot.ID, slot.Asset.Icon));
        }

        private void UpdateUsedPaint(string newPaintValue, Sprite newPaintSprite)
        {
            paintValue = newPaintValue;
            paintSprite = newPaintSprite;
        }

        public ObjectGrid<string> Grid { get => grid; }
        public ItemPaletteAsset Palette { get => palette; }
        public string BrushValue { get => paintValue; }
        public Sprite BrushSprite { get => paintSprite; }
    }
}