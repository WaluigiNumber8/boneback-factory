using System;
using RedRats.Editor.UnityEditorExtensions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

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

        [Title("Main Menus")] 
        [OnInspectorGUI] private void S1() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), GUIColor(1f, 0.5f, 0.5f)] public void MainMenu() => Select(mainMenuUI, menuBackground);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), GUIColor(1f, 0.5f, 0.5f)] public void OptionsMenu() => Select(optionsMenuUI, menuBackground);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), GUIColor(1f, 0.5f, 0.5f)] public void Changelog() => Select(changelogUI, menuBackground);
        [OnInspectorGUI] private void S2() => GUILayout.Space(8);
        
        [Title("Game Menus")]
        [OnInspectorGUI] private void S3() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), GUIColor(1f, 0.45f, 0.15f)] public void CampaignSelection() => Select(campaignSelectionMenuUI, gameBackground);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), GUIColor(1f, 0.45f, 0.15f)] public void CampaignEditor() => Select(campaignEditorUI, gameBackground);
        [OnInspectorGUI] private void S4() => GUILayout.Space(8);
        
        [Title("Editor Menus")]
        [OnInspectorGUI] private void S5() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.4f, 0.4f, 1f)] public void SelectionMenu() => Select(selectionMenuUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.4f, 0.4f, 1f)] public void PaletteEditor() => Select(paletteEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.4f, 0.4f, 1f)] public void SpriteEditor() => Select(spriteEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.4f, 0.4f, 1f)] public void PropertyEditor() => Select(propertyEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.4f, 0.4f, 1f)] public void RoomEditor() => Select(roomEditorUI, editorBackground);
        
        [FoldoutGroup("EditorObjects")]public GameObject mainMenuUI;
        [FoldoutGroup("EditorObjects")]public GameObject optionsMenuUI;
        [FoldoutGroup("EditorObjects")]public GameObject changelogUI;
        [FoldoutGroup("EditorObjects")]public GameObject menuBackground;
        [Space]
        [FoldoutGroup("EditorObjects")]public GameObject campaignSelectionMenuUI;
        [FoldoutGroup("EditorObjects")]public GameObject campaignEditorUI;
        [FoldoutGroup("EditorObjects")]public GameObject gameBackground;
        [Space]
        [FoldoutGroup("EditorObjects")]public GameObject selectionMenuUI;
        [FoldoutGroup("EditorObjects")]public GameObject paletteEditorUI;
        [FoldoutGroup("EditorObjects")]public GameObject spriteEditorUI;
        [FoldoutGroup("EditorObjects")]public GameObject propertyEditorUI;
        [FoldoutGroup("EditorObjects")]public GameObject roomEditorUI;
        [FoldoutGroup("EditorObjects")]public GameObject editorBackground;

        private GameObject lastObject;

        protected override void OnEnable()
        {
            string data = EditorPrefs.GetString("RogiumMenuSwitcher", JsonUtility.ToJson(this));
            JsonUtility.FromJsonOverwrite(data, this);
            base.OnEnable();
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            string data = JsonUtility.ToJson(this, false);
            EditorPrefs.SetString("RogiumMenuSwitcher", data);
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
            background.SetActive(true);
            SceneHierarchyController.Select((menu.transform.childCount <= 0) ? menu : menu.transform.GetChild(0).gameObject);
            lastObject = menu;
        }
        
        /// <summary>
        /// Disables all menu objects.
        /// </summary>
        private void DeselectAll()
        {
            if (lastObject != null) SceneHierarchyController.SetExpanded(lastObject.transform.parent.gameObject, false);
            
            mainMenuUI.SetActive(false);
            optionsMenuUI.SetActive(false);
            changelogUI.SetActive(false);
            menuBackground.SetActive(false);
            
            campaignSelectionMenuUI.SetActive(false);
            campaignEditorUI.SetActive(false);
            gameBackground.SetActive(false);
            
            selectionMenuUI.SetActive(false);
            paletteEditorUI.SetActive(false);
            spriteEditorUI.SetActive(false);
            propertyEditorUI.SetActive(false);
            roomEditorUI.SetActive(false);
            editorBackground.SetActive(false);
        }
    }
}