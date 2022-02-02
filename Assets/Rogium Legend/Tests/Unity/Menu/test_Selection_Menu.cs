using NUnit.Framework;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.UserInterface.AssetSelection;
using System.Collections;
using UnityEditor;
using UnityEngine;
using UnityEngine.TestTools;

public class test_Selection_Menu
{
    private GameObject canvas;
    private PackInfoAsset packInfo;
    private AssetSelectionOverseer assetSelection;
    private AssetSelectionOverseerMono assetSelectionMono;

    [SetUp]
    public void Setup()
    {
        #region Prepare Test Pack
        string packTitle = EditorDefaults.PackTitle;
        string packDescription = EditorDefaults.PackDescription;
        string packAuthor = EditorDefaults.Author;
        Sprite packIcon = EditorDefaults.PackIcon;

        packInfo = new PackInfoAsset(packTitle, packIcon, packAuthor, packDescription);
        #endregion

        #region Prepare Menu
        //Canvas
        TestBuilder.SetupCanvas(out canvas);

        //Camera
        GameObject cam = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/Global/pref_Camera_Main.prefab");
        Object.Instantiate(cam);

        //Menus
        GameObject selectionMenus = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/Rogium Legend/Prefabs/UI/Menus/pref_Menu_Selection.prefab");
        assetSelectionMono = Object.Instantiate(selectionMenus, canvas.transform).GetComponent<AssetSelectionOverseerMono>();
        #endregion
        
        assetSelection = AssetSelectionOverseer.Instance;
    }

    [TearDown]
    public void Teardown()
    {
        LibraryOverseer.Instance.DeletePack("New Pack", "NO_AUTHOR");
    }

    [UnityTest]
    public IEnumerator menu_correctly_sets_up_grid_pack_selection()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        assetSelectionMono.OpenForPacks();
        yield return new WaitForSeconds(0.1f);
        Assert.AreEqual(true, assetSelectionMono.gameObject.activeSelf);
        Assert.AreEqual(true, assetSelectionMono.GridMenu.gameObject.activeSelf);
        Assert.AreEqual(false, assetSelectionMono.ListMenu.gameObject.activeSelf);
        Assert.AreEqual(1, assetSelection.AssetCount);
    }

    [UnityTest]
    public IEnumerator open_same_selection_menu_after_the_last_one_was_loaded()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        assetSelectionMono.OpenForPacks();
        yield return new WaitForSeconds(0.5f);
        assetSelectionMono.OpenForPacks();
        yield return new WaitForSeconds(0.25f);

        Assert.AreEqual(1, assetSelection.AssetCount);

    }

    [UnityTest]
    public IEnumerator reload_selection_menu_with_room_assets()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        assetSelectionMono.OpenForPacks();
        
        yield return new WaitForSeconds(0.2f);
        LibraryOverseer.Instance.ActivatePackEditor(0);
        PackEditorOverseer.Instance.CreateNewRoom();
        PackEditorOverseer.Instance.CreateNewRoom();
        PackEditorOverseer.Instance.CreateNewRoom();

        yield return new WaitForSeconds(0.1f);
        assetSelectionMono.OpenForRooms();
        yield return new WaitForSeconds(0.2f);

        Assert.AreEqual(true, assetSelectionMono.gameObject.activeSelf);
        Assert.AreEqual(true, assetSelectionMono.ListMenu.gameObject.activeSelf);
        Assert.AreEqual(false, assetSelectionMono.GridMenu.gameObject.activeSelf);
        Assert.AreEqual(3, assetSelection.AssetCount);
    }

    [UnityTest]
    public IEnumerator menu_correctly_sets_up_room_selection_list_menu()
    {
        LibraryOverseer.Instance.CreateAndAddPack(packInfo);

        LibraryOverseer.Instance.ActivatePackEditor(0);
        PackEditorOverseer.Instance.CreateNewRoom();
        PackEditorOverseer.Instance.CreateNewRoom();

        yield return new WaitForSeconds(0.1f);
        assetSelectionMono.OpenForRooms();
        yield return new WaitForSeconds(0.2f);

        Assert.AreEqual(true, assetSelectionMono.gameObject.activeSelf);
        Assert.AreEqual(true, assetSelectionMono.ListMenu.gameObject.activeSelf);
        Assert.AreEqual(false, assetSelectionMono.GridMenu.gameObject.activeSelf);
        Assert.AreEqual(2, assetSelection.AssetCount);
    }

    [UnityTest]
    public IEnumerator create_new_pack()
    {
        assetSelectionMono.OpenForPacks();
        yield return new WaitForSeconds(0.1f);
        GASButtonActions.CreatePack();

        yield return new WaitForSeconds(0.1f);

        Assert.AreEqual(1, assetSelection.AssetCount);
    }

    [UnityTest]
    public IEnumerator delete_pack()
    {
        assetSelectionMono.OpenForPacks();
        yield return new WaitForSeconds(0.1f);
        GASButtonActions.CreatePack();
        yield return new WaitForSeconds(0.3f);
        GASButtonActions.DeletePack(0);

        Assert.AreEqual(0, assetSelection.AssetCount);
    }

}
