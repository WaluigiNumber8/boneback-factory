using System;
using BoubakProductions.Core;
using BoubakProductions.UI;
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
            window.FirstColumnContent.gameObject.KillChildren();
            
            b.BuildInputField("Title", data.Title, window.FirstColumnContent, data.UpdateTitle);
            roomBuilder.BuildEssentials(window.FirstColumnContent, data, false);
            b.BuildPlainText("Created by", data.Author, window.FirstColumnContent);
            b.BuildPlainText("Created on", data.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = data;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Blue, "Done", "Cancel", onConfirmAction, true);
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