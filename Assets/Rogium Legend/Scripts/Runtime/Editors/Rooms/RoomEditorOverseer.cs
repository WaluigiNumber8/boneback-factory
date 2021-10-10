using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.TileData;

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

        private readonly ToolboxEffects gridEditor;
        private readonly ToolBox toolBox;

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

        private RoomEditorOverseer()
        {
            gridEditor = new ToolboxEffects();
            toolBox = new ToolBox();
        }

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="room">The room that will be edited.</param>
        /// <param name="index">The position in the list.</param>
        public void AssignAsset(RoomAsset room, int index)
        {
            SafetyNet.EnsureIsNotNull(room, "Assigned Room");
            currentRoom = new RoomAsset(room);
            myIndex = index;
            OnAssignRoom?.Invoke(room);
        }

        /// <summary>
        /// Updates Tiles on the tile grid, based on the active tool in the editor.
        /// </summary>
        /// <param name="worldPosition">Position of the tile, that will be updated, on the grid.</param>
        /// <param name="asset">The tile asset we want to place here.</param>
        public void UpdateTile(Vector2Int worldPosition, TileAsset asset)
        {
            gridEditor.UseTool(CurrentRoom.TileGrid, asset, worldPosition, toolBox.CurrentTool);
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