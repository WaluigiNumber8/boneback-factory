using RedRats.Safety;
using Rogium.Editors.Palettes;
using System;
using System.Collections.Generic;
using RedRats.UI.MenuFilling;
using UnityEngine;
using TMPro;

namespace Rogium.Systems.ItemPalette
{
    /// <summary>
    /// The Item Palette Variant for Colors.
    /// </summary>
    public class ItemPaletteColor : MonoBehaviour
    {
        public event Action<ColorSlot> OnSelect;
        
        [SerializeField] private ColorSlot colorSlotPrefab;
        [SerializeField] private Transform paletteParent;
        [SerializeField] private TextMeshProUGUI emptyText;

        private MenuFiller<ColorSlot> menuFiller;

        private IList<ColorSlot> slots;

        private void Awake()
        {
            slots = new List<ColorSlot>();
            menuFiller = new MenuFiller<ColorSlot>();
        }

        private void OnEnable() => ColorSlot.OnSelectedAny += WhenSelected;
        private void OnDisable() => ColorSlot.OnSelectedAny -= WhenSelected;

        /// <summary>
        /// Call the <see cref="OnSelect"/> event for a specific item.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        public void Select(int index)
        {
            if (slots == null || slots.Count <= 0) return;
            SafetyNet.EnsureIndexWithingCollectionRange(index, slots, "List of Slots");
            slots[index].SetToggle(true);
        }
        
        /// <summary>
        /// Spawns slots and fills them with information.
        /// </summary>
        /// <param name="colors"></param>
        public void Fill(Color[] colors)
        {
            SafetyNet.EnsureIsNotNull(colors, "List of colors");
            
            //Hint Text
            if (emptyText != null) emptyText.gameObject.SetActive((colors.Length <= 0));
            
            //Prebuild objects if none are created.
            menuFiller.Update(slots, colors.Length, colorSlotPrefab, paletteParent);
            
            //Fill each item with data.
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].Construct(colors[i], i);
            }
        }
        
        /// <summary>
        /// Fires an event when selected.
        /// </summary>
        /// <param name="index">The index of the slot to select.</param>
        private void WhenSelected(int index)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(index, slots, "List of Slots");
            OnSelect?.Invoke(slots[index]);
        }
    }
}