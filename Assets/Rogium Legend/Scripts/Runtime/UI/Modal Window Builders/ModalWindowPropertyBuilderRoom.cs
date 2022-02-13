using System;
using System.Collections.Generic;
using BoubakProductions.UI;
using Rogium.Editors.Rooms;

namespace Rogium.UserInterface.ModalWindowBuilding
{
    /// <summary>
    /// Constructor for the Room Properties Modal Window.
    /// </summary>
    public class ModalWindowPropertyBuilderRoom : ModalWindowPropertyBuilder
    {
        private RoomEditorOverseer roomEditor;

        public ModalWindowPropertyBuilderRoom()
        {
            roomEditor = RoomEditorOverseer.Instance;
        }

        public override void OpenForCreate()
        {
            OpenWindow(new RoomAsset(), CreateAsset, "Creating new room");
        }

        public override void OpenForUpdate()
        {
            OpenWindow(new RoomAsset(roomEditor.CurrentAsset), UpdateAsset, $"Editing {roomEditor.CurrentAsset.Title}");
        }

        private void OpenWindow(RoomAsset currentRoomAsset, Action onConfirmAction, string headerText)
        {
            IList<string> options = new List<string>();
            options.Add("Level 1");
            options.Add("Level 2");
            options.Add("Level 3");
            options.Add("Level 4");
            options.Add("Level 5");

            //TODO - Move this to somewhere more logical.

            builder.BuildInputField("Title", currentRoomAsset.Title, window.FirstColumnContent, currentRoomAsset.UpdateTitle);
            builder.BuildDropdown("Difficulty", options, currentRoomAsset.DifficultyLevel, window.FirstColumnContent, currentRoomAsset.UpdateDifficultyLevel);
            builder.BuildDropdown("Type", Enum.GetNames(typeof(RoomType)), (int) currentRoomAsset.Type, window.FirstColumnContent, currentRoomAsset.UpdateType);
            builder.BuildPlainText("Created by", currentRoomAsset.Author, window.FirstColumnContent);
            builder.BuildPlainText("Created on", currentRoomAsset.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = currentRoomAsset;
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