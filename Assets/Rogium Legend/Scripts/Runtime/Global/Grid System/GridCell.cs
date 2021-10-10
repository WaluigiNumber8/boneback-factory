using Rogium.Global.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.Global.GridSystem
{
    /// <summary>
    /// Holds Grid Cell's GUI information.
    /// </summary>
    [RequireComponent(typeof(Image))]
    public class GridCell : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler
    {
        private EditorGridOverseer overseer;
        private UIInput input;
        
        [SerializeField] private Image image;
        private int x;
        private int y;

        /// <summary>
        /// Spawns a Grid Cell with correct information
        /// </summary>
        /// <param name="overseer">The Grid Loader, that spawned this cell.</param>
        /// <param name="x">Cell's X position on the grid.</param>
        /// <param name="y">Cell's Y position on the gird.</param>
        /// <param name="sprite">The sprite of cell, that will show in the UI.</param>
        public void Spawn(EditorGridOverseer overseer, UIInput input, int x, int y, Sprite sprite)
        {
            this.overseer = overseer;
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
            this.image.sprite = sprite;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            overseer.OnCellClick(x, y);
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (input.ClickHold || input.ClickPressed) overseer.OnCellClick(x, y);
        }

    }
}