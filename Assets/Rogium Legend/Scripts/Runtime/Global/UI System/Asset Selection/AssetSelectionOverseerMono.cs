using BoubakProductions.Core;
using BoubakProductions.Safety;
using BoubakProductions.ObjectSwitching;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.Editors.Packs;
using Rogium.Global.UISystem.UI;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Rogium.Global.UISystem.AssetSelection
{
    /// <summary>
    /// Is responsible for controlling the Asset Selection Menu, and switching out content as needed in it.
    /// </summary>
    [RequireComponent(typeof(ObjectSwitcherMono))]
    public class AssetSelectionOverseerMono : MonoSingleton<AssetSelectionOverseerMono>
    {
        [SerializeField] private LayoutInfo layouts;
        [SerializeField] private AssetSelectionMenuInfo selectionMenus;
        
        private EditorOverseer editor;
        private LibraryOverseer lib;
        private ObjectSwitcherMono layoutSwitcher;
        
        private IList<IAssetHolder> assets = new List<IAssetHolder>();
        private AssetType lastTypeOpen;

        protected override void Awake()
        {
            base.Awake();
            editor = EditorOverseer.Instance;
            lib = LibraryOverseer.Instance;
            layoutSwitcher = GetComponent<ObjectSwitcherMono>();
            lastTypeOpen = AssetType.None;
        }

        /// <summary>
        /// Opens the selection menu for weapon assets.
        /// </summary>
        public void OpenForWeapons()
        {
            throw new System.NotImplementedException();
        }

        //TODO - Instead of the SelectionMenuAsset, make the default layout load from the options save file.

        /// <summary>
        /// Opens the selection menu for room assets.
        /// </summary>
        public void ReopenForRooms()
        {
            Setup(AssetType.Room,
                  layouts.list,
                  selectionMenus.room,
                  editor.CurrentPack.Rooms.ToList<AssetBase>());
        }
        
        /// <summary>
        /// Opens the selection menu for tile assets.
        /// </summary>
        public void ReopenForTiles()
        {
            Setup(AssetType.Tile,
                layouts.grid,
                selectionMenus.tile,
                editor.CurrentPack.Tiles.ToList<AssetBase>());
        }

        /// <summary>
        /// Opens the selection menu for pack assets.
        /// </summary>
        public void ReopenForPacks()
        {
            Setup(AssetType.Pack,
                  layouts.grid,
                  selectionMenus.pack,
                  lib.GetPacksCopy.ToList<AssetBase>());
        }

        /// <summary>
        /// Sets up the selection menu.
        /// </summary>
        private void Setup(AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<AssetBase> assetList)
        {
            layoutSwitcher.Switcher.DeselectAllExcept(layout.Menu);

            //Do not refill, if menu is the same.
            if (type == lastTypeOpen && type != AssetType.None)
            {
                if (assetList.Count != assets.Count)
                {
                    //Spawn newly created cards.
                    int newCards = assetList.Count - assets.Count;

                    //If a pack was removed, reload the list.
                    if (newCards > 0)
                    {
                        for (int i = 0; i < newCards; i++)
                        {
                            SpawnCard(assetList.Count + (i-1), type, layout, asset, assetList);
                        }
                        return;
                    }
                }
            }

            RefillMenu(type, layout, asset, assetList);
            lastTypeOpen = type;
        }

        /// <summary>
        /// Fills the selection menu canvas with asset holder objects.
        /// <param name="type">The type of asset this card will be.</param>
        /// <param name="layout">Which layout is used.</param>
        /// <param name="asset">What kind of data will be used.</param>
        /// <param name="assetList">The list to take the data from.</param>
        /// </summary>
        private void RefillMenu(AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<AssetBase> assetList)
        {
            SafetyNet.EnsureIsNotNull(asset.assetObject.GetComponent<IAssetHolder>(), "IAssetHolder in RefillMenu");
            
            //Clear assets from content, respawn add button
            assets.Clear();
            layout.Content.KillChildren();
            Instantiate(asset.addButton, layout.Content.transform);

            //Spawn new assets
            for (int i = 0; i < assetList.Count; i++)
            {
                SpawnCard(i, type, layout, asset, assetList);
            }
        }

        /// <summary>
        /// Spawns the clickable UI Card itself.
        /// </summary>
        /// <param name="id">Position on the list it spawns the card on.</param>
        /// <param name="type">The type of asset this card will be.</param>
        /// <param name="layout">Which layout is used.</param>
        /// <param name="asset">What kind of data will be used.</param>
        /// <param name="assetList">The list to take the data from.</param>
        private void SpawnCard(int id, AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<AssetBase> assetList)
        {
            IAssetHolder holder = Instantiate(asset.assetObject, layout.Content.transform).GetComponent<IAssetHolder>();
            if (layout.IconPositionType == IconPositionType.Global)
                holder.Construct(type, id, assetList[id], layout.IconPosition);
            else
                holder.Construct(type, id, assetList[id]);
            assets.Add(holder);
        }

        /// <summary>
        /// Get amount of assets showcased by the menu.
        /// </summary>
        public int AssetCount => assets.Count;

        public GameObject ListMenu { get => layouts.list.Menu; }
        public GameObject GridMenu { get => layouts.grid.Menu; }
        
        [System.Serializable]
        public struct AssetSelectionMenuInfo
        {
            public SelectionMenuAsset pack;
            public SelectionMenuAsset weapon;
            public SelectionMenuAsset enemy;
            public SelectionMenuAsset room;
            public SelectionMenuAsset tile;
            public SelectionMenuAsset projectile;
            public SelectionMenuAsset palette;
            public SelectionMenuAsset sprite;
        }

        [System.Serializable]
        public struct LayoutInfo
        {
            public SelectionMenuLayout grid;
            public SelectionMenuLayout list;
        }

        public void ReopenForPalettes()
        {
            throw new System.NotImplementedException();
        }

        public void ReopenForSprites()
        {
            throw new System.NotImplementedException();
        }

        public void ReopenForWeapons()
        {
            throw new System.NotImplementedException();
        }

        public void ReopenForEnemies()
        {
            throw new System.NotImplementedException();
        }
    }
}