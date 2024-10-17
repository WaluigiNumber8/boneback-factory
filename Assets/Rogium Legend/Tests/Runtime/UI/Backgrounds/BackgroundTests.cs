using System.Collections;
using NUnit.Framework;
using Rogium.Tests.Core;
using Rogium.UserInterface.Backgrounds;

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
            OverseerLoader.LoadBackgroundOverseer();
            overseer = BackgroundOverseerMono.GetInstance();
            yield return null;
        }

        [Test]
        public void Should_ShowRedBackground_WhenGameStarts()
        {
            Assert.That(overseer.IsSetToMainMenu(), Is.EqualTo(true));
        }
    }
}