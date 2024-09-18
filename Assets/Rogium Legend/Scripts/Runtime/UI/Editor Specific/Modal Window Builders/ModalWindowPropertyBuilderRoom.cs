using System;
using Rogium.Core;
using Rogium.Editors.Rooms;
using UnityEngine;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Room Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderRoom : ModalWindowPropertyBuilderBase
    {
        private readonly RoomEditorOverseer roomEditor = RoomEditorOverseer.Instance;
        private readonly RoomSettingsBuilder roomBuilder = new();

        public override void OpenForCreate(Action whenConfirm = null) => OpenWindow(new RoomAsset.Builder().Build() , () => CreateAsset(whenConfirm), "Creating new room", AssetModificationType.Create);

        public override void OpenForUpdate(Action whenConfirm = null) => OpenWindow(new RoomAsset.Builder().AsCopy(roomEditor.CurrentAsset).Build(), () => UpdateAsset(whenConfirm), $"Editing {roomEditor.CurrentAsset.Title}", AssetModificationType.Update);

        public override void OpenForClone(Action whenConfirm = null) => OpenWindow(new RoomAsset.Builder().AsClone(roomEditor.CurrentAsset).Build(), () => CloneAsset(whenConfirm), $"Creating a clone of {roomEditor.CurrentAsset.Title}", AssetModificationType.Clone);
        
        private void OpenWindow(RoomAsset asset, Action onConfirm, string headerText, AssetModificationType modification)
        {
            asset.UpdateTitle(GetTitleByModificationType(asset, modification));
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", asset.Title, col1, asset.UpdateTitle);
            roomBuilder.BuildEssentials(col1, asset, false);
            b.BuildPlainText("Created by", asset.Author, col1);
            b.BuildPlainText("Created on", asset.CreationDate.ToString(), col1);
            
            editedAssetBase = asset;
        }

        protected override void CreateAsset(Action whenConfirm)
        {
            editor.CreateNewRoom((RoomAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }

        protected override void UpdateAsset(Action whenConfirm)
        {
            roomEditor.UpdateAsset((RoomAsset)editedAssetBase);
            roomEditor.CompleteEditing();
            whenConfirm?.Invoke();
        }
        
        protected override void CloneAsset(Action whenConfirm)
        {
            editor.CreateNewRoom((RoomAsset)editedAssetBase);
            whenConfirm?.Invoke();
        }
    }
}