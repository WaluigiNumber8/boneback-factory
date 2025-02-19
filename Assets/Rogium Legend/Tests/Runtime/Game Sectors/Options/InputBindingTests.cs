using System.Collections;
using NUnit.Framework;
using RedRats.UI.Tabs;
using Rogium.Options.Core;
using Rogium.Tests.Core;
using UnityEngine.TestTools;

namespace Rogium.Tests.Options.Input
{
    /// <summary>
    /// Tests for binding inputs.
    /// </summary>
    public class InputBindingTests : MenuTestBase
    {
        private OptionsMenuOverseerMono optionsMono;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return TUtilsMenuLoader.PrepareOptionsMenu();
            optionsMono = OptionsMenuOverseerMono.Instance;
        }

        [UnityTest]
        public IEnumerator Should_ShowInputBindingScreen_WhenTabButtonClicked()
        {
            TabGroupBase tabGroup = optionsMono.GetComponentInChildren<TabGroupBase>();
            tabGroup.Switch(2);
            yield return null;
            Assert.That(tabGroup.GetPagesAsArray()[2].gameObject.activeSelf, Is.True);
        }
    }
}