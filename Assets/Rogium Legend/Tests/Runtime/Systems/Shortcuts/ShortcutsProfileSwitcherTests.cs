using System.Collections;
using NUnit.Framework;
using Rogium.Core;
using Rogium.Systems.Input;
using Rogium.Tests.Core;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsMenuNavigation;

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
            yield return OpenEditor(AssetType.Room);
            Assert.That(input.ShortcutsRoom.IsMapEnabled, Is.True);
        }
    }
}