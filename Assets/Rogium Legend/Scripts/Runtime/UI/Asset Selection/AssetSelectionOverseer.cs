using BoubakProductions.Core;
using BoubakProductions.Safety;
using BoubakProductions.Systems.ObjectSwitching;
using Rogium.Core;
using Rogium.Editors.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rogium.UserInterface.AssetSelection
{
    /// <summary>
    /// Is responsible for controlling the Asset Selection Menu, and switching out content as needed in it.
    /// </summary>
    public class AssetSelectionOverseer
    {
        public event Action<AssetHolderBase> OnSpawnCard;
        public event Action OnFinishedFilling;
        
        private readonly IList<AssetHolderBase> assets;
        
        private ObjectSwitcherMono layoutSwitcher;
        private AssetType lastTypeOpen;
        
        #region Singleton Pattern
        private static AssetSelectionOverseer instance;
        private static readonly object padlock = new object();

        public static AssetSelectionOverseer Instance
        {
            get
            {
                lock (padlock)
                {
                    if (instance == null)
                        instance = new AssetSelectionOverseer();
                    return instance;
                }
            }
        }

        #endregion

        private AssetSelectionOverseer()
        {
            assets = new List<AssetHolderBase>();
            lastTypeOpen = AssetType.None;
        }

        /// <summary>
        /// Sets up the selection menu.
        /// </summary>
        public void Setup(AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<AssetBase> assetList, ObjectSwitcherMono layoutSwitcher)
        {
            this.layoutSwitcher = layoutSwitcher;
            this.layoutSwitcher.Switch(layout.Menu.gameObject);

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
            OnFinishedFilling?.Invoke();
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
            layout.Content.gameObject.KillChildren();
            
            //Spawn ADD Button if not null.
            if (asset.addButton != null)
                Object.Instantiate(asset.addButton, layout.Content);

            //Cancel further process if list is empty.
            if (CancelProcess(assetList, layout.NotFoundText)) return;
            
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
            AssetHolderBase holder = Object.Instantiate(asset.assetObject, layout.Content);
            if (layout.IconPositionType == IconPositionType.Global)
                holder.Construct(type, id, assetList[id], layout.IconPosition);
            else
                holder.Construct(type, id, assetList[id]);
            OnSpawnCard?.Invoke(holder);
            assets.Add(holder);
        }

        /// <summary>
        /// Determines if to continue with the process.
        /// </summary>
        /// <param name="assets">The list of assets.</param>
        /// <param name="textObject">The text object to activate/deactivate.</param>
        /// <returns></returns>
        private bool CancelProcess(IList<AssetBase> assets, GameObject textObject)
        {
            if (assets.Count <= 0)
            {
                if (textObject != null) textObject.gameObject.SetActive(true);
                return true;
            }
            else
            {
                if (textObject != null) textObject.gameObject.SetActive(false);
                return false;
            }
        }
        
        /// <summary>
        /// Get amount of assets showcased by the menu.
        /// </summary>
        public int AssetCount => assets.Count;
        
    }
}