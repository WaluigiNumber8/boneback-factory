using System.Collections;
using NUnit.Framework;
using RedRats.UI.Core.Cursors;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.Systems.Cursors
{
    /// <summary>
    /// Tests for the <see cref="CursorOverseerMono"/>.
    /// </summary>
    public class CursorOverseerMonoTests : MenuTestBase
    {
        private CursorOverseerMono cursorOverseer;

        public override IEnumerator Setup()
        {
            yield return base.Setup();
            cursorOverseer = CursorOverseerMono.GetInstance();
        }

        [UnityTest]
        public IEnumerator Start_Should_SetCursorToDefault()
        {
            yield return null;
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Default));
        }

        [Test]
        public void Set_Should_SetCursorToGivenType()
        {
            cursorOverseer.Set(CursorType.Interact);
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Interact));
        }

        [Test]
        public void SetDefault_Should_SetCursorToDefault()
        {
            cursorOverseer.Set(CursorType.Interact);
            cursorOverseer.SetDefault();
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Default));
        }
    }
}