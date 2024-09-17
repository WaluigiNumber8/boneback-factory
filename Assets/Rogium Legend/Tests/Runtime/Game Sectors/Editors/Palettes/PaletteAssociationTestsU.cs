using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Systems.Toolbox;
using UnityEngine;

namespace Rogium.Tests.Editors.Palettes
{
    /// <summary>
    /// Utils for <see cref="PaletteAssociationTests"/>.
    /// </summary>
    public static class PaletteAssociationTestsU
    {
        public static void SwitchToFirstPalette()
        {
            PaletteAsset palette = PackEditorOverseer.Instance.CurrentPack.Palettes[0];
            SpriteEditorOverseerMono.GetInstance().SwitchPalette(palette);
        }

        public static void FillSpriteEditorGrid()
        {
            SpriteEditorOverseerMono.GetInstance().Toolbox.SwitchTool(ToolType.Fill);
            SpriteEditorOverseerMono.GetInstance().UpdateCell(Vector2Int.zero);
        }
    }
}