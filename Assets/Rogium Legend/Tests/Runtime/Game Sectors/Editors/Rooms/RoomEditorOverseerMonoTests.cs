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
            PackAsset pack = AssetCreator.CreateAndAssignPack();
            OverseerLoader.LoadInternalLibrary();
            
            yield return null;
            OverseerLoader.LoadUIBuilder();
            OverseerLoader.LoadThemeOverseer();
            
            MenuLoader.LoadRoomEditor();
            RoomEditorOverseer.Instance.AssignAsset(pack.Rooms[0], 0);
            roomEditor = RoomEditorOverseerMono.GetInstance();
            ActionHistorySystem.ClearHistory();
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
            ActionHistorySystem.ForceEndGrouping();
            roomEditor.ClearActiveLayer();
            
            Assert.That(ActionHistorySystem.UndoCount, Is.EqualTo(2));
        }
        
        [Test]
        public void UndoLast_Should_UndoClearActiveGrid()
        {
            RoomEditorUtils.FillEntireActiveLayer();
            roomEditor.ClearActiveLayer();
            ActionHistorySystem.UndoLast();
            
            Assert.That(roomEditor.GetCurrentGridCopy.GetCellsCopy, Is.Not.All.EqualTo(new AssetData()));
        }
    }
}