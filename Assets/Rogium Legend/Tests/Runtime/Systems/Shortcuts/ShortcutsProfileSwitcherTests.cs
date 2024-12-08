using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Editors.AssetSelection;
using Rogium.Systems.GASExtension;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.Systems.Shortcuts
{
    public class ShortcutsProfileSwitcherTests : MenuTestBase
    {
        private InputSystem input;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            input = InputSystem.GetInstance();
            yield return MenuLoader.PrepareSelectionMenu();
            yield return MenuLoader.PrepareRoomEditor(false);
            OverseerLoader.LoadShortcutsOverseer();
            yield return null;
        }

        [UnityTest]
        public IEnumerator Should_SwitchToRoomShortcutsMap_WhenRoomEditorOpened()
        {
            GASButtonActions.OpenSelectionPack();
            yield return null;
            Object.FindFirstObjectByType<EditableAssetCardController>().Edit();
            SelectionMenuOverseerMono.GetInstance().Open(AssetType.Room);
            yield return null;
            Object.FindFirstObjectByType<EditableAssetCardController>().Edit();
            Assert.That(input.ShortcutsRoom.IsMapEnabled, Is.True);
        }
    
    }
}