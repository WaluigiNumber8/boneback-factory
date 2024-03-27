using RedRats.Core;
using RedRats.Safety;
using RedRats.Systems.ObjectSwitching;
using Rogium.Core;
using Rogium.Editors.Core;
using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Rogium.UserInterface.Editors.AssetSelection
{
    /// <summary>
    /// Is responsible for controlling the Asset Selection Menu, and switching out content as needed in it.
    /// </summary>
    public sealed class AssetSelectionOverseer : Singleton<AssetSelectionOverseer>
    {
        public event Action<AssetHolderBase> OnSpawnCard;
        public event Action OnFinishedFilling;
        
        private readonly IList<AssetHolderBase> assets;
        private SelectionMenuLayout currentLayout;
        private AssetType lastTypeOpen;

        private AssetSelectionOverseer()
        {
            assets = new List<AssetHolderBase>();
            lastTypeOpen = AssetType.None;
        }

        
        /// <summary>
        /// Sets up the selection menu.
        /// </summary>
        public void Setup(AssetType type, SelectionMenuLayout layout, SelectionMenuAsset asset, IList<IAsset> assetList)
        {
            currentLayout = layout;

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
                            SpawnCard(assetList.Count + (i-1), type, asset, assetList);
                        }
                        return;
                    }
                }
            }

            RefillMenu(type, asset, assetList);
            lastTypeOpen = type;
            OnFinishedFilling?.Invoke();
        }
        
        /// <summary>
        /// Fills the selection menu canvas with asset holder objects.
        /// <param name="type">The type of asset this card will be.</param>
        /// <param name="asset">What kind of data will be used.</param>
        /// <param name="assetList">The list to take the data from.</param>
        /// </summary>
        private void RefillMenu(AssetType type, SelectionMenuAsset asset, IList<IAsset> assetList)
        {
            SafetyNet.EnsureIsNotNull(asset.assetObject.GetComponent<IAssetHolder>(), "IAssetHolder in RefillMenu");
            
            //Clear assets from content, respawn add button
            assets.Clear();
            currentLayout.Content.gameObject.KillChildren();
            
            //Spawn ADD Button if not null.
            if (asset.addButton != null) Object.Instantiate(asset.addButton, currentLayout.Content);

            //Spawn select empty button.
            
            
            //Cancel further process if list is empty.
            if (CancelIfEmpty(assetList, currentLayout.NotFoundText)) return;
            
            //Spawn new assets
            for (int i = 0; i < assetList.Count; i++)
            {
                SpawnCard(i, type, asset, assetList);
            }
        }

        /// <summary>
        /// Spawns the clickable UI Card itself.
        /// </summary>
        /// <param name="index">Position on the list it spawns the card on.</param>
        /// <param name="type">The type of asset this card will be.</param>
        /// <param name="asset">What kind of data will be used.</param>
        /// <param name="assetList">The list to take the data from.</param>
        private void SpawnCard(int index, AssetType type, SelectionMenuAsset asset, IList<IAsset> assetList)
        {
            AssetHolderBase holder = Object.Instantiate(asset.assetObject, currentLayout.Content);
            if (currentLayout.IconPositionType == IconPositionType.Global)
                holder.Construct(type, index, assetList[index], currentLayout.IconPosition);
            else
                holder.Construct(type, index, assetList[index]);
            OnSpawnCard?.Invoke(holder);
            assets.Add(holder);
        }

        /// <summary>
        /// Determines if to continue with the process.
        /// </summary>
        /// <param name="assetList">The list of assets.</param>
        /// <param name="textObject">The text object to activate/deactivate.</param>
        /// <returns></returns>
        private bool CancelIfEmpty(IList<IAsset> assetList, GameObject textObject)
        {
            bool shouldCancel = assetList.Count <= 0;
            if (textObject != null) textObject.gameObject.SetActive(shouldCancel);
            return shouldCancel;
        }
        
        /// <summary>
        /// Get amount of assets showcased by the menu.
        /// </summary>
        public int AssetCount => assets.Count;
        
    }
}