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

        [Title("Menus")] 
        [OnInspectorGUI] private void S1() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), GUIColor(1f, 0.65f, 0.4f)] public void MainMenu() => Select(mainMenuUI, menuBackground);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), GUIColor(1f, 0.65f, 0.4f)] public void OptionsMenu() => Select(optionsMenuUI, menuBackground);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), GUIColor(1f, 0.65f, 0.4f)] public void Changelog() => Select(changelogUI, menuBackground);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), GUIColor(1f, 0.5f, 0.5f)] public void CampaignSelection() => Select(campaignSelectionMenuUI, gameBackground);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), GUIColor(1f, 0.5f, 0.5f)] public void CampaignEditor() => Select(campaignEditorUI, gameBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.45f, 0.45f, 1f)] public void SelectionMenu() => Select(selectionMenuUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.45f, 0.45f, 1f)] public void PaletteEditor() => Select(paletteEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.45f, 0.45f, 1f)] public void SpriteEditor() => Select(spriteEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.45f, 0.45f, 1f)] public void PropertyEditor() => Select(propertyEditorUI, editorBackground);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), GUIColor(0.45f, 0.45f, 1f)] public void RoomEditor() => Select(roomEditorUI, editorBackground);
        [OnInspectorGUI] private void S2() => GUILayout.Space(8);
        [Title("Settings")]
        [OnInspectorGUI] private void S3() => GUILayout.Space(0);
        [SerializeField, FoldoutGroup("Menu Objects", order:5), GUIColor(1f, 0.65f, 0.4f)] public GameObject menuBackground;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(1f, 0.65f, 0.4f)] public GameObject mainMenuUI;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(1f, 0.65f, 0.4f)] public GameObject optionsMenuUI;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(1f, 0.65f, 0.4f)] public GameObject changelogUI;
        [Space]
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(1f, 0.5f, 0.5f)] public GameObject gameBackground;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(1f, 0.5f, 0.5f)] public GameObject campaignSelectionMenuUI;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(1f, 0.5f, 0.5f)] public GameObject campaignEditorUI;
        [Space]
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(0.45f, 0.45f, 1f)] public GameObject editorBackground;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(0.45f, 0.45f, 1f)] public GameObject selectionMenuUI;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(0.45f, 0.45f, 1f)] public GameObject paletteEditorUI;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(0.45f, 0.45f, 1f)] public GameObject spriteEditorUI;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(0.45f, 0.45f, 1f)] public GameObject propertyEditorUI;
        [SerializeField, FoldoutGroup("Menu Objects"), GUIColor(0.45f, 0.45f, 1f)] public GameObject roomEditorUI;

        private GameObject lastObject;

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
            background.SetActive(true);
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