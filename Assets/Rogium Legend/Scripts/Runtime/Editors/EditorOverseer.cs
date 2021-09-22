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
            roomEditor.OnCompleteEditing += UpdateRoom;
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

        #region Rooms
        /// <summary>
        /// Creates a new room, and adds it to the Pack Asset.
        /// <param name="newRoom">The new Room Asset to Add.</param>
        /// </summary>
        public void CreateNewRoom(RoomAsset newRoom)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            CurrentPack.Rooms.Add(newRoom);
            
        }
        public void CreateNewRoom()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            CurrentPack.Rooms.Add(new RoomAsset());
            SavePackChanges();
        }

        /// <summary>
        /// Updates the room in the given pack.
        /// </summary>
        /// <param name="newRoom">Room Asset with the new details.</param>
        /// <param name="positionIndex">Which room to override.</param>
        public void UpdateRoom(RoomAsset newRoom, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            
            CurrentPack.Rooms.Update(positionIndex, newRoom);
            SavePackChanges();
        } 

        /// <summary>
        /// Deletes a room from the pack.
        /// <param name="roomIndex">The index of the room to be deleted.</param>
        /// </summary>
        public void RemoveRoom(int roomIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            currentPack.Rooms.RemoveAt(roomIndex);

            SavePackChanges();
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
            roomEditor.AssignCurrentAsset(CurrentPack.Rooms[roomIndex], roomIndex);
        }

        #endregion
        
        /// <summary>
        /// Saves all edits done to a pack and "returns" it to the library.
        /// </summary>
        public void CompleteEditing()
        {
            SavePackChanges();
            currentPack = null;
        }

        /// <summary>
        /// Save changes.
        /// </summary>
        private void SavePackChanges()
        {
            ExternalStorageOverseer.Instance.Save(CurrentPack);

            if (startingTitle != CurrentPack.Title)
                ExternalStorageOverseer.Instance.Delete(startingTitle);
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