using System.Collections;
using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.PackData;
using Rogium.Editors.RoomData;
using Rogium.Editors.TileData;
using Rogium.Global.GridSystem;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class test_Room_Editor_UI
{
    private EventSystem eventSystem;
    
    private RoomEditorUIOverseer roomEditorUIOverseer;
    private RoomEditorOverseer roomEditorOverseer;
    private EditorGridOverseer roomGrid;

    private string tileID;
    
    [SetUp]
    public void Setup()
    {
        #region Build the scene
        //Room Editor Overseer
        roomEditorOverseer = RoomEditorOverseer.Instance;
        
        //Event System
        eventSystem = AssetDatabase.LoadAssetAtPath<EventSystem>("Assets/Rogium Legend/Prefabs/Global/pref_EventSystem.prefab");
        
        //Canvas
        GameObject canvas = new GameObject();
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<GraphicRaycaster>();
        CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();

        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
        
        //Room Editor
        roomEditorUIOverseer = AssetDatabase.LoadAssetAtPath<RoomEditorUIOverseer>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_RoomEditor.prefab");
        roomGrid = roomEditorUIOverseer.GetComponentInChildren<EditorGridOverseer>();
        #endregion

        #region Build Pack & Room
        string packTitle = EditorDefaults.PackTitle;
        string packDescription = EditorDefaults.PackDescription;
        string packAuthor = EditorDefaults.Author;
        Sprite packIcon = EditorDefaults.PackIcon;

        string roomTitle = EditorDefaults.RoomTitle;
        Sprite roomIcon = EditorDefaults.RoomIcon;
        int roomDifficulty = EditorDefaults.RoomDifficulty;

        string tileTitle = EditorDefaults.TileTitle;
        Sprite tileIcon = EditorDefaults.TileIcon;
        
        LibraryOverseer.Instance.CreateAndAddPack(new PackInfoAsset(packTitle, packIcon, packAuthor, packDescription));
        LibraryOverseer.Instance.ActivatePackEditor(0);
        EditorOverseer.Instance.CreateNewTile(new TileAsset(tileTitle, tileIcon, packAuthor, TileType.Wall));
        EditorOverseer.Instance.ActivateTileEditor(0);
        tileID = TileEditorOverseer.Instance.CurrentTile.ID;
        TileEditorOverseer.Instance.CompleteEditing();
        EditorOverseer.Instance.CreateNewRoom(new RoomAsset(roomTitle, roomIcon, packAuthor, roomDifficulty));
        EditorOverseer.Instance.ActivateRoomEditor(0);
        #endregion
        
    }
    
    [TearDown]
    public void Teardown()
    {
        LibraryOverseer.Instance.RemovePack(EditorDefaults.PackTitle, EditorDefaults.Author);
    }
    
    [UnityTest]
    public IEnumerator use_brush_to_paint_tile()
    {
        roomEditorUIOverseer.UpdateGridCell(new Vector2Int(1, 3));

        yield return new WaitForSeconds(0.1f);
        
        Assert.AreEqual(tileID, roomEditorOverseer.CurrentRoom.TileGrid.GetValue(1, 3));
    }
}