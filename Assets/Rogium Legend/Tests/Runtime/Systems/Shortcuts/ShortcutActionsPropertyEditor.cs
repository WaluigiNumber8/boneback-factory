using System.Collections;
using NUnit.Framework;
using RedRats.UI.MenuSwitching;
using Rogium.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.PropertyEditor;
using Rogium.Tests.Core;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsMenuNavigation;
using static Rogium.Tests.Core.TUtilsModalWindow;
using static Rogium.Tests.Core.TUtilsPropertyEditor;

namespace Rogium.Tests.Systems.Shortcuts
{
    /// <summary>
    /// Tests for the property editor shortcut actions.
    /// </summary>
    public class ShortcutActionsPropertyEditor : MenuTestWithInputBase
    {
        private PropertyEditorOverseerMono editor;

        public override IEnumerator SetUp()
        {
            yield return base.SetUp();
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            yield return TUtilsMenuLoader.PrepareWeaponEditor(false);
            TUtilsOverseerLoader.LoadModalWindowBuilder();
            editor = PropertyEditorOverseerMono.GetInstance();
            yield return null;
            yield return OpenEditor(AssetType.Weapon);
        }
        
        [UnityTest]
        public IEnumerator Should_Undo_WhenShortcutPressed()
        {
            yield return EditFirstInputField("Fred");
            string value = editor.GetComponentInChildren<InteractablePropertyInputField>().PropertyValue;
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.yKey);
            i.Trigger(input.Shortcuts.Undo.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetComponentInChildren<InteractablePropertyInputField>().PropertyValue, Is.Not.EqualTo(value));
        }

        [UnityTest]
        public IEnumerator Should_Redo_WhenShortcutPressed()
        {
            yield return EditFirstInputField("Fred");
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.zKey);
            i.Trigger(input.Shortcuts.Redo.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(editor.GetComponentInChildren<InteractablePropertyInputField>().PropertyValue, Is.EqualTo("Fred"));
        }

        [UnityTest]
        public IEnumerator Should_SaveChanges_WhenShortcutPressed()
        {
            string originalData = PackEditorOverseer.Instance.CurrentPack.Weapons[0].Title;
            yield return EditFirstInputField("Fred");
            i.Press(keyboard.ctrlKey);
            i.Press(keyboard.sKey);
            i.Trigger(input.Shortcuts.Save.Action);
            yield return new WaitForSecondsRealtime(0.1f);
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Weapons[0].Title, Is.Not.EqualTo(originalData));
        }

        [UnityTest]
        public IEnumerator Should_CancelChanges_WhenShortcutPressed()
        {
            string originalData = PackEditorOverseer.Instance.CurrentPack.Weapons[0].Title;
            yield return EditFirstInputField("Fred");
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            yield return null;
            yield return WindowAccept();
            Assert.That(PackEditorOverseer.Instance.CurrentPack.Weapons[0].Title, Is.EqualTo(originalData));
        }

        [UnityTest]
        public IEnumerator Should_CloseSoundPickerWindow_WhenShortcutPressed()
        {
            editor.GetComponentInChildren<InteractablePropertySoundField>().OpenWindow();
            yield return null;
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            Assert.That(Object.FindFirstObjectByType<SoundPickerWindow>(), Is.Not.Null);
            Assert.That(Object.FindFirstObjectByType<SoundPickerWindow>().IsOpen, Is.False);
        }

        [UnityTest]
        public IEnumerator Should_CloseSoundPickerWindowWithoutLeavingPropertyEditor_WhenShortcutPressed()
        {
            editor.GetComponentInChildren<InteractablePropertySoundField>().OpenWindow();
            yield return null;
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.PropertyEditor));
        }
        
        [UnityTest]
        public IEnumerator Should_CloseSoundPickingWindowButNotSoundPickerWindow_WhenShortcutPressed()
        {
            editor.GetComponentInChildren<InteractablePropertySoundField>().OpenWindow();
            yield return null;
            Object.FindFirstObjectByType<SoundPickerWindow>().GetComponentInChildren<InteractablePropertyAssetField>().OpenWindow();
            i.Trigger(input.UI.Cancel.Action);
            yield return null;
            Assert.That(Object.FindFirstObjectByType<SoundPickerWindow>().IsOpen, Is.True);
            Assert.That(Object.FindFirstObjectByType<AssetPickerWindow>().IsOpen, Is.False);
        }
        
        [UnityTest]
        public IEnumerator Should_CloseSoundPickingWindowAndNotLeavePropertyEditor_WhenShortcutPressed()
        {
            editor.GetComponentInChildren<InteractablePropertySoundField>().OpenWindow();
            yield return new WaitForSeconds(0.1f);
            Object.FindFirstObjectByType<SoundPickerWindow>().GetComponentInChildren<InteractablePropertyAssetField>().OpenWindow();
            yield return new WaitForSeconds(0.1f);
            i.Trigger(input.UI.Cancel.Action);
            yield return new WaitForSeconds(0.1f);
            Assert.That(MenuSwitcher.GetInstance().CurrentMenu, Is.EqualTo(MenuType.PropertyEditor));
        }
    }
}