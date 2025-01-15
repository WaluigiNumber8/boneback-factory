using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.AssetSelection.Campaigns;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Systems.GASExtension;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsAssetCreator;
using static Rogium.Tests.Core.TUtilsMenuNavigation;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for the Campaign Selection shortcut actions.
    /// </summary>
    public class ShortcutActionsCampaignSelectionTests : MenuTestWithInputBase
    {
        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            yield return MenuLoader.PrepareCampaignSelection();
            OverseerLoader.LoadModalWindowBuilder();
            yield return null;
            AddNewCampaignToLibrary(PackEditorOverseer.Instance.CurrentPack);
            AddNewCampaignToLibrary(PackEditorOverseer.Instance.CurrentPack);
            AddNewCampaignToLibrary(PackEditorOverseer.Instance.CurrentPack);
            yield return OpenSelectionMenu(AssetType.Campaign, 0);
        }
        
        [UnityTest]
        public IEnumerator Should_ReturnToMainMenu_WhenShortcutPressed()
        {
            yield return MenuLoader.PrepareMainMenu();
            yield return OpenSelectionMenu(AssetType.Campaign, 0);
            yield return new WaitForSecondsRealtime(0.1f);
            i.Trigger(input.UI.Cancel.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.MainMenu));
        }

        [UnityTest]
        public IEnumerator Should_CreateNewAsset_WhenShortcutPressed()
        {
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.nKey);
            i.Trigger(input.Shortcuts.NewAsset.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_Edit_WhenShortcutPressed()
        {
            yield return MenuLoader.PrepareCampaignEditor(false);
            i.Trigger(input.Shortcuts.Edit.Action);
            yield return null;
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.CampaignEditor));
        }

        [UnityTest]
        public IEnumerator Should_OpenDeleteDialog_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.Delete.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_SwitchRight_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.SwitchRight.Action);
            yield return null;
            Assert.That(CampaignAssetSelectionOverseer.Instance.GetSelectedCampaign().ID, Is.EqualTo(ExternalLibraryOverseer.Instance.Campaigns[1].ID));
        }
        
        [UnityTest]
        public IEnumerator Should_SwitchLeft_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.SwitchLeft.Action);
            yield return null;
            Assert.That(CampaignAssetSelectionOverseer.Instance.GetSelectedCampaign().ID, Is.EqualTo(ExternalLibraryOverseer.Instance.Campaigns[2].ID));
        }
        
        [UnityTest]
        public IEnumerator Should_RefreshCurrentCampaign_WhenEditedThenShortcutPressed()
        {
            yield return MenuLoader.PrepareSelectionMenu();
            yield return MenuLoader.PrepareRoomEditor();
            yield return OpenSelectionMenu(AssetType.Pack, 0);
            yield return OpenEditor(AssetType.Room);
            yield return TUtilsRoomEditor.FillTileLayer();
            GASActions.SaveChangesRoom();
            GASActions.ReturnFromSelectionMenu();
            GASActions.ReturnFromSelectionMenu();
            PackEditorOverseer.Instance.CompleteEditing();
            yield return null;
            yield return OpenSelectionMenu(AssetType.Campaign, 0);
            i.Trigger(input.Shortcuts.RefreshCurrent.Action);
            yield return null;
            Assert.That(ExternalLibraryOverseer.Instance.Campaigns[0].DataPack.Rooms[0].TileGrid.GetAt(0, 0).ID, Is.EqualTo(ExternalLibraryOverseer.Instance.Packs[0].Tiles[0].ID));
        }

        [UnityTest]
        public IEnumerator Should_ShowRefreshAllCampaignsDialog_WhenEditedThenShortcutPressed()
        {
            yield return new WaitForSecondsRealtime(0.1f);
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.rKey);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_Play_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.Play.Action);
            yield return new WaitForSecondsRealtime(5f);
            Assert.That(SceneManager.GetActiveScene().buildIndex, Is.EqualTo(1));
        }
    }
}