using System;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// Holds Grid Cell's GUI information.
    /// </summary>
    public class GridCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
    {
        public static event Action<int, int> OnCellClick;
        
        [SerializeField] private Image image;
        
        private UIInput input;
        
        private int x, y;

        /// <summary>
        /// Spawns a Grid Cell with correct information.
        /// </summary>
        /// <param name="x">Cell's X position on the grid.</param>
        /// <param name="y">Cell's Y position on the gird.</param>
        /// <param name="color">The color of cell, that will show in the UI.</param>
        public void Construct(UIInput input, int x, int y, Color color)
        {
            Construct(input, x, y, null);
            UpdateColor(color);
        }
        /// <summary>
        /// Spawns a Grid Cell with correct information.
        /// </summary>
        /// <param name="x">Cell's X position on the grid.</param>
        /// <param name="y">Cell's Y position on the gird.</param>
        /// <param name="sprite">The sprite of cell, that will show in the UI.</param>
        public void Construct(UIInput input, int x, int y, Sprite sprite)
        {
            this.input = input;
            this.x = x;
            this.y = y;
            UpdateSprite(sprite);
        }

        /// <summary>
        /// Updates the sprite of the cell.
        /// </summary>
        /// <param name="sprite"></param>
        public void UpdateSprite(Sprite sprite)
        {
            this.image.color = Color.white;
            this.image.sprite = sprite;
        }

        /// <summary>
        /// Updates the color of the cell.
        /// </summary>
        /// <param name="color">The new color the cell will use.</param>
        public void UpdateColor(Color color)
        {
            this.image.sprite = null;
            this.image.color = color;
        }

        /// <summary>
        /// Sets the cell to an empty state.
        /// </summary>
        public void Clear()
        {
            UpdateSprite(null);
            UpdateColor(Color.black);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            OnCellClick?.Invoke(x, y);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (input.ClickHold || input.ClickPressed) OnCellClick?.Invoke(x, y);
        }

    }
}