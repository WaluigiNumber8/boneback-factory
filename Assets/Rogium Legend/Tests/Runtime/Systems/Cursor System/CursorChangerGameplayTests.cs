using System.Collections;
using NUnit.Framework;
using RedRats.UI.Core.Cursors;
using Rogium.Gameplay.Core;
using Rogium.Tests.Core;
using Rogium.UserInterface.Cursors;
using UnityEngine.TestTools;
using static Rogium.Tests.Systems.Cursors.CursorUtils;

namespace Rogium.Tests.Systems.Cursors
{
    /// <summary>
    /// Tests for the <see cref="CursorChangerGameplay"/>.
    /// </summary>
    public class CursorChangerGameplayTests : GameplayTestBase
    {
        private CursorOverseerMono cursorOverseer;
        private GameplayOverseerMono gameplay;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            cursorOverseer = CursorOverseerMono.Instance;
            gameplay = GameplayOverseerMono.Instance;
        }

        [UnityTest]
        public IEnumerator ToGameplay_Should_SetCursorToGameplay_WhenGameStarted()
        {
            CreateChangerGameplay();
            yield return null;
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Gameplay));
        }
        
        [UnityTest]
        public IEnumerator ToDefault_Should_SetCursorToDefault_WhenGamePaused()
        {
            CreateChangerGameplay();
            yield return null;
            
            gameplay.Pause();
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Default));
        }

        [UnityTest]
        public IEnumerator ToGameplay_Should_SetCursorToGameplay_WhenGameResumed()
        {
            CreateChangerGameplay();
            
            gameplay.Pause();
            yield return null;
            gameplay.Resume();
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Gameplay));
        }
       
    }
}