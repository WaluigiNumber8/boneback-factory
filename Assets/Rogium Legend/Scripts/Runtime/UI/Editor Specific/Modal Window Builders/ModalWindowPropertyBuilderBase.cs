using System;
using RedRats.Core;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Systems.GASExtension;
using Rogium.UserInterface.Editors.AssetSelection;
using Rogium.UserInterface.Interactables.Properties;
using Rogium.UserInterface.ModalWindows;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Base for all Modal Window Constructors.
    /// </summary>
    public abstract class ModalWindowPropertyBuilderBase
    {
        private const string ModalWindowKey = "PropertyBuilder";
        
        protected readonly UIPropertyBuilder b;
        protected readonly PackEditorOverseer editor;
        protected readonly ExternalLibraryOverseer lib;
        protected readonly AssetSelectionMenuOverseerMono selectionMenu;
        protected AssetBase editedAssetBase;

        private readonly ModalWindowBuilder wb;

        protected ModalWindowPropertyBuilderBase()
        {
            b = UIPropertyBuilder.GetInstance();
            lib = ExternalLibraryOverseer.Instance;
            editor = PackEditorOverseer.Instance;
            selectionMenu = AssetSelectionMenuOverseerMono.GetInstance();
            
            wb = ModalWindowBuilder.GetInstance();
        }

        protected void OpenForColumns1(string headerText, Action onConfirm, out Transform column1)
        {
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Columns1)
                .WithHeaderText(headerText)
                .WithAcceptButton("Done", onConfirm)
                .WithDenyButton("Cancel")
                .Build();
            wb.OpenWindow(data, ModalWindowKey, out column1);
            column1.KillChildren();
        }
        
        protected void OpenForColumns2(string headerText, Action onConfirm, out Transform column1, out Transform column2)
        {
            ModalWindowData data = new ModalWindowData.Builder()
                .WithLayout(ModalWindowLayoutType.Columns2)
                .WithHeaderText(headerText)
                .WithAcceptButton("Done", onConfirm)
                .WithDenyButton("Cancel")
                .Build();
            wb.OpenWindow(data, ModalWindowKey, out column1, out column2);
            column1.KillChildren();
            column2.KillChildren();
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