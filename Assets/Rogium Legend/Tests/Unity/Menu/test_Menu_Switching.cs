using System.Collections;
using RedRats.UI.MenuSwitching;
using NUnit.Framework;
using Rogium.UserInterface;
using UnityEngine;
using UnityEngine.TestTools;
using UnityEngine.UI;

public class GeneralMenu
{
    private GameObject canvas;

    [SetUp]
    public void Setup()
    {
        canvas = new GameObject();
        canvas.AddComponent<Canvas>();
        canvas.AddComponent<GraphicRaycaster>();
        CanvasScaler scaler = canvas.AddComponent<CanvasScaler>();

        scaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        scaler.referenceResolution = new Vector2(1920, 1080);
    }

    [UnityTest]
    public IEnumerator switch_from_one_menu_to_the_next()
    {
        GameObject menu1 = new GameObject("Main Menu");
        menu1.transform.parent = canvas.transform;
        menu1.AddComponent<MenuObject>().MenuType = MenuType.MainMenu;

        GameObject menu2 = new GameObject("Options Menu");
        menu2.transform.parent = canvas.transform;
        menu2.AddComponent<MenuObject>().MenuType = MenuType.OptionsMenu;

        GameObject menu3 = new GameObject("Campaign Editor");
        menu3.transform.parent = canvas.transform;
        menu3.AddComponent<MenuObject>().MenuType = MenuType.CampaignEditor;

        canvas.AddComponent<MenuSwitcher>();

        yield return new WaitForSeconds(0.3f);

        MenuSwitcher.GetInstance().SwitchTo(MenuType.OptionsMenu);

        Assert.AreEqual(false, menu1.activeSelf);
        Assert.AreEqual(true, menu2.activeSelf);
        Assert.AreEqual(false, menu3.activeSelf);
    }

    [UnityTest]
    public IEnumerator allowed_only_1_menu_type_per_object()
    {
        GameObject menu1 = new GameObject("Main Menu");
        menu1.transform.parent = canvas.transform;
        menu1.AddComponent<MenuObject>().MenuType = MenuType.MainMenu;

        GameObject menu2 = new GameObject("Options Menu");
        menu2.transform.parent = canvas.transform;
        menu2.AddComponent<MenuObject>().MenuType = MenuType.OptionsMenu;

        GameObject menu3 = new GameObject("Options Menu Again");
        menu3.transform.parent = canvas.transform;
        menu3.AddComponent<MenuObject>().MenuType = MenuType.OptionsMenu;

        yield return new WaitForSeconds(0.3f);

        try
        {
            canvas.AddComponent<MenuSwitcher>();
            Assert.Fail();
        }
        catch (System.Exception)
        {}
    }

    [UnityTest]
    public IEnumerator properly_finds_all_menus_on_awake()
    {
        GameObject mainMenus = new GameObject("Main Menus");
        mainMenus.transform.parent = canvas.transform;
        GameObject editorMenus = new GameObject("Editor Menus");
        editorMenus.transform.parent = canvas.transform;

        GameObject menu1 = new GameObject("Main Menu");
        menu1.transform.parent = mainMenus.transform;
        menu1.AddComponent<MenuObject>().MenuType = MenuType.MainMenu;

        GameObject menu2 = new GameObject("Options Menu");
        menu2.transform.parent = mainMenus.transform;
        menu2.AddComponent<MenuObject>().MenuType = MenuType.OptionsMenu;

        GameObject menu3 = new GameObject("Campaign Editor");
        menu3.transform.parent = editorMenus.transform;
        menu3.AddComponent<MenuObject>().MenuType = MenuType.CampaignEditor;

        MenuSwitcher switcher = canvas.AddComponent<MenuSwitcher>();

        yield return new WaitForSeconds(0.3f);

        Assert.AreEqual(3, switcher.GetAmountOfMenus);
    }

    [UnityTest]
    public IEnumerator properly_activate_default_menu()
    {
        GameObject mainMenus = new GameObject("Main Menus");
        mainMenus.transform.parent = canvas.transform;
        GameObject editorMenus = new GameObject("Editor Menus");
        editorMenus.transform.parent = canvas.transform;

        GameObject menu1 = new GameObject("Main Menu");
        menu1.transform.parent = mainMenus.transform;
        menu1.AddComponent<MenuObject>().MenuType = MenuType.MainMenu;

        GameObject menu2 = new GameObject("Options Menu");
        menu2.transform.parent = mainMenus.transform;
        menu2.AddComponent<MenuObject>().MenuType = MenuType.OptionsMenu;

        GameObject menu3 = new GameObject("Campaign Editor");
        menu3.transform.parent = editorMenus.transform;
        menu3.AddComponent<MenuObject>().MenuType = MenuType.CampaignEditor;

        canvas.AddComponent<MenuSwitcher>();

        yield return new WaitForSeconds(0.3f);

        Assert.AreEqual(true, menu1.activeSelf);
        Assert.AreEqual(false, menu2.activeSelf);
        Assert.AreEqual(false, menu3.activeSelf);
    }
}
