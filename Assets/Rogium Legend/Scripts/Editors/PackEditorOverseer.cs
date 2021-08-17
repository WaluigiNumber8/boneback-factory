using BoubakProductions.Safety;
using RogiumLegend.Editors.PackData;
using RogiumLegend.Editors.RoomData;
using RogiumLegend.ExternalStorage;
using UnityEngine;

namespace RogiumLegend.Editors
{
    /// <summary>
    /// Overseers the work on a given pack.
    /// </summary>
    public class PackEditorOverseer : IEditorOverseer
    {
        private PackAsset currentPack;

        #region Singleton Pattern
        private static PackEditorOverseer instance;
        private static readonly object padlock = new object();
        public static PackEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new PackEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private PackEditorOverseer() { }

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="pack">The new pack that will be edited.</param>
        public void AssignNewPack(PackAsset pack)
        {
            currentPack = pack;
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
        /// Send Command to Room Editor, to start editing a room.
        /// </summary>
        /// <param name="roomIndex">Room index from the list</param>
        public void ActivateRoomEditor(int roomIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            RoomEditorOverseer.Instance.AssignCurrentAsset(CurrentPack.Rooms[roomIndex]);
        }

        /// <summary>
        /// Saves all edits done to a pack and "returns" it to the library.
        /// </summary>
        public void CompleteEditing()
        {
            ExternalStorageOverseer.Instance.Save(CurrentPack);
            currentPack = null;
        }

        public PackAsset CurrentPack
        {
            get 
            {
                if (currentPack == null) throw new MissingReferenceException("Current Room has not been set. Did you forget to activate the editor?");
                return this.currentPack;
            }
        }
    }
}