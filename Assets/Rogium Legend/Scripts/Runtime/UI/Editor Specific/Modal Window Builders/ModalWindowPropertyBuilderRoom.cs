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
        private readonly RoomEditorOverseer roomEditor;
        private readonly RoomSettingsBuilder roomBuilder;

        public ModalWindowPropertyBuilderRoom()
        {
            roomEditor = RoomEditorOverseer.Instance;
            roomBuilder = new RoomSettingsBuilder();
        }

        public override void OpenForCreate() => OpenWindow(new RoomAsset.Builder().Build() , CreateAsset, "Creating new room");

        public override void OpenForUpdate() => OpenWindow(new RoomAsset.Builder().AsCopy(roomEditor.CurrentAsset).Build(), UpdateAsset, $"Editing {roomEditor.CurrentAsset.Title}");

        private void OpenWindow(RoomAsset data, Action onConfirm, string headerText)
        {
            OpenForColumns1(headerText, onConfirm, out Transform col1);
            
            b.BuildInputField("Title", data.Title, col1, data.UpdateTitle);
            roomBuilder.BuildEssentials(col1, data, false);
            b.BuildPlainText("Created by", data.Author, col1);
            b.BuildPlainText("Created on", data.CreationDate.ToString(), col1);
            
            editedAssetBase = data;
        }

        protected override void CreateAsset()
        {
            editor.CreateNewRoom((RoomAsset)editedAssetBase);
            selectionMenu.Open(AssetType.Room);
        }

        protected override void UpdateAsset()
        {
            roomEditor.UpdateAsset((RoomAsset)editedAssetBase);
            roomEditor.CompleteEditing();
            selectionMenu.Open(AssetType.Room);
        }
    }
}