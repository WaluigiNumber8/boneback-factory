using BoubakProductions.Core;
using Rogium.Editors.Tiles;
using Rogium.Systems.ThemeSystem;
using Rogium.UserInterface.Interactables;
using UnityEngine;
using UnityEngine.UI;

namespace Rogium.Editors.PropertyEditor
{
    /// <summary>
    /// Overseers the Property Editor.
    /// </summary>
    public class PropertyEditorOverseerMono : MonoSingleton<PropertyEditorOverseerMono>
    {
        [SerializeField] private Transform importantContent;
        [SerializeField] private Transform propertyContent;
        [SerializeField] private UIInfo ui;

        private PropertyEditorBuilder builder;

        protected override void Awake()
        {
            base.Awake();
            builder = new PropertyEditorBuilder(importantContent, propertyContent);
        }

        private void OnEnable()
        {
            TileEditorOverseer.Instance.OnAssignAsset += InitTiles;
        }

        private void OnDisable()
        {
            TileEditorOverseer.Instance.OnAssignAsset -= InitTiles;
        }

        
        /// <summary>
        /// Initializes the editor for Tiles.
        /// </summary>
        /// <param name="asset">The Tile Asset to work with.</param>
        private void InitTiles(TileAsset asset)
        {
            Init();

            ui.saveButton.Action = ButtonType.SaveChangesTile;
            ui.cancelButton.Action = ButtonType.CancelChangesTile;
            
            builder.BuildForTiles(asset);
        }

        /// <summary>
        /// Initializes the editor.
        /// </summary>
        private void Init()
        {
            ReloadTheme();
        }

        /// <summary>
        /// Sets graphics from the current theme.
        /// </summary>
        private void ReloadTheme()
        {
            ThemeUpdater.UpdateElement(ui.importantColumnBackground);
            ThemeUpdater.UpdateButtonCard(ui.saveButton);
            ThemeUpdater.UpdateButtonCard(ui.cancelButton);
            ThemeUpdater.UpdateButtonMenu(ui.undoButton);
            ThemeUpdater.UpdateButtonMenu(ui.redoButton);
            ThemeUpdater.UpdateButtonMenu(ui.resetButton);
        }
        
        [System.Serializable]
        public struct UIInfo
        {
            public Image importantColumnBackground;
            public InteractableButton saveButton;
            public InteractableButton cancelButton;
            public InteractableButton undoButton;
            public InteractableButton redoButton;
            public InteractableButton resetButton;
        }
    }
}