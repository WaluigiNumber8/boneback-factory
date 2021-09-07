using System.Collections;
using NUnit.Framework;
using RogiumLegend.Editors.PackData;
using RogiumLegend.Global.MenuSystem.AssetSelection;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class test_Selection_Menu
{
    private GameObject canvas;
    private AssetSelectionOverseer selectionMenu;

    [SetUp]
    public void Setup()
    {
        #region Prepare Test Pack
        string packName = "Test Pack";
        string packDescription = "Created this pack for testing purposes.";
        string packAuthor = "TestAuthor";
        Sprite packIcon = Sprite.Create(new Texture2D(16, 16), new Rect(0, 0, 16, 16), new Vector2(0.5f, 0.5f));

        PackInfoAsset packInfo = new PackInfoAsset(packName, packDescription, packAuthor, packIcon);
        PackAsset pack = new PackAsset(packInfo);

        LibraryOverseer.Instance.Library.Add(pack);
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
        LibraryOverseer.Instance.Library.Remove("Test Pack", "TestAuthor");
    }

    [UnityTest]
    public IEnumerator menu_correctly_sets_up_pack_selection()
    {
        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, selectionMenu.GridMenu.activeSelf);
        Assert.AreEqual(false, selectionMenu.ListMenu.activeSelf);
        Assert.AreEqual(1, selectionMenu.AssetCount);
    }

    [UnityTest]
    public IEnumerator open_same_selection_menu_after_the_last_one_was_loaded()
    {
        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.5f);
        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.25f);

        Assert.AreEqual(1, selectionMenu.AssetCount);

    }

    [UnityTest]
    public IEnumerator reload_selection_menu_with_room_assets()
    {
        selectionMenu.OpenForPacks();
        yield return new WaitForSeconds(0.5f);
        selectionMenu.OpenForRooms();
    }
}
