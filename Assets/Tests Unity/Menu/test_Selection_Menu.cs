using System.Collections;
using NUnit.Framework;
using RogiumLegend.Editors.Core;
using RogiumLegend.Editors.PackData;
using RogiumLegend.Global.MenuSystem.AssetSelection;
using RogiumLegend.Global.MenuSystem.Interactables;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class test_Selection_Menu
{
    private GameObject canvas;
    private PackInfoAsset packInfo;
    private AssetSelectionOverseer selectionMenu;

    [SetUp]
    public void Setup()
    {
        #region Prepare Test Pack
        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        packInfo = new PackInfoAsset(packName, packIcon, packAuthor, packDescription);
        #endregion

        #region Prepare Menu
        //Canvas
        canvas = new GameObject();
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<GraphicRaycaster>();
        CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();

        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);

        //Camera
        GameObject cam = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/pref_Camera_Main.prefab");
        Object.Instantiate(cam);

        //Menus
        GameObject selectionMenus = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_Selection.prefab");
        selectionMenu = Object.Instantiate(selectionMenus, canvas.transform).GetComponent<AssetSelectionOverseer>();
        #endregion
    }

    [TearDown]
    public void Teardown()
    {
        LibraryOverseer.Instance.RemovePack("New Pack", "NO_AUTHOR");
    }

    [UnityTest]
    public IEnumerator menu_correctly_sets_up_grid_pack_selection()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, selectionMenu.gameObject.activeSelf);
        Assert.AreEqual(true, selectionMenu.GridMenu.activeSelf);
        Assert.AreEqual(false, selectionMenu.ListMenu.activeSelf);
        Assert.AreEqual(1, selectionMenu.AssetCount);
    }

    [UnityTest]
    public IEnumerator open_same_selection_menu_after_the_last_one_was_loaded()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.5f);
        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.25f);

        Assert.AreEqual(1, selectionMenu.AssetCount);

    }

    [UnityTest]
    public IEnumerator reload_selection_menu_with_room_assets()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        selectionMenu.OpenForPacks();
        
        yield return new WaitForSeconds(0.2f);
        LibraryOverseer.Instance.ActivatePackEditor(0);
        EditorOverseer.Instance.CreateNewRoom();
        EditorOverseer.Instance.CreateNewRoom();
        EditorOverseer.Instance.CreateNewRoom();

        yield return new WaitForSeconds(0.1f);
        selectionMenu.OpenForRooms();
        yield return new WaitForSeconds(0.2f);

        Assert.AreEqual(true, selectionMenu.gameObject.activeSelf);
        Assert.AreEqual(true, selectionMenu.ListMenu.activeSelf);
        Assert.AreEqual(false, selectionMenu.GridMenu.activeSelf);
        Assert.AreEqual(3, selectionMenu.AssetCount);
    }

    [UnityTest]
    public IEnumerator menu_correctly_sets_up_room_selection_list_menu()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        LibraryOverseer.Instance.ActivatePackEditor(0);
        EditorOverseer.Instance.CreateNewRoom();
        EditorOverseer.Instance.CreateNewRoom();

        yield return new WaitForSeconds(0.1f);
        selectionMenu.OpenForRooms();
        yield return new WaitForSeconds(0.2f);

        Assert.AreEqual(true, selectionMenu.gameObject.activeSelf);
        Assert.AreEqual(true, selectionMenu.ListMenu.activeSelf);
        Assert.AreEqual(false, selectionMenu.GridMenu.activeSelf);
        Assert.AreEqual(2, selectionMenu.AssetCount);
    }

    [UnityTest]
    public IEnumerator create_new_pack()
    {
        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.1f);
        GASButtonActions.CreatePack();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1, selectionMenu.AssetCount);
    }

    [UnityTest]
    public IEnumerator delete_pack()
    {
        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.1f);
        GASButtonActions.CreatePack();
        yield return new WaitForSeconds(0.3f);
        GASButtonActions.RemovePack(0);

        Assert.AreEqual(0, selectionMenu.AssetCount);
    }

}
