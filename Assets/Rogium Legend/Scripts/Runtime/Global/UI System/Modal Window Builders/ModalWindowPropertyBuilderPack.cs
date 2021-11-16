using System;
using BoubakProductions.UI;
using Rogium.Editors.Packs;

namespace Rogium.Global.UISystem.UI
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
            propertyBuilder.BuildInputField("Name", currentPackInfo.Title, window.FirstColumnContent, currentPackInfo.UpdateTitle);
            propertyBuilder.BuildInputFieldArea("Description", currentPackInfo.Description, window.FirstColumnContent, currentPackInfo.UpdateDescription);
            propertyBuilder.BuildSprite("",currentPackInfo.Icon, window.SecondColumnContent);
            propertyBuilder.BuildPlainText("Created by", currentPackInfo.Author, window.SecondColumnContent);
            propertyBuilder.BuildPlainText("Created on", currentPackInfo.CreationDate.ToString(), window.SecondColumnContent);

            editedAssetBase = currentPackInfo;
            window.OpenAsPropertiesColumn2(headerText, ThemeType.Blue, "Done", "Cancel", onConfirmButton, true);
        }

        /// <summary>
        /// Builds a pack and saves it to the library.
        /// </summary>
        protected override void CreateAsset()
        {
            lib.CreateAndAddPack((PackInfoAsset)editedAssetBase);
            selectionMenu.ReopenForPacks();
        }

        /// <summary>
        /// Updates a given pack.
        /// </summary>
        protected override void UpdateAsset()
        {
            editor.CurrentPack.UpdatePackInfo((PackInfoAsset)editedAssetBase);
            editor.CompleteEditing();
            selectionMenu.ReopenForPacks();
        }
    }
}