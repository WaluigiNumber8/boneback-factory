using System;
using RedRats.UI;
using Rogium.Core;
using Rogium.Editors.Packs;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Pack Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderPack : ModalWindowPropertyBuilder
    {
        public override void OpenForCreate()
        {
            OpenWindow(new PackInfoAsset(), CreateAsset, "Creating a new pack");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new PackInfoAsset(editor.CurrentPack.PackInfo), UpdateAsset, $"Editing {editor.CurrentPack.Title}");
        }

        /// <summary>
        /// Opens a Modal Window as a Pack Properties Window.
        /// <param name="currentPackInfo">The PackInfo to edit.</param>
        /// <param name="onConfirmButton">What happens when the 'Confirm' button is pressed.</param>
        /// </summary>
        private void OpenWindow(PackInfoAsset currentPackInfo, Action onConfirmButton, string headerText)
        {
            bool isDisabled = !editor.CurrentPack?.ContainsAnySprites ?? true;
            
            b.BuildInputField("Name", currentPackInfo.Title, window.FirstColumnContent, currentPackInfo.UpdateTitle);
            b.BuildInputFieldArea("Description", currentPackInfo.Description, window.FirstColumnContent, currentPackInfo.UpdateDescription);
            b.BuildAssetField("", AssetType.Sprite, currentPackInfo, window.SecondColumnContent, a => editedAssetBase.UpdateIcon(a?.Icon), isDisabled, ThemeType.Pink);
            b.BuildPlainText("Created by", currentPackInfo.Author, window.SecondColumnContent);
            b.BuildPlainText("Created on", currentPackInfo.CreationDate.ToString(), window.SecondColumnContent);

            editedAssetBase = currentPackInfo;
            window.OpenAsPropertiesColumn2(headerText, ThemeType.Blue, "Done", "Cancel", onConfirmButton, true);
        }

        /// <summary>
        /// Builds a pack and saves it to the library.
        /// </summary>
        protected override void CreateAsset()
        {
            lib.CreateAndAddPack((PackInfoAsset)editedAssetBase);
            selectionMenu.OpenForPacks();
        }

        /// <summary>
        /// Updates a given pack.
        /// </summary>
        protected override void UpdateAsset()
        {
            editor.CurrentPack.UpdatePackInfo((PackInfoAsset)editedAssetBase);
            editor.CompleteEditing();
            selectionMenu.OpenForPacks();
        }
    }
}