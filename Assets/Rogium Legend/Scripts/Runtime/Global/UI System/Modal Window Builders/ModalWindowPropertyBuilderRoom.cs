using System;
using System.Collections.Generic;
using BoubakProductions.UI;
using Rogium.Editors.Rooms;

namespace Rogium.Global.UISystem.UI
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
            OpenWindow(new RoomAsset(roomEditor.CurrentRoom), UpdateAsset, $"Editing {roomEditor.CurrentRoom}");
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

            propertyBuilder.BuildInputField("Title", currentRoomAsset.Title, window.FirstColumnContent, currentRoomAsset.UpdateTitle);
            propertyBuilder.BuildDropdown("Difficulty", options, currentRoomAsset.DifficultyLevel, window.FirstColumnContent, currentRoomAsset.UpdateDifficultyLevel);
            propertyBuilder.BuildDropdown("Type", Enum.GetNames(typeof(RoomType)), (int) currentRoomAsset.Type, window.FirstColumnContent, currentRoomAsset.UpdateType);
            propertyBuilder.BuildPlainText("Created by", currentRoomAsset.Author, window.FirstColumnContent);
            propertyBuilder.BuildPlainText("Created on", currentRoomAsset.CreationDate.ToString(), window.FirstColumnContent);
            
            editedAssetBase = currentRoomAsset;
            window.OpenAsPropertiesColumn1(headerText, ThemeType.Blue, "Done", "Cancel", onConfirmAction, true);
        }

        protected override void CreateAsset()
        {
            editor.CreateNewRoom((RoomAsset)editedAssetBase);
            selectionMenu.ReopenForRooms();
        }

        protected override void UpdateAsset()
        {
            roomEditor.UpdateAsset((RoomAsset)editedAssetBase);
            roomEditor.CompleteEditing();
            selectionMenu.ReopenForRooms();
        }

    }
}