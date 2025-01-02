using System.Collections;
using System.Linq;
using NUnit.Framework;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Editors.Campaign;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsAssetCreator;
using static Rogium.Tests.Core.TUtilsMenuNavigation;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for the campaign editor shortcut actions.
    /// </summary>
    public class ShortcutActionsCampaignEditorTests : MenuTestWithInputBase
    {
        private CampaignEditorOverseerMono editor;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            yield return MenuLoader.PrepareCampaignSelection();
            yield return MenuLoader.PrepareCampaignEditor(false);
            AddNewPackToLibrary();
            AddNewPackToLibrary();
            AddNewCampaignToLibrary();
            OverseerLoader.LoadModalWindowBuilder();
            editor = CampaignEditorOverseerMono.GetInstance();
            yield return null;
            yield return OpenEditor(AssetType.Campaign);
        }
        
        [UnityTest]
        public IEnumerator Should_ShowCombineDialog_WhenShortcutPressed()
        {
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.sKey);
            i.Trigger(input.Shortcuts.Save.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_CancelChanges_WhenShortcutPressed()
        {
            i.Trigger(input.Shortcuts.Cancel.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<ModalWindow>().IsOpen, Is.True);
        }

        [UnityTest]
        public IEnumerator Should_SelectAll_WhenShortcutPressed()
        {
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.aKey);
            i.Trigger(input.Shortcuts.SelectAll.Action);
            yield return null;
            Assert.That(editor.GetComponentsInChildren<AssetCardController>().All(c => c.IsOn), Is.True);
        }

        [UnityTest]
        public IEnumerator Should_DeselectAll_WhenShortcutPressed()
        {
            editor.SelectionPicker.Selector.GetCard(0).SetToggle(true);
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.dKey);
            i.Trigger(input.Shortcuts.DeselectAll.Action);
            yield return null;
            Assert.That(editor.GetComponentsInChildren<AssetCardController>().Any(c => c.IsOn), Is.False);
        }

        [UnityTest]
        public IEnumerator Should_SelectRandom_WhenShortcutPressed()
        {
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.rKey);
            i.Trigger(input.Shortcuts.SelectRandom.Action);
            yield return null;
            Assert.That(editor.GetComponentsInChildren<AssetCardController>().Any(c => c.IsOn), Is.True);
        }
    }
}