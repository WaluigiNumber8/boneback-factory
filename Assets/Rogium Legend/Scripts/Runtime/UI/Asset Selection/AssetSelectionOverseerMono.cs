using System;
using System.Linq;
using BoubakProductions.Systems.ObjectSwitching;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.ExternalStorage;
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
        private ExternalLibraryOverseer lib;
        private ObjectSwitcherMono layoutSwitcher;

        private Action<AssetHolderBase> listeningMethod;
        private Action finishedMethod;
        
        private void Awake()
        {
            editor = PackEditorOverseer.Instance;
            lib = ExternalLibraryOverseer.Instance;
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
                           lib.GetPacksCopy.ToList<IAsset>(),
                           layoutSwitcher);
        }
        
        public void OpenForPalettes()
        {
            overseer.Setup(AssetType.Palette,
                           layouts.grid,
                           selectionMenus.palette,
                           editor.CurrentPack.Palettes.ToList<IAsset>(),
                           layoutSwitcher);
        }

        public void OpenForSprites()
        {
            overseer.Setup(AssetType.Sprite,
                           layouts.grid,
                           selectionMenus.sprite,
                           editor.CurrentPack.Sprites.ToList<IAsset>(),
                           layoutSwitcher);
        }
        
        public void OpenForWeapons()
        {
            overseer.Setup(AssetType.Weapon,
                           layouts.grid,
                           selectionMenus.weapon,
                           editor.CurrentPack.Weapons.ToList<IAsset>(),
                           layoutSwitcher);
        }

        public void OpenForProjectiles()
        {
            overseer.Setup(AssetType.Projectile,
                           layouts.grid,
                           selectionMenus.projectile,
                           editor.CurrentPack.Projectiles.ToList<IAsset>(),
                           layoutSwitcher);
        }

        public void OpenForEnemies()
        {
            overseer.Setup(AssetType.Enemy,
                           layouts.grid,
                           selectionMenus.enemy,
                           editor.CurrentPack.Enemies.ToList<IAsset>(),
                           layoutSwitcher);
        }

        public void OpenForRooms()
        {
            overseer.Setup(AssetType.Room,
                           layouts.list,
                           selectionMenus.room,
                           editor.CurrentPack.Rooms.ToList<IAsset>(),
                           layoutSwitcher);
        }
        
        public void OpenForTiles()
        {
            overseer.Setup(AssetType.Tile,
                           layouts.grid,
                           selectionMenus.tile,
                           editor.CurrentPack.Tiles.ToList<IAsset>(),
                           layoutSwitcher);
        }

        public void OpenForObjects()
        {
            overseer.Setup(AssetType.Object,
                           layouts.grid,
                           selectionMenus.interactableObject,
                           InternalLibraryOverseer.GetInstance().GetObjectsCopy().ToList<IAsset>(),
                           layoutSwitcher);
        }
        
        public void OpenForSounds()
        {
            throw new NotImplementedException();
        }
        #endregion

        /// <summary>
        /// Start listening to every card that is spawned by the selection menu.
        /// Will no longer listen after the menu is filled.
        /// </summary>
        /// <param name="listeningMethod">The method that will work with the currently spawned card.</param>
        public void BeginListeningToSpawnedCards(Action<AssetHolderBase> listeningMethod, Action finishedMethod = null)
        {
            this.listeningMethod = listeningMethod;
            this.finishedMethod = finishedMethod;
            overseer.OnSpawnCard += this.listeningMethod;
            overseer.OnFinishedFilling += StopListeningToSpawnedCards;
            
        }

        /// <summary>
        /// Stop listening to every card that is spawned by the selection menu.
        /// </summary>
        private void StopListeningToSpawnedCards()
        {
            overseer.OnSpawnCard -= listeningMethod;
            finishedMethod?.Invoke();
            listeningMethod = null;
            finishedMethod = null;
        }
        
        public Transform ListMenu { get => layouts.list.Menu; }
        public Transform GridMenu { get => layouts.grid.Menu; }

    }
}