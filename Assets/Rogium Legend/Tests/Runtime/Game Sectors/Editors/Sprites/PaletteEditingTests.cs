using System.Collections;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Palettes;
using Rogium.Editors.Sprites;
using Rogium.Systems.ActionHistory;
using Rogium.Systems.GASExtension;
using Rogium.Systems.GridSystem;
using Rogium.Tests.Core;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.PointerDataCreator;
using static Rogium.Tests.Editors.Sprites.PaletteEditingTestsU;
using static Rogium.Tests.UI.Interactables.Properties.InteractableUtils;

namespace Rogium.Tests.Editors.Sprites
{
    /// <summary>
    /// Tests for editing the palette in the Sprite Editor.
    /// </summary>
    public class PaletteEditingTests : MenuTestBase
    {
        private SpriteEditorOverseerMono spriteEditor;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            OverseerLoader.LoadModalWindowBuilder();
            OverseerLoader.LoadUIBuilder();
            yield return null;
            yield return MenuLoader.PrepareSelectionMenu();
            yield return MenuLoader.PrepareSpriteEditor();
            spriteEditor = SpriteEditorOverseerMono.GetInstance();
            ActionHistorySystem.ClearHistory();
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_OpenColorPicker_WhenPaletteSlotRightClicked()
        {
            spriteEditor.Palette.GetSlot(0).OnPointerClick(RightClick());
            yield return null;
            ColorPickerWindow colorPicker = FindFirstColorPickerWindow();

            Assert.That(colorPicker.gameObject.activeSelf, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_ChangeColorSlotColor_WhenColorPickerColorChanged()
        {
            yield return UpdateColorSlot(Color.blue);
            yield return null;

            Assert.That(spriteEditor.Palette.GetSlot(0).CurrentColor, Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_UpdateCurrentBrushColor_WhenChangedColorOfCurrentColorSlot()
        {
            yield return UpdateColorSlot(Color.blue);
            yield return null;

            Assert.That(spriteEditor.CurrentBrushColor, Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_AddSlotColorChangeToActionHistory()
        {
            yield return UpdateColorSlot(Color.blue);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;

            Assert.That(ActionHistorySystem.UndoCount, Is.GreaterThanOrEqualTo(1));
        }

        [UnityTest]
        public IEnumerator Should_RevertColorSlotChange_WhenUndoIsCalled()
        {
            yield return UpdateColorSlot(Color.blue);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;
            yield return UpdateColorSlot(Color.red);
            ActionHistorySystem.ForceEndGrouping();
            yield return null;
            ActionHistorySystem.UndoLast();

            Assert.That(spriteEditor.Palette.GetSlot(0).CurrentColor, Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_ApplyColorChangeToGrid_WhenColorSlotColorChanged()
        {
            yield return UpdateColorSlot(Color.blue);
            spriteEditor.UpdateCell(new Vector2Int(0, 0));
            yield return UpdateColorSlot(Color.red);

            Assert.That(spriteEditor.CurrentGridSprite.texture.GetPixel(0, 0), Is.EqualTo(Color.red));
        }

        [UnityTest]
        public IEnumerator Should_RevertGridColorChange_WhenUndoIsCalled()
        {
            yield return UpdateColorSlot(Color.blue);
            spriteEditor.UpdateCell(new Vector2Int(0, 0));
            yield return UpdateColorSlot(Color.red);
            ActionHistorySystem.UndoLast();

            Assert.That(spriteEditor.CurrentGridSprite.texture.GetPixel(0, 0), Is.EqualTo(Color.blue));
        }

        [Test]
        public void Should_NotOpenPaletteDialog_WhenPaletteEditedAndOverriden()
        {
            GASButtonActions.SavePaletteAsOverride();
            Assert.That(ModalWindowBuilder.GetInstance().GenericActiveWindows, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator Should_FlagPaletteAsChanged_WhenPaletteSlotColorChanged()
        {
            yield return UpdateColorSlot(Color.blue);
            Assert.That(spriteEditor.PaletteChanged, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_FlagPaletteAsNotChanged_WhenEditedPaletteSwitched()
        {
            yield return UpdateColorSlot(Color.blue);
            spriteEditor.SwitchPalette(new PaletteAsset.Builder().Build());
            Assert.That(spriteEditor.PaletteChanged, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_FlagPaletteAsChanged_WhenPaletteSlotColorChangedThenUndoThenChanged()
        {
            yield return UpdateColorSlot(Color.blue);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            yield return UpdateColorSlot(Color.red);
            Assert.That(spriteEditor.PaletteChanged, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_FlagPaletteAsNotChanged_WhenPaletteSlotColorChangedAndUndoIsCalled()
        {
            yield return UpdateColorSlot(Color.blue);
            ActionHistorySystem.ForceEndGrouping();
            ActionHistorySystem.UndoLast();
            Assert.That(spriteEditor.PaletteChanged, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_OverridePaletteAsset_WhenEditedAndOverriden()
        {
            yield return UpdateColorSlot(Color.blue);
            GASButtonActions.SavePaletteAsOverride();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Colors[0], Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_UpdatePaletteIcon_WhenEditedAndOverriden()
        {
            yield return UpdateColorSlot(Color.blue, 0);
            GASButtonActions.SavePaletteAsOverride();
            yield return null;
            Texture2D icon = PackEditorOverseer.Instance.CurrentPack.Palettes[0].Icon.texture;
            Assert.That(icon.GetPixel(0, icon.height-1), Is.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_OpenPaletteDialog_WhenPaletteEditedAndSavedAsNew()
        {
            yield return UpdateColorSlot(Color.blue);
            GASButtonActions.SavePaletteAsNew();
            Assert.That(ModalWindowBuilder.GetInstance().GenericActiveWindows, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator Should_CreatePaletteClone_WhenEditedAndSavedAsNew()
        {
            yield return SavePaletteAsNewAndConfirm();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes.Count, Is.EqualTo(2));
        }

        [UnityTest]
        public IEnumerator Should_CreatePaletteCloneWithOnly1Association_WhenEditedAndSavedAsNew()
        {
            yield return SavePaletteAsNewAndConfirm();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[1].AssociatedAssetsIDs.Count, Is.EqualTo(1));
        }
        
        [UnityTest]
        public IEnumerator Should_CreatePaletteCloneWithOnlyCurrentSpriteAssociation_WhenEditedAndSavedAsNew()
        {
            yield return SavePaletteAsNewAndConfirm();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[1].AssociatedAssetsIDs.Contains(PackEditorOverseer.Instance.CurrentPack.Sprites[0].ID), Is.True);
        }

        [UnityTest]
        public IEnumerator Should_NotAddAssociationToOriginalPalette_WhenPaletteEditedAndSavedAsNew()
        {
            yield return SavePaletteAsNewAndConfirm();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[0].AssociatedAssetsIDs.Count, Is.EqualTo(0));
        }

        [UnityTest]
        public IEnumerator Should_SetSpriteAssociatedPaletteIDToClone_WhenPaletteEditedAndSavedAsNew()
        {
            yield return SavePaletteAsNewAndConfirm();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Sprites[0].AssociatedPaletteID, Is.EqualTo(PackEditorOverseer.Instance.CurrentPack.Palettes[1].ID));
        }

        [UnityTest]
        public IEnumerator Should_SetSpriteAssociatedPaletteToMissing_WhenAssociatedPaletteRemoved()
        {
            yield return SavePaletteAsNewAndConfirm();
            PackEditorOverseer.Instance.RemovePalette(1);
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Sprites[0].AssociatedPaletteID, Is.EqualTo(EditorDefaults.EmptyAssetID));
        }

        [UnityTest]
        public IEnumerator Should_LoadMissingPaletteInEditor_WhenAssociatedPaletteRemoved()
        {
            yield return SavePaletteAsNewAndConfirm();
            PackEditorOverseer.Instance.RemovePalette(1);
            yield return null;
            PackEditorOverseer.Instance.ActivateSpriteEditor(0);
            yield return null;
            Assert.That(SpriteEditorOverseerMono.GetInstance().Palette.GetSlot(0).CurrentColor, Is.EqualTo(EditorDefaults.Instance.MissingPalette[0]));
        }

        [UnityTest]
        public IEnumerator Should_NotSaveChanges_WhenSpriteHasTheDefaultPaletteAssociated()
        {
            yield return UpdateColorSlotForSpriteWithMissingPalette();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Sprites[0].Icon.texture.GetPixel(0, 0), Is.EqualTo(EditorDefaults.Instance.MissingPalette[0]));
        }
        
        [UnityTest]
        public IEnumerator Should_NotOverrideMissingPaletteColors_WhenEditedAndSaved()
        {
            yield return UpdateColorSlotForSpriteWithMissingPalette();
            Assert.That(EditorDefaults.Instance.MissingPalette[0], Is.Not.EqualTo(Color.blue));
        }

        [UnityTest]
        public IEnumerator Should_SaveEditedMissingPalette_WhenEditedAndSavedAsCopy()
        {
            yield return UpdateColorSlotForSpriteWithMissingPalette();
            Object.FindFirstObjectByType<ModalWindow>()?.OnAccept();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes.Count, Is.EqualTo(2));   
        }
        
        [UnityTest]
        public IEnumerator Should_SaveEditedColorToMissingPaletteCopy_WhenEditedAndSavedAsCopy()
        {
            yield return UpdateColorSlotForSpriteWithMissingPalette();
            Object.FindFirstObjectByType<ModalWindow>()?.OnAccept();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[1].Colors[0], Is.EqualTo(Color.blue));   
        }
        
        [UnityTest]
        public IEnumerator Should_KeepUneditedColorsInMissingPaletteCopy_WhenEditedAndSavedAsCopy()
        {
            yield return UpdateColorSlotForSpriteWithMissingPalette();
            Object.FindFirstObjectByType<ModalWindow>()?.OnAccept();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[1].Colors[1], Is.EqualTo(EditorDefaults.Instance.MissingPalette[1]));   
        }

        [UnityTest]
        public IEnumerator Should_CreateCopyWithProperIcon_WhenEditedAndSavedAsCopy()
        {
            yield return UpdateColorSlotForSpriteWithMissingPalette();
            Object.FindFirstObjectByType<ModalWindow>()?.OnAccept();
            yield return null;
            Texture2D icon = PackEditorOverseer.Instance.CurrentPack.Palettes[1].Icon.texture;
            Assert.That(icon.GetPixel(0, icon.height-1), Is.EqualTo(Color.blue));   
        }

        [UnityTest]
        public IEnumerator Should_NotChangePaletteColors_WhenEditedWithoutSaving()
        {
            yield return UpdateColorSlot(Color.blue);
            GASButtonActions.SaveChangesSprite();
            Object.FindFirstObjectByType<ModalWindow>()?.OnDeny();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Colors[0], Is.Not.EqualTo(Color.blue));
        }
        
        [UnityTest]
        public IEnumerator Should_KeepOriginalPaletteColors_WhenEditedWithoutSaving()
        {
            yield return UpdateColorSlot(Color.blue);
            GASButtonActions.SaveChangesSprite();
            Object.FindFirstObjectByType<ModalWindow>()?.OnDeny();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Palettes[0].Colors[0], Is.EqualTo(Color.magenta));
        }

        [UnityTest]
        public IEnumerator Should_SaveSpriteChanges_WhenPaletteEditedWithoutSaving()
        {
            yield return UpdateColorSlot(Color.blue);
            FillSpriteEditorGrid();
            GASButtonActions.SaveChangesSprite();
            Object.FindFirstObjectByType<ModalWindow>()?.OnDeny();
            Texture2D icon = PackEditorOverseer.Instance.CurrentPack.Sprites[0].Icon.texture;
            Assert.That(icon.GetPixel(0, icon.height-1), Is.EqualTo(Color.magenta));
        }
        
        [UnityTest]
        public IEnumerator Should_SaveSpriteWithMissingPaletteChanges_WhenPaletteEditedWithoutSaving()
        {
            yield return UpdateColorSlotForSpriteWithMissingPalette();
            FillSpriteEditorGrid();
            GASButtonActions.SaveChangesSprite();
            Object.FindFirstObjectByType<ModalWindow>()?.OnDeny();
            Texture2D icon = PackEditorOverseer.Instance.CurrentPack.Sprites[0].Icon.texture;
            Assert.That(icon.GetPixel(0, icon.height-1), Is.EqualTo(EditorDefaults.Instance.MissingPalette[0]));
        }

        [UnityTest]
        public IEnumerator Should_UpdateGridVisualPreviewerColor_WhenPaletteSlotColorChanged()
        {
            yield return UpdateColorSlot(Color.blue);
            Color previewerColor = Object.FindFirstObjectByType<GridVisualPreviewer>().Color;
            Assert.That(previewerColor, Is.EqualTo(Color.blue));
        }
        
        [UnityTest]
        public IEnumerator Should_NotUpdateGridVisualPreviewerColor_WhenUnselectedPaletteSlotColorChanged()
        {
            yield return UpdateColorSlot(Color.blue, 1);
            Color previewerColor = Object.FindFirstObjectByType<GridVisualPreviewer>().Color;
            Assert.That(previewerColor, Is.EqualTo(Color.magenta));
        }
    }
}