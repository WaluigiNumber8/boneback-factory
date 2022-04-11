using BoubakProductions.Safety;
using Rogium.Core;
using Rogium.Editors.Core;
using Rogium.UserInterface.Editors.AssetSelection;
using System;
using System.Collections.Generic;
using BoubakProductions.UI.MenuFilling;
using UnityEngine;
using TMPro;

namespace Rogium.Systems.ItemPalette
{
    /// <summary>
    /// The Item Palette Variant for Assets.
    /// </summary>
    public class ItemPaletteAsset : MonoBehaviour
    {
        public event Action<IAsset> OnSelect;
        
        [SerializeField] private AssetSlot assetHolderPrefab;
        [SerializeField] private Transform paletteParent;
        [SerializeField] private TextMeshProUGUI emptyText;

        private MenuFiller<AssetSlot> menuFiller;

        private readonly IList<AssetSlot> holders = new List<AssetSlot>();
        private IList<string> ids;

        private int lastSelected = -1;
        
        private void Awake() => menuFiller = new MenuFiller<AssetSlot>();
        private void OnEnable() => AssetSlot.OnSelectedAny += WhenSelected;
        private void OnDisable() => AssetSlot.OnSelectedAny -= WhenSelected;

        /// <summary>
        /// Call the <see cref="OnSelect"/> event for a specific item.
        /// </summary>
        /// <param name="id">The id of the item.</param>
        public void Select(string id)
        {
            if (ids?.Count != holders.Count)
                ids = holders.ConvertToIDs();
            
            Select(ids.IndexOf(id));
        }
        /// <summary>
        /// Call the <see cref="OnSelect"/> event for a specific item.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        public void Select(int index)
        {
            if (holders?.Count <= 0) return;
            SafetyNet.EnsureIndexWithingCollectionRange(index, holders, "Item Index");
            holders[index].SetToggle(true);
            OnSelect?.Invoke(holders[index].Asset);
        }

        /// <summary>
        /// Select the last selected asset on this palette.
        /// </summary>
        public void SelectLast()
        {
            if (lastSelected < 0 || lastSelected > holders.Count) return;
            Select(lastSelected);
        }

        /// <summary>
        /// Spawns assets holders and fills them with information.
        /// </summary>
        /// <param name="assets">The list of assets the base the filling on.</param>
        /// <param name="type">The type the assets are.</param>
        public void Fill<T>(IList<T> assets, AssetType type) where T : IAsset
        {
            SafetyNet.EnsureIsNotNull(assets, "List of assets");
            
            //Hint Text
            emptyText.gameObject.SetActive((assets.Count <= 0));
            
            menuFiller.Update(holders, assets.Count, assetHolderPrefab, paletteParent);
            
            //Fill each item with data.
            for (int i = 0; i < holders.Count; i++)
            {
                holders[i].Construct(type, i, assets[i]);
            }
        }
        
        /// <summary>
        /// Fires an event when selected.
        /// </summary>
        /// <param name="index">The index of the slot to select.</param>
        private void WhenSelected(int index)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(index, holders, "List of holders");
            lastSelected = index;
            OnSelect?.Invoke(holders[index].Asset);
        }
    }
}