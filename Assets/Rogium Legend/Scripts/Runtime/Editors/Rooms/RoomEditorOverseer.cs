using BoubakProductions.Safety;
using System;
using System.Collections;
using UnityEngine;

namespace Rogium.Editors.RoomData
{
    /// <summary>
    /// Overseers everything happening in the room editor.
    /// </summary>
    public class RoomEditorOverseer : IEditorOverseer
    {
        public event Action<RoomAsset> OnAssignRoom;
        private RoomAsset currentRoom;

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
        public void AssignCurrentAsset(RoomAsset room)
        {
            SafetyNet.EnsureIsNotNull(room, "Assigned Room");
            OnAssignRoom?.Invoke(room);
            currentRoom = room;
        }

        public void CompleteEditing()
        {
            throw new System.NotImplementedException();
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