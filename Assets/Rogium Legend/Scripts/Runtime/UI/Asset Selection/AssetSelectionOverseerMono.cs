using BoubakProductions.Systems.ObjectSwitching;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using System;
using System.Linq;
using UnityEngine;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Prepares the Asset Selection system for choosing assets to edit.
    /// </summary>
    [RequireComponent(typeof(ObjectSwitcherMono))]
    public class AssetSelectionOverseerMono : MonoBehaviour, IAssetSelectionOverseer
    {
        [SerializeField] private LayoutInfo layouts;
        [SerializeField] private AssetSelectionMenuInfo selectionMenus;

        private AssetSelectionOverseer overseer;
        private PackEditorOverseer editor;
        private LibraryOverseer lib;
        private ObjectSwitcherMono layoutSwitcher;

        private Action<AssetHolderBase> listeningMethod;
        
        private void Awake()
        {
            editor = PackEditorOverseer.Instance;
            lib = LibraryOverseer.Instance;
            layoutSwitcher = GetComponent<ObjectSwitcherMono>();
            
            overseer = AssetSelectionOverseer.Instance;
        }

        #region Open Selection Menu

        //TODO - Instead of the SelectionMenuAsset, make the default layout type load from the options save file.
        public void OpenForPacks()
        {
            overseer.Setup(AssetType.Pack,
                           layouts.grid,
                           selectionMenus.pack,
                           lib.GetPacksCopy.ToList<AssetBase>(),
                           layoutSwitcher);
        }
        
        public void OpenForPalettes()
        {
            overseer.Setup(AssetType.Palette,
                           layouts.grid,
                           selectionMenus.palette,
                           editor.CurrentPack.Palettes.ToList<AssetBase>(),
                           layoutSwitcher);
        }

        public void OpenForSprites()
        {
            overseer.Setup(AssetType.Sprite,
                           layouts.grid,
                           selectionMenus.sprite,
                           editor.CurrentPack.Sprites.ToList<AssetBase>(),
                           layoutSwitcher);
        }
        
        public void OpenForWeapons()
        {
            throw new NotImplementedException();
        }

        public void OpenForProjectiles()
        {
            throw new NotImplementedException();
        }

        public void OpenForEnemies()
        {
            throw new NotImplementedException();
        }

        public void OpenForRooms()
        {
            overseer.Setup(AssetType.Room,
                           layouts.list,
                           selectionMenus.room,
                           editor.CurrentPack.Rooms.ToList<AssetBase>(),
                           layoutSwitcher);
        }
        
        public void OpenForTiles()
        {
            overseer.Setup(AssetType.Tile,
                           layouts.grid,
                           selectionMenus.tile,
                           editor.CurrentPack.Tiles.ToList<AssetBase>(),
                           layoutSwitcher);
        }

        #endregion

        /// <summary>
        /// Start listening to every card that is spawned by the selection menu.
        /// Will no longer listen after the menu is filled.
        /// </summary>
        /// <param name="listeningMethod">The method that will work with the currently spawned card.</param>
        public void BeginListeningToSpawnedCards(Action<AssetHolderBase> listeningMethod)
        {
            this.listeningMethod = listeningMethod;
            overseer.OnSpawnCard += this.listeningMethod;
            overseer.OnFinishedFilling += StopListeningToSpawnedCards;
            
        }

        /// <summary>
        /// Stop listening to every card that is spawned by the selection menu.
        /// </summary>
        private void StopListeningToSpawnedCards()
        {
            overseer.OnSpawnCard -= listeningMethod;
        }
        
        public Transform ListMenu { get => layouts.list.Menu; }
        public Transform GridMenu { get => layouts.grid.Menu; }
        
    }
}