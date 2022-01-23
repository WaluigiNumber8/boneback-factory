using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.UserInterface.AssetSelection;
using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Rogium.Systems.ItemPalette
{
    /// <summary>
    /// The Item Palette Variant for Assets.
    /// </summary>
    public class ItemPaletteAsset : MonoBehaviour
    {
        public event Action<AssetSlot> OnSelect;
        
        [SerializeField] private AssetSlot assetHolderPrefab;
        [SerializeField] private Transform paletteParent;
        [SerializeField] private TextMeshProUGUI emptyText;
        
        private readonly IList<AssetSlot> holders = new List<AssetSlot>();

        private void OnEnable()
        {
            AssetSlot.OnSelectedAny += Select;
        }

        private void OnDisable()
        {
            AssetSlot.OnSelectedAny -= Select;
        }

        
        /// <summary>
        /// Call the <see cref="OnSelect"/> event for a specific item.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        public void Select(int index)
        {
            SafetyNet.EnsureIntIsInRange(index, 0, holders.Count, "Item Index");
            if (holders?.Count <= 0) return;
            
            OnSelect?.Invoke(holders[index]);
        }

        /// <summary>
        /// Spawns assets holders and fills them with information.
        /// </summary>
        /// <param name="assets">The list of assets the base the filling on.</param>
        /// <param name="type">The type the assets are.</param>
        public void Fill<T>(IList<T> assets, AssetType type) where T : AssetBase
        {
            SafetyNet.EnsureIsNotNull(assets, "List of assets");
            
            //Hint Text
            emptyText.gameObject.SetActive((assets.Count <= 0));
            
            //Prebuild objects if none are created.
            Build(assets.Count);
            
            //Fill each item with data.
            for (int i = 0; i < holders.Count; i++)
            {
                holders[i].Construct(type, i, assets[i]);
            }
        }

        /// <summary>
        /// Fills the palette with objects.
        /// </summary>
        /// <param name="length">The size of the palette.</param>
        private void Build(int length)
        {
            if (length == holders.Count) return;
                
            //Length is bigger.
            if (length > holders.Count)
            {
                BuildHolders(holders.Count, length);
                return;
            }
            
            //Length is smaller.
            if (length < holders.Count)
            {
                DestroyHolders(holders.Count - (length - holders.Count), holders.Count);
                return;
            }
            
        }

        /// <summary>
        /// Fills the palette with objects.
        /// </summary>
        /// <param name="startIndex">The position to start on.</param>
        /// <param name="endIndex">The position to end with.</param>
        private void BuildHolders(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                AssetSlot holder = Instantiate(assetHolderPrefab, paletteParent).GetComponent<AssetSlot>();

                if (i < holders.Count)
                    holders[i] = holder;
                else holders.Add(holder);
            }
        }
        
        /// <summary>
        /// Destroys objects in the palette.
        /// </summary>
        /// <param name="startIndex">The position to start on.</param>
        /// <param name="endIndex">The position to end with.</param>
        private void DestroyHolders(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                Destroy(holders[i]);
                holders.RemoveAt(i);
            }
        }
    }
}