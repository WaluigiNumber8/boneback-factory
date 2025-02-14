using RedRats.Safety;
using Rogium.Editors.Palettes;
using System;
using System.Collections.Generic;
using RedRats.UI.MenuFilling;
using Rogium.Editors.Core.Defaults;
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
        public event Action<ColorSlot> OnChangeColorOnSelectedSlot;
        
        [SerializeField] private ColorSlot colorSlotPrefab;
        [SerializeField] private Transform paletteParent;
        [SerializeField] private TextMeshProUGUI emptyText;

        private MenuFiller<ColorSlot> menuFiller;
        private IList<ColorSlot> slots;
        private int currentSelectedIndex;

        private void Awake()
        {
            slots = new List<ColorSlot>();
            menuFiller = new MenuFiller<ColorSlot>();
        }

        private void OnEnable()
        {
            ColorSlot.OnSelectedAny += NotifyListeners;
            ColorSlot.OnChangeColor += TryNotifyColorChanged;
        }

        private void OnDisable()
        {
            ColorSlot.OnSelectedAny -= NotifyListeners;
            ColorSlot.OnChangeColor -= TryNotifyColorChanged;
        }

        /// <summary>
        /// Spawns slots and fills them with information.
        /// </summary>
        /// <param name="colors"></param>
        public void Fill(Color[] colors)
        {
            Preconditions.IsNotNull(colors, "List of colors");
            
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
        /// Call the <see cref="OnSelect"/> event for a specific item.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        public void Select(int index)
        {
            if (index == EditorDefaults.EmptyColorID) return;
            if (slots == null || slots.Count <= 0) return;
            Preconditions.IsIndexWithingCollectionRange(slots, index, "List of Slots");
            if (slots[index].IsOn) slots[index].SetToggle(false);
            slots[index].SetToggle(true);
        }
        
        public ColorSlot GetSlot(int index)
        {
            Preconditions.IsIndexWithingCollectionRange(slots, index, "List of Slots");
            Preconditions.IsNotNull(slots[index], "Slot");
            return slots[index];
        }
        
        /// <summary>
        /// Fires an event when selected.
        /// </summary>
        /// <param name="index">The index of the slot to select.</param>
        private void NotifyListeners(int index)
        {
            Preconditions.IsIndexWithingCollectionRange(slots, index, "List of Slots");
            currentSelectedIndex = index;
            OnSelect?.Invoke(slots[index]);
        }
        
        private void TryNotifyColorChanged(int index)
        {
            if (index != currentSelectedIndex) return;
            OnChangeColorOnSelectedSlot?.Invoke(slots[currentSelectedIndex]);
        }
    }
}