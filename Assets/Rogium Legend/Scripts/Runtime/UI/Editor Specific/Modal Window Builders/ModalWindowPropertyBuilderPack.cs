using System;
using Rogium.Core;
using Rogium.Editors.Packs;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Pack Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderPack : ModalWindowPropertyBuilderBase
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
        /// <param name="onConfirm">What happens when the 'Confirm' button is pressed.</param>
        /// </summary>
        private void OpenWindow(PackAsset currentPackInfo, Action onConfirm, string headerText)
        {
            OpenForColumns2(headerText, onConfirm, out Transform col1, out Transform col2);
            
            bool isDisabled = !editor.CurrentPack?.ContainsAnySprites ?? true;
            
            b.BuildInputField("Name", currentPackInfo.Title, col1, currentPackInfo.UpdateTitle, false, true);
            b.BuildInputFieldArea("Description", currentPackInfo.Description, col1, currentPackInfo.UpdateDescription);
            b.BuildAssetField("", AssetType.Sprite, currentPackInfo, col2, a => editedAssetBase.UpdateIcon(a), null, isDisabled);
            b.BuildPlainText("Created by", currentPackInfo.Author, col2);
            b.BuildPlainText("Created on", currentPackInfo.CreationDate.ToString(), col2);

            editedAssetBase = currentPackInfo;
        }

        /// <summary>
        /// Builds a pack and saves it to the library.
        /// </summary>
        protected override void CreateAsset()
        {
            lib.CreateAndAddPack(editedAssetBase);
            selectionMenu.Open(AssetType.Pack);
        }

        /// <summary>
        /// Updates a given pack.
        /// </summary>
        protected override void UpdateAsset()
        {
            PackEditorOverseer.Instance.UpdateAsset(editedAssetBase);
            editor.CompleteEditing();
            selectionMenu.Open(AssetType.Pack);
        }
    }
}