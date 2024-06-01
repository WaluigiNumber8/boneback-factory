using System.Collections;
using System.Linq;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Systems.ActionHistory;
using Rogium.Tests.Core;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

namespace Rogium.Tests.Editors.Rooms
{
    /// <summary>
    /// Tests for the <see cref="RoomEditorOverseerMono"/> class.
    /// </summary>
    [RequiresPlayMode]
    public class RoomEditorOverseerMonoTests
    {
        private readonly GameObject internalLibraryPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/pref_InternalLibrary.prefab");
        private readonly GameObject uiBuilderPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/Builders/pref_Builder_InteractableProperties.prefab");
        private readonly GameObject themeOverseerPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/Overseers/pref_Overseer_Themes.prefab");
        
        private RoomEditorOverseerMono roomEditor;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            SceneLoader.LoadUIScene();
            yield return null;
            PackAsset pack = AssetCreator.CreatePack();
            PackEditorOverseer.Instance.AssignAsset(pack, 0);
            Object.Instantiate(internalLibraryPrefab);
            Object.Instantiate(uiBuilderPrefab);
            Object.Instantiate(themeOverseerPrefab);
            
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