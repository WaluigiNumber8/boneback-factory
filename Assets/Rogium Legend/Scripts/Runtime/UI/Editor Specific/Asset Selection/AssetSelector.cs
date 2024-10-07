using System;
using System.Linq;
using RedRats.Systems.ObjectSwitching;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using UnityEngine;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Prepares the Asset Selection system for choosing assets to edit.
    /// </summary>
    [RequireComponent(typeof(ObjectSwitcherMono))]
    public class AssetSelector : MonoBehaviour
    {
        [SerializeField] private LayoutInfo layouts;
        [SerializeField] private AssetSelectionMenuInfo selectionMenus;
        [SerializeField] private AssetHolderBase emptyAsset;

        private ObjectSwitcherMono layoutSwitcher;
        private AssetSelectionController assetSelection;
        private PackEditorOverseer editor;
        private ExternalLibraryOverseer lib;
        private InternalLibraryOverseer internalLib;

        private Action<AssetHolderBase> whenSpawnCard;
        private Action whenFinishedFilling;

        private void Awake()
        {
            layoutSwitcher = GetComponent<ObjectSwitcherMono>();
            editor = PackEditorOverseer.Instance;
            lib = ExternalLibraryOverseer.Instance;
            internalLib = InternalLibraryOverseer.GetInstance();

            assetSelection = new AssetSelectionController();
        }

        #region Open Selection Menu

        //TODO - Instead of the SelectionMenuAsset, make the default layout type load from the options save file.
        public void Open(AssetType type, bool addEmptyAsset = false)
        {
            AssetHolderBase empty = (addEmptyAsset) ? emptyAsset :  null;
            switch (type)
            {
                case AssetType.Pack:
                    assetSelection.Setup(AssetType.Pack,
                        layouts.grid,
                        selectionMenus.pack,
                        lib.Packs.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Palette:
                    assetSelection.Setup(AssetType.Palette,
                        layouts.grid,
                        selectionMenus.palette,
                        editor.CurrentPack.Palettes.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Sprite:
                    assetSelection.Setup(AssetType.Sprite,
                        layouts.grid,
                        selectionMenus.sprite,
                        editor.CurrentPack.Sprites.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Weapon:
                    assetSelection.Setup(AssetType.Weapon,
                        layouts.grid,
                        selectionMenus.weapon,
                        editor.CurrentPack.Weapons.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Projectile:
                    assetSelection.Setup(AssetType.Projectile,
                        layouts.grid,
                        selectionMenus.projectile,
                        editor.CurrentPack.Projectiles.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Enemy:
                    assetSelection.Setup(AssetType.Enemy,
                        layouts.grid,
                        selectionMenus.enemy,
                        editor.CurrentPack.Enemies.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Room:
                    assetSelection.Setup(AssetType.Room,
                        layouts.list,
                        selectionMenus.room,
                        editor.CurrentPack.Rooms.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Tile:
                    assetSelection.Setup(AssetType.Tile,
                        layouts.grid,
                        selectionMenus.tile,
                        editor.CurrentPack.Tiles.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Object:
                    assetSelection.Setup(AssetType.Object,
                        layouts.grid,
                        selectionMenus.interactableObject,
                        internalLib.Objects.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.Sound:
                    assetSelection.Setup(AssetType.Sound,
                        layouts.grid,
                        selectionMenus.sound,
                        internalLib.Sounds.ToList<IAsset>(),
                        empty);
                    break;
                case AssetType.None:
                case AssetType.Campaign:
                default:
                    throw new ArgumentOutOfRangeException($"{type} is not supported.");
            }
            layoutSwitcher.Switch((type == AssetType.Room) ? 1 : 0);
        }

        #endregion

        /// <summary>
        /// Start listening to every card that is spawned by the selection menu.
        /// Will no longer listen after the menu is filled.
        /// </summary>
        /// <param name="whenSpawnCard">The method that will work with the currently spawned card.</param>
        public void BeginListeningToSpawnedCards(Action<AssetHolderBase> whenSpawnCard, Action whenFinishedFilling = null)
        {
            this.whenSpawnCard = whenSpawnCard;
            this.whenFinishedFilling = whenFinishedFilling;
            assetSelection.OnSpawnCard += this.whenSpawnCard;
            assetSelection.OnFinishedFilling += StopListeningToSpawnedCards;
        }

        /// <summary>
        /// Stop listening to every card that is spawned by the selection menu.
        /// </summary>
        private void StopListeningToSpawnedCards()
        {
            assetSelection.OnSpawnCard -= whenSpawnCard;
            whenFinishedFilling?.Invoke();
            whenSpawnCard = null;
            whenFinishedFilling = null;
        }

        public Transform ListMenu { get => layouts.list.Menu; }
        public Transform GridMenu { get => layouts.grid.Menu; }
    }
}