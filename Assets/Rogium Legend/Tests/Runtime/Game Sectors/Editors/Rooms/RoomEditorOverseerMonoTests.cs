using System.Collections;
using System.Linq;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using UnityEngine;
using UnityEngine.TestTools;
using static Rogium.Tests.Core.TUtilsAssetCreator;

namespace Rogium.Tests.Editors.Rooms
{
    /// <summary>
    /// Tests for the <see cref="RoomEditorOverseerMono"/> class.
    /// </summary>
    public class RoomEditorOverseerMonoTests : MenuTestBase
    {
        private RoomEditorOverseerMono roomEditor;

        [UnitySetUp]
        public override IEnumerator Setup()
        {
            yield return base.Setup();
            yield return CreateAndAssignPack();
            yield return MenuLoader.PrepareRoomEditor();
            roomEditor = RoomEditorOverseerMono.GetInstance();
            yield return null;
        }
        
        [Test]
        public void ClearActiveLayer_Should_ClearDataGrid()
        {
            roomEditor.ClearActiveLayer();
            Assert.That(roomEditor.GetCurrentGridCopy.GetCellsCopy, Is.All.EqualTo(new AssetData()));
        }
        
        [Test]
        public void ClearActiveLayer_Should_ClearVisualGrid()
        {
            roomEditor.ClearActiveLayer();
            Color[] pixels = roomEditor.CurrentGridSprite.texture.GetPixels();
            bool allTransparent = pixels.All(pixel => pixel.a == 0);

            Assert.That(allTransparent, Is.True);
        }
        
        [Test]
        public void ClearActiveLayer_Should_AddToUndoHistory()
        {
            RoomEditorUtils.FillEntireActiveLayer();
            roomEditor.ClearActiveLayer();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }
        
        [UnityTest]
        public IEnumerator UndoLast_Should_UndoClearActiveGrid()
        {
            RoomEditorUtils.FillEntireActiveLayer();
            yield return null;
            roomEditor.ClearActiveLayer();
            yield return null;
            ActionHistorySystem.Undo();
            
            Assert.That(roomEditor.GetCurrentGridCopy.GetCellsCopy, Is.Not.All.EqualTo(new AssetData()));
        }
    }
}