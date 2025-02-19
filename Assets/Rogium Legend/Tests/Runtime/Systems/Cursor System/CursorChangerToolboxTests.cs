using System.Collections;
using NUnit.Framework;
using RedRats.UI.Core.Cursors;
using Rogium.Systems.Toolbox;
using Rogium.Tests.Core;
using Rogium.UserInterface.Cursors;
using UnityEngine.TestTools;
using static Rogium.Tests.Systems.Cursors.CursorUtils;

namespace Rogium.Tests.Systems.Cursors
{
    /// <summary>
    /// Tests for the <see cref="CursorChangerToolbox"/>.
    /// </summary>
    public class CursorChangerToolboxTests : MenuTestBase
    {
        private CursorOverseerMono cursorOverseer;
        
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            cursorOverseer = CursorOverseerMono.Instance;
        }

        [UnityTest]
        public IEnumerator UpdateCursor_Should_SetCursorToGivenTool_IfWithinBounds()
        {
            CursorChangerToolbox cursorChanger = CreateChangerToolbox();
            
            cursorChanger.OnPointerEnter(null);
            cursorChanger.UpdateCursor(ToolType.Brush);
            yield return null;
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.ToolBrush));
        }

        [UnityTest]
        public IEnumerator UpdateCursor_Should_PrepareCursorButNotSet_IfNotWithinBounds()
        {
            CursorChangerToolbox cursorChanger = CreateChangerToolbox();
            
            cursorChanger.UpdateCursor(ToolType.Brush);
            yield return null;
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Default));
        }
        
        [UnityTest]
        public IEnumerator UpdateCursor_Should_PrepareToSetCursor_IfWithinBounds()
        {
            CursorChangerToolbox cursorChanger = CreateChangerToolbox();
            
            cursorChanger.UpdateCursor(ToolType.Brush);
            yield return null;
            cursorChanger.OnPointerEnter(null);
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.ToolBrush));
        }
        
        [UnityTest]
        public IEnumerator UpdateCursor_Should_SetCursorToDefault_IfNotWithinBounds()
        {
            CursorChangerToolbox cursorChanger = CreateChangerToolbox();
            
            cursorChanger.UpdateCursor(ToolType.Brush);
            cursorChanger.OnPointerEnter(null);
            yield return null;
            cursorChanger.OnPointerExit(null);
            yield return null;
            
            Assert.That(cursorOverseer.CurrentCursor, Is.EqualTo(CursorType.Default));
        }
    }
}