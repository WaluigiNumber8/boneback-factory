using System;
using Rogium.Core;
using Rogium.Editors.Core;
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
        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new PackAsset.Builder().Build(), () => CreateAsset(whenConfirm), "Creating a new pack", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new PackAsset.Builder().AsCopy(editor.CurrentPack).Build(), () => UpdateAsset(whenConfirm), $"Editing {editor.CurrentPack.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new PackAsset.Builder().AsClone(editor.CurrentPack).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {editor.CurrentPack.Title}", AssetModificationType.Clone);
        
        /// <summary>
        /// Opens a Modal Window as a Pack Properties Window.
        /// <param name="asset">The PackInfo to edit.</param>
        /// <param name="onConfirm">What happens when the 'Confirm' button is pressed.</param>
        /// </summary>
        private void OpenWindow(PackAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            OpenForColumns2(headerText, onConfirm, out Transform col1, out Transform col2);
            
            bool isDisabled = !editor.CurrentPack?.ContainsAnySprites ?? true;
            
            b.BuildInputField("Name", GetTitleByModificationType(asset, modification), col1, asset.UpdateTitle, false, true);
            b.BuildInputFieldArea("Description", asset.Description, col1, asset.UpdateDescription);
            b.BuildAssetField("", AssetType.Sprite, asset, col2, a => editedAssetBase.UpdateIcon(a), null, isDisabled);
            b.BuildPlainText("Created by", asset.Author, col2);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col2);

            editedAssetBase = asset;
        }

        /// <summary>
        /// Builds a pack and saves it to the library.
        /// </summary>
        protected override void CreateAsset(Action whenConfirm)
        {
            ExternalLibraryOverseer.Instance.CreateAndAddPack(editedAssetBase);
            whenConfirm?.Invoke();
        }

        /// <summary>
        /// Updates a given pack.
        /// </summary>
        protected override void UpdateAsset(Action whenConfirm)
        {
            PackEditorOverseer.Instance.UpdateAsset(editedAssetBase);
            editor.CompleteEditing();
            whenConfirm?.Invoke();
        }

        protected override void CloneAsset(Action whenConfirm)
        {
            ExternalLibraryOverseer.Instance.CreateAndAddPack(editedAssetBase);
            whenConfirm?.Invoke();
        }
    }
}