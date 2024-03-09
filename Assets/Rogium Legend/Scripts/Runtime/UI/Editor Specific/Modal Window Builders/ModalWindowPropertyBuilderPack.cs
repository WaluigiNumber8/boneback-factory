using System;
using RedRats.UI.ModalWindows;
using Rogium.Core;
using Rogium.Editors.Packs;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Pack Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderPack : ModalWindowPropertyBuilder
    {
        private new PackAsset editedAssetBase;
        public override void OpenForCreate()
        {
            OpenWindow(new PackAsset(), CreateAsset, "Creating a new pack");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new PackAsset(editor.CurrentPack), UpdateAsset, $"Editing {editor.CurrentPack.Title}");
        }

        /// <summary>
        /// Opens a Modal Window as a Pack Properties Window.
        /// <param name="currentPackInfo">The PackInfo to edit.</param>
        /// <param name="onConfirmButton">What happens when the 'Confirm' button is pressed.</param>
        /// </summary>
        private void OpenWindow(PackAsset currentPackInfo, Action onConfirmButton, string headerText)
        {
            bool isDisabled = !editor.CurrentPack?.ContainsAnySprites ?? true;
            
            b.BuildInputField("Name", currentPackInfo.Title, windowColumn1, currentPackInfo.UpdateTitle, false, true);
            b.BuildInputFieldArea("Description", currentPackInfo.Description, windowColumn1, currentPackInfo.UpdateDescription);
            b.BuildAssetField("", AssetType.Sprite, currentPackInfo, windowColumn2, a => editedAssetBase.UpdateIcon(a), isDisabled);
            b.BuildPlainText("Created by", currentPackInfo.Author, windowColumn2);
            b.BuildPlainText("Created on", currentPackInfo.CreationDate.ToString(), windowColumn2);

            editedAssetBase = currentPackInfo;
            Open( new PropertyWindowInfo(headerText, PropertyLayoutType.Columns2, "Done", "Cancel", onConfirmButton));
        }

        /// <summary>
        /// Builds a pack and saves it to the library.
        /// </summary>
        protected override void CreateAsset()
        {
            lib.CreateAndAddPack(editedAssetBase);
            selectionMenu.OpenForPacks();
        }

        /// <summary>
        /// Updates a given pack.
        /// </summary>
        protected override void UpdateAsset()
        {
            PackEditorOverseer.Instance.UpdateAsset(editedAssetBase);
            editor.CompleteEditing();
            selectionMenu.OpenForPacks();
        }
    }
}