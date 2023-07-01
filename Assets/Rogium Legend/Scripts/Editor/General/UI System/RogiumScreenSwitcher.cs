using System;
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
        [ResponsiveButtonGroup("Main"), Button, GUIColor(1f, 0.5f, 0.5f)] public void MainMenu() => Select(editorObjects.mainMenuUI);
        [ResponsiveButtonGroup("Main"), Button, GUIColor(1f, 0.5f, 0.5f)] public void OptionsMenu() => Select(editorObjects.optionsMenuUI);
        [ResponsiveButtonGroup("Main"), Button, GUIColor(1f, 0.5f, 0.5f)] public void Changelog() => Select(editorObjects.changelogUI);
        [OnInspectorGUI] private void S2() => GUILayout.Space(8);
        
        [Title("Game Menus")]
        [OnInspectorGUI] private void S3() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Game"), Button, GUIColor(1f, 0.45f, 0.15f)] public void CampaignSelection() => Select(editorObjects.campaignSelectionMenuUI);
        [ResponsiveButtonGroup("Game"), Button, GUIColor(1f, 0.45f, 0.15f)] public void CampaignEditor() => Select(editorObjects.campaignEditorUI);
        [OnInspectorGUI] private void S4() => GUILayout.Space(8);
        
        [Title("Editor Menus")]
        [OnInspectorGUI] private void S5() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Editor", UniformLayout = true), Button, GUIColor(0.4f, 0.4f, 1f)] public void SelectionMenu() => Select(editorObjects.selectionMenuUI);
        [ResponsiveButtonGroup("Editor"), Button, GUIColor(0.4f, 0.4f, 1f)] public void PaletteEditor() => Select(editorObjects.paletteEditorUI);
        [ResponsiveButtonGroup("Editor"), Button, GUIColor(0.4f, 0.4f, 1f)] public void SpriteEditor() => Select(editorObjects.spriteEditorUI);
        [ResponsiveButtonGroup("Editor"), Button, GUIColor(0.4f, 0.4f, 1f)] public void PropertyEditor() => Select(editorObjects.propertyEditorUI);
        [ResponsiveButtonGroup("Editor"), Button, GUIColor(0.4f, 0.4f, 1f)] public void RoomEditor() => Select(editorObjects.roomEditorUI);
        
        [HideInInspector] public MenuInfo editorObjects;

        private void Select(GameObject menu)
        {
            DeselectAll();
            menu.SetActive(true);
            Selection.activeGameObject = (menu.transform.childCount <= 0) ? menu : menu.transform.GetChild(0).gameObject;
        }
        
        private void DeselectAll()
        {
            editorObjects.mainMenuUI.SetActive(false);
            editorObjects.optionsMenuUI.SetActive(false);
            editorObjects.changelogUI.SetActive(false);
            
            editorObjects.campaignSelectionMenuUI.SetActive(false);
            editorObjects.campaignEditorUI.SetActive(false);
            
            editorObjects.selectionMenuUI.SetActive(false);
            editorObjects.paletteEditorUI.SetActive(false);
            editorObjects.spriteEditorUI.SetActive(false);
            editorObjects.propertyEditorUI.SetActive(false);
            editorObjects.roomEditorUI.SetActive(false);
        }
        
        [Serializable]
        public struct MenuInfo
        {
            public GameObject mainMenuUI;
            public GameObject optionsMenuUI;
            public GameObject changelogUI;
            [Space]
            public GameObject campaignSelectionMenuUI;
            public GameObject campaignEditorUI;
            [Space]
            
            public GameObject selectionMenuUI;
            public GameObject paletteEditorUI;
            public GameObject spriteEditorUI;
            public GameObject propertyEditorUI;
            public GameObject roomEditorUI;
        }
        
    }
}