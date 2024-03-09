using System;
using RedRats.Core;
using RedRats.UI.ModalWindows;
using Rogium.Editors.Rooms;

namespace Rogium.UserInterface.Editors.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Room Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderRoom : ModalWindowPropertyBuilder
    {
        private readonly RoomEditorOverseer roomEditor;
        private readonly RoomSettingsBuilder roomBuilder;

        public ModalWindowPropertyBuilderRoom()
        {
            roomEditor = RoomEditorOverseer.Instance;
            roomBuilder = new RoomSettingsBuilder();
        }

        public override void OpenForCreate()
        {
            OpenWindow(new RoomAsset(), CreateAsset, "Creating new room");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(roomEditor.CurrentAsset, UpdateAsset, $"Editing {roomEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(RoomAsset data, Action onConfirmAction, string headerText)
        {
            windowColumn1.gameObject.KillChildren();
            
            b.BuildInputField("Title", data.Title, windowColumn1, data.UpdateTitle);
            roomBuilder.BuildEssentials(windowColumn1, data, false);
            b.BuildPlainText("Created by", data.Author, windowColumn1);
            b.BuildPlainText("Created on", data.CreationDate.ToString(), windowColumn1);
            
            editedAssetBase = data;
            Open(new PropertyWindowInfo(headerText, PropertyLayoutType.Column1, "Done", "Cancel", onConfirmAction));
        }

        protected override void CreateAsset()
        {
            editor.CreateNewRoom((RoomAsset)editedAssetBase);
            selectionMenu.OpenForRooms();
        }

        protected override void UpdateAsset()
        {
            roomEditor.UpdateAsset((RoomAsset)editedAssetBase);
            roomEditor.CompleteEditing();
            selectionMenu.OpenForRooms();
        }

    }
}