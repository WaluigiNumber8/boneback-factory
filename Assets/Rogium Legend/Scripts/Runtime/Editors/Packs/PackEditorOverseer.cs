using System;
using UnityEngine;
using BoubakProductions.Safety;
using Rogium.Editors.Palettes;
using Rogium.Editors.Rooms;
using Rogium.Editors.Sprites;
using Rogium.Editors.Tiles;

namespace Rogium.Editors.Packs
{
    /// <summary>
    /// Overseers the work on a given pack.
    /// </summary>
    public class PackEditorOverseer : IEditorOverseer
    {
        public event Action<PackAsset, int, string, string> OnSaveChanges;

        private readonly PaletteEditorOverseer paletteEditor;
        private readonly SpriteEditorOverseer spriteEditor;
        private readonly TileEditorOverseer tileEditor;
        private readonly RoomEditorOverseer roomEditor;

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
            paletteEditor = PaletteEditorOverseer.Instance;
            spriteEditor = SpriteEditorOverseer.Instance;
            tileEditor = TileEditorOverseer.Instance;
            roomEditor = RoomEditorOverseer.Instance;

            paletteEditor.OnCompleteEditing += UpdatePalette;
            spriteEditor.OnCompleteEditing += UpdateSprite;
            tileEditor.OnCompleteEditing += UpdateTile;
            roomEditor.OnCompleteEditing += UpdateRoom;
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

