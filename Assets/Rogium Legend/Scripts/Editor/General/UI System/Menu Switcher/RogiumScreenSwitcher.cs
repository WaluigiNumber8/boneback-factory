using System;
using RedRats.Editor.UnityEditorExtensions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rogium.Editor.UI
{
    public class RogiumScreenSwitcher : OdinEditorWindow
    {
        [MenuItem("Tools/Rogium Legend/Menu Screen Switcher")]
        private static void ShowWindow()
        {
            RogiumScreenSwitcher window = GetWindow<RogiumScreenSwitcher>();
            window.titleContent = new GUIContent("Menu Screen Switcher");
            window.Show();
        }

        [Title("Menus")] 
        [OnInspectorGUI] private void S1() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), DisableIf("@" + nameof(mainMenuUI) + "== null"), GUIColor(1f, 0.65f, 0.4f)] public void MainMenu() => Select(mainMenuUI, menuBackground);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), DisableIf("@" + nameof(optionsMenuUI) + "== null"), GUIColor(1f, 0.65f, 0.4f)] public void OptionsMenu() => Select(optionsMenuUI, menuBackground);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), DisableIf("@" + nameof(changelogUI) + "== null"), GUIColor(1f, 0.65f, 0.4f)] public void Changelog() => Select(changelogUI, menuBackground);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), DisableIf("@" + nameof(campaignSelectionMenuUI) + "== null"), GUIColor(1f, 0.5f, 0.5f)] public void CampaignSelection() => Select(campaignSelectionMenuUI, gameBackground);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), DisableIf("@" + nameof(campaignEditorUI) + "== null"), GUIColor(1f, 0.5f, 0.5f)] public void CampaignEditor() => Select(campaignEditorUI, gameBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(selectionMenuUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void SelectionMenu() => Select(selectionMenuUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(paletteEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void PaletteEditor() => Select(paletteEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(spriteEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void SpriteEditor() => Select(spriteEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(propertyEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void PropertyEditor() => Select(propertyEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(roomEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void RoomEditor() => Select(roomEditorUI, editorBackground);
        [OnInspectorGUI] private void S2() => GUILayout.Space(8);
        [Title("Settings")]
        [OnInspectorGUI] private void S3() => GUILayout.Space(0);
        [SerializeField, FoldoutGroup("Labels", order:5), GUIColor(1f, 0.65f, 0.4f)] public string menuBackgroundLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.65f, 0.4f)] public string mainMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.65f, 0.4f)] public string optionsMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.65f, 0.4f)] public string changelogLabel;
        [Space]
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.5f, 0.5f)] public string gameBackgroundLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.5f, 0.5f)] public string campaignSelectionMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.5f, 0.5f)] public string campaignEditorLabel;
        [Space]
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string editorBackgroundLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string selectionMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string paletteEditorLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string spriteEditorLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string propertyEditorLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string roomEditorLabel;
        [FoldoutGroup("Labels"), Button(ButtonSizes.Large)] public void ReloadObjects() => DetectObjects();
        [OnInspectorGUI] private void S4() => GUILayout.Space(8);
        [SerializeField, FoldoutGroup("Actual Objects", order:10), ReadOnly] private GameObject menuBackground, mainMenuUI, optionsMenuUI, changelogUI;
        [SerializeField, FoldoutGroup("Actual Objects"), ReadOnly] private GameObject gameBackground, campaignSelectionMenuUI, campaignEditorUI;
        [SerializeField, FoldoutGroup("Actual Objects"), ReadOnly] private GameObject editorBackground, selectionMenuUI, paletteEditorUI, spriteEditorUI, propertyEditorUI, roomEditorUI;

        private GameObject lastObject;
        
        private void OnBecameVisible() => EditorSceneManager.sceneOpened += DetectObjects;
        private void OnBecameInvisible() => EditorSceneManager.sceneOpened -= DetectObjects;
        protected override void OnEnable()
        {
            base.OnEnable();
            string data = EditorPrefs.GetString("RogiumMenuSwitcher", JsonUtility.ToJson(this));
            JsonUtility.FromJsonOverwrite(data, this);
        }

        protected override void OnDisable()
        {
            string data = JsonUtility.ToJson(this, false);
            EditorPrefs.SetString("RogiumMenuSwitcher", data);
            base.OnDisable();
        }

        /// <summary>
        /// Activates a specific menu.
        /// </summary>
        /// <param name="menu">The menu object to activate.</param>
        /// <param name="background">The background the editor uses.</param>
        private void Select(GameObject menu, GameObject background)
        {
            DeselectAll();
            menu.SetActive(true);
            if (background != null) background.SetActive(true);
            SceneHierarchyController.Select(menu);
            SceneHierarchyController.SetExpanded(menu, true);
            lastObject = menu;
        }
        
        /// <summary>
        /// Disables all menu objects.
        /// </summary>
        private void DeselectAll()
        {
            if (lastObject != null) SceneHierarchyController.SetExpanded(lastObject.transform.parent.gameObject, false);
            
            menuBackground.SetActive(false);
            mainMenuUI.SetActive(false);
            optionsMenuUI.SetActive(false);
            changelogUI.SetActive(false);
            
            gameBackground.SetActive(false);
            campaignSelectionMenuUI.SetActive(false);
            campaignEditorUI.SetActive(false);
            
            editorBackground.SetActive(false);
            selectionMenuUI.SetActive(false);
            paletteEditorUI.SetActive(false);
            spriteEditorUI.SetActive(false);
            propertyEditorUI.SetActive(false);
            roomEditorUI.SetActive(false);
        }

        /// <summary>
        /// Tries to detect menu objects in the scene (override for event).
        /// </summary>
        private void DetectObjects(Scene scene, OpenSceneMode mode) => DetectObjects();
        /// <summary>
        /// Tries to detect menu objects in the scene.
        /// </summary>
        private void DetectObjects()
        {
            menuBackground = FindObject(menuBackgroundLabel);
            mainMenuUI = FindObject(mainMenuLabel);
            optionsMenuUI = FindObject(optionsMenuLabel);
            changelogUI = FindObject(changelogLabel);
            
            gameBackground = FindObject(gameBackgroundLabel);
            campaignSelectionMenuUI = FindObject(campaignSelectionMenuLabel);
            campaignEditorUI = FindObject(campaignEditorLabel);
            
            editorBackground = FindObject(editorBackgroundLabel);
            selectionMenuUI = FindObject(selectionMenuLabel);
            paletteEditorUI = FindObject(paletteEditorLabel);
            spriteEditorUI = FindObject(spriteEditorLabel);
            propertyEditorUI = FindObject(propertyEditorLabel);
            roomEditorUI = FindObject(roomEditorLabel);
        }

        /// <summary>
        /// Tries to find an object with a specific name and return it's first child.
        /// </summary>
        /// <param name="parentName">The name of the parent.</param>
        /// <returns>The child object or if not found, then returns the parent. If no object was found, return NULL.</returns>
        private GameObject FindObject(string parentName)
        {
            GameObject o = GameObject.Find(parentName);
            if (o == null) return null;
            return (o.transform.childCount > 0) ? o.transform.GetChild(0).gameObject : o;
        }
    }
}