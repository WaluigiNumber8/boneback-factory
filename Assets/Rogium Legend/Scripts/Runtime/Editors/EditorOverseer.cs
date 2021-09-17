using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.PackData;
using Rogium.Editors.RoomData;
using Rogium.ExternalStorage;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the work on a given pack.
    /// </summary>
    public class EditorOverseer : IEditorOverseer
    {
        private readonly RoomEditorOverseer roomEditor;

        private PackAsset currentPack;
        private string startingTitle;

        #region Singleton Pattern
        private static EditorOverseer instance;
        private static readonly object padlock = new object();
        public static EditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new EditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private EditorOverseer() 
        {
            roomEditor = RoomEditorOverseer.Instance;
        }

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="pack">The new pack that will be edited.</param>
        public void AssignNewPack(PackAsset pack)
        {
            currentPack = pack;
            startingTitle = pack.Title;
        }

        /// <summary>
        /// Creates a new room, and adds it to the Pack Asset.
        /// </summary>
        public void CreateNewRoom()
        {
            RoomAsset newRoom = new RoomAsset();
            CurrentPack.Rooms.Add(newRoom);
        }

        /// <summary>
        /// Deletes a room from the pack.
        /// <param name="roomIndex">The index of the room to be deleted.</param>
        /// </summary>
        public void DeleteRoom(int roomIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            SafetyNet.EnsureIntIsInRange(roomIndex, 0, currentPack.Rooms.Count, "Room Index");
            currentPack.Rooms.RemoveAt(roomIndex);
        }

        /// <summary>
        /// Send Command to Room Editor, to start editing a room.
        /// </summary>
        /// <param name="roomIndex">Room index from the list</param>
        public void ActivateRoomEditor(int roomIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            SafetyNet.EnsureIntIsInRange(roomIndex, 0, currentPack.Rooms.Count, "Room Index");
            roomEditor.AssignCurrentAsset(CurrentPack.Rooms[roomIndex]);
        }

        /// <summary>
        /// Saves all edits done to a pack and "returns" it to the library.
        /// </summary>
        public void CompleteEditing()
        {
            ExternalStorageOverseer.Instance.Save(CurrentPack);

            if (startingTitle != CurrentPack.Title)
                ExternalStorageOverseer.Instance.Delete(startingTitle);

            currentPack = null;
        }

        public PackAsset CurrentPack
        {
            get 
            {
                if (currentPack == null) throw new MissingReferenceException("Current Pack has not been set. Did you forget to activate the editor?");
                return this.currentPack;
            }
        }
    }
}