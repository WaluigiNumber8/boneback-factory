using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.Packs;
using Rogium.Editors.Rooms;
using Rogium.Editors.TileData;

namespace Rogium.Editors.Core
{
    /// <summary>
    /// Overseers the work on a given pack.
    /// </summary>
    public class PackEditorOverseer : IEditorOverseer
    {
        public event Action<PackAsset, int, string, string> OnSaveChanges;
        
        private readonly RoomEditorOverseer roomEditor;
        private readonly TileEditorOverseer tileEditor;

        private PackAsset currentPack;
        private int myIndex;
        private string startingTitle;
        private string startingAuthor;

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

        private PackEditorOverseer() 
        {
            roomEditor = RoomEditorOverseer.Instance;
            tileEditor = TileEditorOverseer.Instance;
            roomEditor.OnCompleteEditing += UpdateRoom;
            tileEditor.OnCompleteEditing += UpdateTile;
        }

        /// <summary>
        /// Assigns a new pack for editing.
        /// </summary>
        /// <param name="pack">The new pack that will be edited.</param>
        public void AssignAsset(PackAsset pack, int index)
        {
            SafetyNet.EnsureIsNotNull(pack, "Pack to assign");
            currentPack = new PackAsset(pack);
            myIndex = index;
            startingTitle = currentPack.Title;
            startingAuthor = currentPack.Author;
        }

        #region Rooms
        /// <summary>
        /// Creates a new room, and adds it to the Pack Asset.
        /// <param name="newRoom">The new Room Asset to Add.</param>
        /// </summary>
        public void CreateNewRoom(RoomAsset newRoom)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - List of Rooms");
            CurrentPack.Rooms.Add(newRoom);
            
        }
        public void CreateNewRoom()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - List of Rooms");
            CurrentPack.Rooms.Add(new RoomAsset());
            SavePackChanges();
        }

        /// <summary>
        /// Updates the room in the given pack.
        /// </summary>
        /// <param name="newTile">Room Asset with the new details.</param>
        /// <param name="positionIndex">Which room to override.</param>
        public void UpdateRoom(RoomAsset newTile, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            
            CurrentPack.Rooms.Update(positionIndex, newTile);
            SavePackChanges();
        } 

        /// <summary>
        /// Deletes a room from the pack.
        /// <param name="roomIndex">The index of the room to be deleted.</param>
        /// </summary>
        public void RemoveRoom(int roomIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Correct Pack");
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
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Rooms, "List of Rooms");
            SafetyNet.EnsureIntIsInRange(roomIndex, 0, currentPack.Rooms.Count, "Room Index");
            roomEditor.AssignAsset(CurrentPack.Rooms[roomIndex], roomIndex);
        }

        #endregion

        #region Tiles
        /// <summary>
        /// Creates a new tile, and adds it to the Pack Asset.
        /// <param name="newTile">The new Tile Asset to Add.</param>
        /// </summary>
        public void CreateNewTile(TileAsset newTile)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - List of Tiles");
            CurrentPack.Tiles.Add(newTile);
            
        }
        public void CreateNewTile()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - List of Tiles");
            CurrentPack.Tiles.Add(new TileAsset());
            SavePackChanges();
        }

        /// <summary>
        /// Updates the tile in the given pack.
        /// </summary>
        /// <param name="newTile">Tile Asset with the new details.</param>
        /// <param name="positionIndex">Which tile to override.</param>
        public void UpdateTile(TileAsset newTile, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Tiles, "List of Tiles");
            
            CurrentPack.Tiles.Update(positionIndex, newTile);
            SavePackChanges();
        } 

        /// <summary>
        /// Deletes a tile from the pack.
        /// <param name="tileIndex">The index of the tile to be deleted.</param>
        /// </summary>
        public void RemoveTile(int tileIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Tiles, "List of Tiles");
            currentPack.Tiles.RemoveAt(tileIndex);

            SavePackChanges();
        }

        /// <summary>
        /// Send Command to the Tile Editor, to start editing a tile.
        /// </summary>
        /// <param name="tileIndex">Room index from the list</param>
        public void ActivateTileEditor(int tileIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Currect Pack");
            SafetyNet.EnsureListIsNotEmptyOrNull(currentPack.Tiles, "List of Tiles");
            SafetyNet.EnsureIntIsInRange(tileIndex, 0, currentPack.Tiles.Count, "Tile Index");
            tileEditor.AssignAsset(CurrentPack.Tiles[tileIndex], tileIndex);
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
            OnSaveChanges?.Invoke(currentPack, myIndex, startingTitle, startingAuthor);
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