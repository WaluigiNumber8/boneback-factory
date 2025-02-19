using System.Collections;
using NUnit.Framework;
using Rogium.Systems.GASExtension;
using Rogium.Tests.Core;
using Rogium.UserInterface.Backgrounds;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.UI.Backgrounds
{
    /// <summary>
    /// Tests for changing the background of the menu.
    /// </summary>
    public class BackgroundTests : MenuTestBase
    {

        private BackgroundOverseerMono overseer;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            TUtilsOverseerLoader.LoadBackgroundOverseer();
            overseer = BackgroundOverseerMono.Instance;
            yield return null;
        }

        [Test]
        public void Should_ShowMainMenuBackground_WhenGameStarts()
        {
            Assert.That(overseer.IsSetToMainMenu(), Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator Should_ShowEditorBackground_WhenSwitched()
        {
            overseer.SwitchToEditor();
            yield return new WaitForSeconds(1f);
            Assert.That(overseer.IsSetToEditor(), Is.EqualTo(true));
        }
        
        [UnityTest]
        public IEnumerator Should_ShowGameMenuBackgroundAfterOpeningEditor_WhenSwitched()
        {
            overseer.SwitchToEditor();
            yield return new WaitForSeconds(1f);
            overseer.SwitchToMainMenu();
            yield return new WaitForSeconds(1f);
            overseer.SwitchToGameMenu();
            yield return new WaitForSeconds(1f);
            Assert.That(overseer.IsSetToGameMenu(), Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator Should_ShowEditorBackground_WhenGoToPackSelectionMenu()
        {
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            GASActions.OpenSelectionPack();
            yield return null;
            Assert.That(overseer.IsSetToEditor(), Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator Should_ShowMainMenuBackground_WhenGoToMainMenuFromPackSelection()
        {
            yield return TUtilsMenuLoader.PrepareSelectionMenu();
            GASActions.OpenSelectionPack();
            yield return null;
            GASActions.ReturnFromSelectionMenu();
            yield return null;
            Assert.That(overseer.IsSetToMainMenu(), Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator Should_ShowGameMenuBackground_WhenGoToCampaignSelection()
        {
            yield return TUtilsMenuLoader.PrepareCampaignSelection();
            GASActions.OpenSelectionCampaign();
            yield return null;
            Assert.That(overseer.IsSetToGameMenu(), Is.EqualTo(true));
        }

        [UnityTest]
        public IEnumerator Should_ShowMainMenuBackground_WhenGoToMainMenuFromCampaignSelection()
        {
            yield return TUtilsMenuLoader.PrepareCampaignSelection();
            GASActions.OpenSelectionCampaign();
            yield return null;
            GASActions.ReturnFromSelectionMenu();
            yield return null;
            Assert.That(overseer.IsSetToMainMenu(), Is.EqualTo(true));
        }
    }
}