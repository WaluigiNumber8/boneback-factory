using System;
using UnityEngine;
using BoubakProductions.Safety;

namespace Rogium.Editors.RoomData
{
    /// <summary>
    /// Overseers everything happening in the room editor.
    /// </summary>
    public class RoomEditorOverseer : IEditorOverseer
    {
        public event Action<RoomAsset> OnAssignRoom;
        public event Action<RoomAsset, int> OnCompleteEditing;
        private RoomAsset currentRoom;
        private int myIndex;

        #region Singleton Pattern
        private static RoomEditorOverseer instance;
        private static readonly object padlock = new object();
        public static RoomEditorOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new RoomEditorOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private RoomEditorOverseer() { }

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="room">The room that will be edited.</param>
        /// <param name="index"></param>
        public void AssignAsset(RoomAsset room, int index)
        {
            SafetyNet.EnsureIsNotNull(room, "Assigned Room");
            OnAssignRoom?.Invoke(room);
            currentRoom = new RoomAsset(room);
            myIndex = index;
        }

        public void CompleteEditing()
        {
            OnCompleteEditing?.Invoke(CurrentRoom, myIndex);
        }

        public RoomAsset CurrentRoom 
        {
            get 
            {
                if (currentRoom == null) throw new MissingReferenceException("Current Room has not been set. Did you forget to activate the editor?");
                return this.currentRoom;
            } 
        }
    }
}