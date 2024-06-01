using NSubstitute;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GridSystem;
using UnityEngine;

namespace Rogium.Tests.Editors.Sprites
{
    /// <summary>
    /// Utils for the <see cref="SpriteEditorOverseerMono"/> tests.
    /// </summary>
    public static class SpriteEditorUtils
    {
        private static readonly SpriteEditorOverseerMono spriteEditor = SpriteEditorOverseerMono.GetInstance();

        public static void FillEntireGrid()
        {
            ObjectGrid<int> grid = spriteEditor.GetCurrentGridCopy;
            IColorSlot color = Substitute.For<IColorSlot>();
            color.CurrentColor.Returns(Color.red);
            spriteEditor.UpdateCurrentColor(color);
            
            ActionHistorySystem.ForceBeginGrouping();
            for (int i = 0; i < grid.Width; i++)
            {
                for (int j = 0; j < grid.Height; j++)
                {
                    spriteEditor.UpdateGridCell(new Vector2Int(i, j));
                }
            }
            ActionHistorySystem.ForceEndGrouping();
        }
    }
}