        #region Palettes
        /// <summary>
        /// Creates a new palette, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Palette Asset to Add.</param>
        /// </summary>
        public void CreateNewPalette(PaletteAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Palettes, "Pack Editor - List of Palettes");
            CurrentPack.Palettes.Add(newAsset);
            
        }
        public void CreateNewPalette()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Palettes, "Pack Editor - List of Palettes");
            CurrentPack.Palettes.Add(new PaletteAsset());
            SavePackChanges();
        }

        /// <summary>
        /// Updates the palette in the given pack.
        /// </summary>
        /// <param name="newAsset">Palette Asset with the new details.</param>
        /// <param name="positionIndex">Which palette to override.</param>
        public void UpdatePalette(PaletteAsset newAsset, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Palettes, "List of Palettes");
            
            CurrentPack.Palettes.Update(positionIndex, newAsset);
            SavePackChanges();
        } 

        /// <summary>
        /// Deletes a palette from the pack.
        /// <param name="assetIndex">The index of the palette to be deleted.</param>
        /// </summary>
        public void RemovePalette(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Palettes, "List of Palettes");
            currentPack.Palettes.RemoveAt(assetIndex);

            SavePackChanges();
        }

        /// <summary>
        /// Send Command to the Palette Editor, to start editing a palette.
        /// </summary>
        /// <param name="assetIndex">Palette index from the list.</param>
        /// <param name="prepareEditor"></param>
        public void ActivatePaletteEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Palettes, "List of Palettes");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Palettes.Count, "Palette Index");
            paletteEditor.AssignAsset(CurrentPack.Palettes[assetIndex], assetIndex, prepareEditor);
        }

        #endregion
        
        #region Sprites
        /// <summary>
        /// Creates a new sprite, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Sprite Asset to Add.</param>
        /// </summary>
        public void CreateNewSprite(SpriteAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Sprites, "Pack Editor - List of Sprites");
            CurrentPack.Sprites.Add(newAsset);
            
        }
        public void CreateNewSprite()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Sprites, "Pack Editor - List of Sprites");
            CurrentPack.Sprites.Add(new SpriteAsset());
            SavePackChanges();
        }

        /// <summary>
        /// Updates the sprite in the given pack.
        /// </summary>
        /// <param name="newAsset">Sprite Asset with the new details.</param>
        /// <param name="positionIndex">Which sprite to override.</param>
        public void UpdateSprite(SpriteAsset newAsset, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Sprites, "List of Sprites");
            
            CurrentPack.Sprites.Update(positionIndex, newAsset);
            SavePackChanges();
        } 

        /// <summary>
        /// Deletes a sprite from the pack.
        /// <param name="assetIndex">The index of the sprite to be deleted.</param>
        /// </summary>
        public void RemoveSprite(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Sprites, "List of Sprites");
            currentPack.Sprites.RemoveAt(assetIndex);

            SavePackChanges();
        }

        /// <summary>
        /// Send Command to the Sprite Editor, to start editing a palette.
        /// </summary>
        /// <param name="assetIndex">Sprite index from the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateSpriteEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Sprites, "List of Sprites");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Sprites.Count, "Sprite Index");
            spriteEditor.AssignAsset(CurrentPack.Sprites[assetIndex], assetIndex, prepareEditor);
        }

        #endregion
        
        #region Tiles
        /// <summary>
        /// Creates a new tile, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Tile Asset to Add.</param>
        /// </summary>
        public void CreateNewTile(TileAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Tiles, "Pack Editor - List of Tiles");
            CurrentPack.Tiles.Add(newAsset);
            
        }
        public void CreateNewTile()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Tiles, "Pack Editor - List of Tiles");
            CurrentPack.Tiles.Add(new TileAsset());
            SavePackChanges();
        }

        /// <summary>
        /// Updates the tile in the given pack.
        /// </summary>
        /// <param name="newAsset">Tile Asset with the new details.</param>
        /// <param name="positionIndex">Which tile to override.</param>
        public void UpdateTile(TileAsset newAsset, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Tiles, "List of Tiles");
            
            CurrentPack.Tiles.Update(positionIndex, newAsset);
            SavePackChanges();
        } 

        /// <summary>
        /// Deletes a tile from the pack.
        /// <param name="assetIndex">The index of the tile to be deleted.</param>
        /// </summary>
        public void RemoveTile(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Tiles, "List of Tiles");
            currentPack.Tiles.RemoveAt(assetIndex);

            SavePackChanges();
        }

        /// <summary>
        /// Send Command to the Tile Editor, to start editing a tile.
        /// </summary>
        /// <param name="assetIndex">Tile index from the list.</param>
        /// <param name="prepareEditor">If true, load asset into the editor.</param>
        public void ActivateTileEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Tiles, "List of Tiles");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Tiles.Count, "Tile Index");
            tileEditor.AssignAsset(CurrentPack.Tiles[assetIndex], assetIndex, prepareEditor);
        }

        #endregion
        
        #region Rooms
        /// <summary>
        /// Creates a new room, and adds it to the Pack Asset.
        /// <param name="newAsset">The new Room Asset to Add.</param>
        /// </summary>
        public void CreateNewRoom(RoomAsset newAsset)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - List of Rooms");
            CurrentPack.Rooms.Add(newAsset);
            
        }
        public void CreateNewRoom()
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureIsNotNull(currentPack.Rooms, "Pack Editor - List of Rooms");
            CurrentPack.Rooms.Add(new RoomAsset());
            SavePackChanges();
        }

        /// <summary>
        /// Updates the room in the given pack.
        /// </summary>
        /// <param name="newAsset">Room Asset with the new details.</param>
        /// <param name="positionIndex">Which room to override.</param>
        public void UpdateRoom(RoomAsset newAsset, int positionIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Rooms, "List of Rooms");
            
            CurrentPack.Rooms.Update(positionIndex, newAsset);
            SavePackChanges();
        } 

        /// <summary>
        /// Deletes a room from the pack.
        /// <param name="assetIndex">The index of the room to be deleted.</param>
        /// </summary>
        public void RemoveRoom(int assetIndex)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Rooms, "List of Rooms");
            currentPack.Rooms.RemoveAt(assetIndex);

            SavePackChanges();
        }

        /// <summary>
        /// Send Command to Room Editor, to start editing a room.
        /// </summary>
        /// <param name="assetIndex">Room index from the list</param>
        /// <param name="prepareEditor"></param>
        public void ActivateRoomEditor(int assetIndex, bool prepareEditor = true)
        {
            SafetyNet.EnsureIsNotNull(currentPack, "Pack Editor - Current Pack");
            SafetyNet.EnsureListIsNotNullOrEmpty(currentPack.Rooms, "List of Rooms");
            SafetyNet.EnsureIntIsInRange(assetIndex, 0, currentPack.Rooms.Count, "Room Index");
            roomEditor.AssignAsset(CurrentPack.Rooms[assetIndex], assetIndex, prepareEditor);
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