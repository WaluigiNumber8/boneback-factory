using RedRats.Editor.UnityEditorExtensions;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEditor.SceneManagement;
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

        [Title("Scenes")]
        [OnInspectorGUI] private void S0() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Scenes"), Button(ButtonSizes.Large)] public void Menu() => SwitchScene(menuScenePath);
        [ResponsiveButtonGroup("Scenes"), Button(ButtonSizes.Large)] public void Game() => SwitchScene(gameScenePath);
        [ResponsiveButtonGroup("Scenes"), Button(ButtonSizes.Medium)] public void Testing() => SwitchScene(testScenePath);
        [ResponsiveButtonGroup("Scenes"), Button(ButtonSizes.Medium)] public void UnitTestingMenu() => SwitchScene(unitTestMenuScenePath);
        [ResponsiveButtonGroup("Scenes"), Button(ButtonSizes.Medium)] public void UnitTestingGameplay() => SwitchScene(unitTestGameplayScenePath);
        
        [Title("Menus")] 
        [OnInspectorGUI] private void S1() => GUILayout.Space(0);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), DisableIf("@" + nameof(mainMenuUI) + "== null"), GUIColor(1f, 0.65f, 0.4f)] public void MainMenu() => Select(mainMenuUI);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), DisableIf("@" + nameof(optionsMenuUI) + "== null"), GUIColor(1f, 0.65f, 0.4f)] public void OptionsMenu() => Select(optionsMenuUI);
        [ResponsiveButtonGroup("Main"), Button(ButtonSizes.Large), DisableIf("@" + nameof(changelogUI) + "== null"), GUIColor(1f, 0.65f, 0.4f)] public void Changelog() => Select(changelogUI);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), DisableIf("@" + nameof(campaignSelectionMenuUI) + "== null"), GUIColor(1f, 0.5f, 0.5f)] public void CampaignSelection() => Select(campaignSelectionMenuUI);
        [ResponsiveButtonGroup("Game"), Button(ButtonSizes.Large), DisableIf("@" + nameof(campaignEditorUI) + "== null"), GUIColor(1f, 0.5f, 0.5f)] public void CampaignEditor() => Select(campaignEditorUI);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(selectionMenuUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void SelectionMenu() => Select(selectionMenuUI);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(paletteEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void PaletteEditor() => Select(paletteEditorUI);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(spriteEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void SpriteEditor() => Select(spriteEditorUI);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(propertyEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void PropertyEditor() => Select(propertyEditorUI);
        [ResponsiveButtonGroup("Editor"), Button(ButtonSizes.Large), DisableIf("@" + nameof(roomEditorUI) + "== null"), GUIColor(0.45f, 0.45f, 1f)] public void RoomEditor() => Select(roomEditorUI);
        [OnInspectorGUI] private void S2() => GUILayout.Space(8);
        [Title("Settings")]
        [OnInspectorGUI] private void S3() => GUILayout.Space(0);
        [SerializeField, FoldoutGroup("Labels", order:5)] public string menuScenePath;
        [SerializeField, FoldoutGroup("Labels")] public string gameScenePath;
        [SerializeField, FoldoutGroup("Labels")] public string testScenePath;
        [SerializeField, FoldoutGroup("Labels")] public string unitTestMenuScenePath;
        [SerializeField, FoldoutGroup("Labels")] public string unitTestGameplayScenePath;
        [Space]
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.65f, 0.4f)] public string mainMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.65f, 0.4f)] public string optionsMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.65f, 0.4f)] public string changelogLabel;
        [Space]
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.5f, 0.5f)] public string campaignSelectionMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(1f, 0.5f, 0.5f)] public string campaignEditorLabel;
        [Space]
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string selectionMenuLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string paletteEditorLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string spriteEditorLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string propertyEditorLabel;
        [SerializeField, FoldoutGroup("Labels"), GUIColor(0.45f, 0.45f, 1f)] public string roomEditorLabel;
        [FoldoutGroup("Labels"), Button(ButtonSizes.Large)] public void ReloadObjects() => DetectObjects();
        [OnInspectorGUI] private void S4() => GUILayout.Space(8);
        [SerializeField, FoldoutGroup("Actual Objects", order:10), ReadOnly] private GameObject mainMenuUI, optionsMenuUI, changelogUI;
        [SerializeField, FoldoutGroup("Actual Objects"), ReadOnly] private GameObject campaignSelectionMenuUI, campaignEditorUI;
        [SerializeField, FoldoutGroup("Actual Objects"), ReadOnly] private GameObject selectionMenuUI, paletteEditorUI, spriteEditorUI, propertyEditorUI, roomEditorUI;

        private GameObject lastObject;

        private void OnDidOpenScene() => DetectObjects();

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
        /// Switches to a scene under path.
        /// </summary>
        /// <param name="path">The path of the scene asset to switch to.</param>
        private void SwitchScene(string path)
        {
            if (!EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo()) return;
            EditorSceneManager.OpenScene(path, OpenSceneMode.Single);
        }

        /// <summary>
        /// Activates a specific menu.
        /// </summary>
        /// <param name="menu">The menu object to activate.</param>
        private void Select(GameObject menu)
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode) throw new System.InvalidOperationException("Cannot switch between menus in playmode.");
            
            DeselectAll();
            menu.SetActive(true);
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
            
            campaignSelectionMenuUI.SetActive(false);
            campaignEditorUI.SetActive(false);
            
            selectionMenuUI.SetActive(false);
            paletteEditorUI.SetActive(false);
            spriteEditorUI.SetActive(false);
            propertyEditorUI.SetActive(false);
            roomEditorUI.SetActive(false);
        }
        
        /// <summary>
        /// Tries to detect menu objects in the scene.
        /// </summary>
        private void DetectObjects()
        {
            mainMenuUI = FindObject(mainMenuLabel);
            optionsMenuUI = FindObject(optionsMenuLabel);
            changelogUI = FindObject(changelogLabel);
            
            campaignSelectionMenuUI = FindObject(campaignSelectionMenuLabel);
            campaignEditorUI = FindObject(campaignEditorLabel);
            
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
        private static GameObject FindObject(string parentName)
        {
            GameObject o = GameObject.Find(parentName);
            if (o == null) return null;
            return (o.transform.childCount > 0) ? (o.transform.GetChild(0).childCount > 0) ? o.transform.GetChild(0).gameObject : o.transform.gameObject : o;
        }
    }
}