using RedRats.UI.ModalWindows;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.UserInterface.Editors.AssetSelection;
using Rogium.UserInterface.Interactables.Properties;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Base for all Modal Window Constructors.
    /// </summary>
    public abstract class ModalWindowPropertyBuilder
    {
        private const string ModalWindowKey = "PropertyBuilder";
        
        protected readonly UIPropertyBuilder b;
        protected readonly PackEditorOverseer editor;
        protected readonly ExternalLibraryOverseer lib;
        protected readonly AssetSelectionMenu selectionMenu;
        protected AssetBase editedAssetBase;

        protected readonly Transform windowColumn1;
        protected readonly Transform windowColumn2;
        private readonly ModalWindowOverseerMono windowOverseer;

        protected ModalWindowPropertyBuilder()
        {
            b = UIPropertyBuilder.GetInstance();
            lib = ExternalLibraryOverseer.Instance;
            editor = PackEditorOverseer.Instance;
            selectionMenu = GASContainer.GetInstance().AssetSelection;
            
            windowOverseer = ModalWindowOverseerMono.GetInstance();
            windowColumn1 = windowOverseer.GetColumn1(ModalWindowKey);
            windowColumn2 = windowOverseer.GetColumn2(ModalWindowKey);
        }

        /// <summary>
        /// Opens the assigned modal window.
        /// </summary>
        /// <param name="data">Data to use for the window.</param>
        protected void Open(ModalWindowInfoBase data)
        {
            windowOverseer.OpenWindow(data, ModalWindowKey);
        }

        /// <summary>
        /// Opens a Modal Window as a Creation Window.
        /// </summary>
        public abstract void OpenForCreate();

        /// <summary>
        /// Opens a Modal Window as an Edit Window.
        /// </summary>
        public abstract void OpenForUpdate();

        /// <summary>
        /// What happens, when the confirm button is pressed in the creation window variation.
        /// </summary>
        protected abstract void CreateAsset();
        
        /// <summary>
        /// What happens, when the confirm button is pressed in the update window variation.
        /// </summary>
        protected abstract void UpdateAsset();
    }
}