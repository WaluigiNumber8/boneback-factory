using System.Collections;
using NUnit.Framework;
using RedRats.UI.Core.Cursors;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Systems.Cursors.CursorUtils;

namespace Rogium.Tests.Systems.Cursors
{
    /// <summary>
    /// Tests for the <see cref="CursorChangerUI"/>.
    /// </summary>
    public class CursorChangerUITests : MenuTestBase
    {
        private CursorOverseerMono cursorOverseer;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            cursorOverseer = CursorOverseerMono.Instance;
        }

        [UnityTest]
        public IEnumerator OnPointerEnter_Should_SetCursorToGivenType()
        {
            CursorChangerUI cursorChanger = CreateChangerUI();
            
            cursorChanger.OnPointerEnter(null);
            yield return null;
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Interact));
        }

        [UnityTest]
        public IEnumerator OnPointerExit_Should_SetCursorToDefault()
        {
            CursorChangerUI cursorChanger = CreateChangerUI();
            
            cursorChanger.OnPointerEnter(null);
            yield return null;
            cursorChanger.OnPointerExit(null);
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Default));
        }

        [UnityTest]
        public IEnumerator OnDestroy_Should_SetCursorToDefault()
        {
            CursorChangerUI cursorChanger = CreateChangerUI();
            
            cursorChanger.OnPointerEnter(null);
            yield return null;
            Object.Destroy(cursorChanger);
            yield return null;
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Default));
        }
    }
}