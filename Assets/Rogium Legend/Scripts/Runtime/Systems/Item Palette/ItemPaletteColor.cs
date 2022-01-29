using BoubakProductions.Safety;
using Rogium.Editors.Palettes;
using System;
using System.Collections.Generic;
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
        
        private IList<ColorSlot> slots = new List<ColorSlot>();
        
        private void OnEnable()
        {
            ColorSlot.OnSelectedAny += Select;
        }

        private void OnDisable()
        {
            ColorSlot.OnSelectedAny -= Select;
        }

        /// <summary>
        /// Call the <see cref="OnSelect"/> event for a specific item.
        /// </summary>
        /// <param name="index">The index of the item.</param>
        public void Select(int index)
        {
            SafetyNet.EnsureIntIsInRange(index, 0, slots.Count, "Item Index");
            if (slots?.Count <= 0) return;

            slots[index].Toggle.isOn = true;
            OnSelect?.Invoke(slots[index]);
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
            Build(colors.Length);
            
            //Fill each item with data.
            for (int i = 0; i < slots.Count; i++)
            {
                slots[i].Construct(colors[i], i);
            }
        }

        /// <summary>
        /// Fills the palette with objects.
        /// </summary>
        /// <param name="length">The size of the palette.</param>
        private void Build(int length)
        {
            if (length == slots.Count) return;
                
            //Length is bigger.
            if (length > slots.Count)
            {
                BuildSlots(slots.Count, length);
                return;
            }
            
            //Length is smaller.
            if (length < slots.Count)
            {
                DestroySlots(slots.Count - (length - slots.Count), slots.Count);
                return;
            }
            
        }

        /// <summary>
        /// Fills the palette with objects.
        /// </summary>
        /// <param name="startIndex">The position to start on.</param>
        /// <param name="endIndex">The position to end with.</param>
        private void BuildSlots(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                ColorSlot slot = Instantiate(colorSlotPrefab, paletteParent).GetComponent<ColorSlot>();

                if (i < slots.Count)
                    slots[i] = slot;
                else slots.Add(slot);
            }
        }
        
        /// <summary>
        /// Destroys objects in the palette.
        /// </summary>
        /// <param name="startIndex">The position to start on.</param>
        /// <param name="endIndex">The position to end with.</param>
        private void DestroySlots(int startIndex, int endIndex)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                Destroy(slots[i]);
                slots.RemoveAt(i);
            }
        }
    }
}