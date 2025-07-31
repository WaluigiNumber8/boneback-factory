using System.Collections;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Editors.Weapons;
using Rogium.Systems.GASExtension;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using UnityEngine;

namespace Rogium.Tests.Editors.Palettes
{
    /// <summary>
    /// Utils for <see cref="PaletteAssociationTests"/>.
    /// </summary>
    public static class PaletteAssociationTestsU
    {
        public static void SwitchToPalette(int index = 0)
        {
            PaletteAsset palette = PackEditorOverseer.Instance.CurrentPack.Palettes[index];
            SpriteEditorOverseerMono.Instance.SwitchPalette(palette);
        }

        public static void FillSpriteEditorGrid()
        {
            SpriteEditorOverseerMono.Instance.Toolbox.SwitchTool(ToolType.Fill);
            SpriteEditorOverseerMono.Instance.UpdateCell(Vector2Int.zero);
        }

        public static IEnumerator UpdatePaletteColorInPaletteEditor(Color color, int index = 0)
        {
            yield return TUtilsMenuLoader.PreparePaletteEditor();
            PaletteEditorOverseerMono.Instance.UpdateColorSlotColor(color, index);
            PaletteEditorOverseer.Instance.CompleteEditing();
        }
        
        public static IEnumerator UpdateSpriteOfWeaponInEditor(int index = 0)
        {
            yield return TUtilsMenuLoader.PrepareWeaponEditor();
            WeaponEditorOverseer.Instance.CurrentAsset.UpdateIcon(PackEditorOverseer.Instance.CurrentPack.Sprites[index]);
            WeaponEditorOverseer.Instance.CompleteEditing();
        }

        public static IEnumerator UpdatePaletteColorInSpriteEditor(Color color, int index = 0)
        {
            PackEditorOverseer.Instance.ActivateSpriteEditor(index);
            yield return null;
            SpriteEditorOverseerMono.Instance.Palette.GetSlot(index).UpdateColor(color);
            SpriteEditorOverseerMono.Instance.Palette.Select(index);
            GASActions.SavePaletteAsOverride();
        }

        public static IEnumerator SwitchPaletteAndFillForSprite(int index = 0)
        {
            PackEditorOverseer.Instance.ActivateSpriteEditor(index);
            yield return null;
            SwitchToPalette();
            FillSpriteEditorGrid();
            SpriteEditorOverseer.Instance.CompleteEditing();
        }
    }
}