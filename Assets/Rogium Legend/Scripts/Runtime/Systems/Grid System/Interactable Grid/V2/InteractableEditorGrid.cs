using System;
using System.Collections.Generic;
using RedRats.Core;
using RedRats.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// A type of grid the player can interact with via a pointer.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class InteractableEditorGrid : InteractableEditorGridBase, IPointerClickHandler, IPointerMoveHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<Vector2Int> OnClick;
        public event Action<Vector2Int> OnClickAlternative;
        public event Action OnPointerLeave;
        public event Action OnPointerComeIn;
        public event Action OnPointerClicked;
        
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private LayerInfo[] layers;
        
        private RectTransform ttransform;
        private SpriteDrawer drawer;
        
        private Image activeLayer;
        
        private Vector2 cellSize;
        private Vector2Int selectedPos;
        
        private void Awake()
        {
            SafetyNet.EnsureIntIsBiggerOrEqualTo(gridSize.x, 1, "Grid Size X");
            SafetyNet.EnsureIntIsBiggerOrEqualTo(gridSize.y, 1, "Grid Size Y");
            SafetyNet.EnsureIntIsBiggerThan(layers.Length, 0, "Layers amount");
            
            ttransform = GetComponent<RectTransform>();
            drawer = new SpriteDrawer(gridSize, new Vector2Int(EditorConstants.SpriteSize, EditorConstants.SpriteSize), EditorConstants.SpriteSize);
            
            cellSize = new Vector2(ttransform.rect.width / gridSize.x, ttransform.rect.height / gridSize.y);
            
            PrepareLayers();
            SwitchActiveLayer(0);
        }

        public void OnPointerEnter(PointerEventData eventData) => OnPointerComeIn?.Invoke();
        public void OnPointerExit(PointerEventData eventData) => OnPointerLeave?.Invoke();
        
        public void OnPointerMove(PointerEventData eventData)
        {
            RecalculateSelectedPosition(eventData.position);
            if (InputSystem.Instance.UI.Click.IsHeld)
            {
                OnClick?.Invoke(selectedPos);
                return;
            }
            
            if (InputSystem.Instance.UI.ClickAlternative.IsHeld)
            {
                OnClickAlternative?.Invoke(selectedPos);
                return;
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            RecalculateSelectedPosition(eventData.position);
            OnPointerClicked?.Invoke();
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    OnClick?.Invoke(selectedPos);
                    return;
                case PointerEventData.InputButton.Right:
                    OnClickAlternative?.Invoke(selectedPos);
                    return;
            }
        }

        /// <summary>
        /// Loads sprites into the editor grid.
        /// </summary>
        /// <param name="assetList">From which list of assets to load from.</param>
        /// <param name="IDGrid">The grid of IDs to read.</param>
        /// <param name="layerIndex">The index of the layer to load on.</param>
        /// <typeparam name="T">Is a type of <see cref="IAsset"/>.</typeparam>
        /// <typeparam name="TS">Any type of <see cref="IComparable"/>.</typeparam>
        public void LoadWithSprites<T, TS>(ObjectGrid<TS> IDGrid, IList<T> assetList, int layerIndex) where T : IAsset where TS : IComparable
        {
            SafetyNet.EnsureIntIsEqual(IDGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(IDGrid.Height, gridSize.y, "Grid Height");
            layers[layerIndex].layer.sprite = drawer.Build(IDGrid, assetList);
        }
        
        public override void LoadWithSprites<T>(ObjectGrid<string> IDGrid, IList<T> assetList)
        {
            SafetyNet.EnsureIntIsEqual(IDGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(IDGrid.Height, gridSize.y, "Grid Height");
            activeLayer.sprite = drawer.Build(IDGrid, assetList);
        }
        
        public override void LoadWithColors(ObjectGrid<int> indexGrid, Color[] colorArray)
        {
            SafetyNet.EnsureIntIsEqual(indexGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(indexGrid.Height, gridSize.y, "Grid Height");
            activeLayer.sprite = drawer.Build(indexGrid, colorArray);
        }
        
        public override void UpdateCell(Vector2Int position, Color value)
        {
            drawer.UpdateValue(activeLayer.sprite, position, value);
        }
        
        public override void UpdateCell(Vector2Int position, Sprite value)
        {
            drawer.UpdateValue(activeLayer.sprite, position, value);
        }

        public override void Apply() => drawer.Apply(activeLayer.sprite);

        public override void ClearAllCells()
        {
            drawer.ClearAllCells(activeLayer.sprite);
            Apply();
        }

        /// <summary>
        /// Switches the currently active layer.
        /// </summary>
        /// <param name="layerIndex">The index of the active layer.</param>
        public void SwitchActiveLayer(int layerIndex)
        {
            SafetyNet.EnsureIndexWithingCollectionRange(layerIndex, layers, nameof(layers));
            
            activeLayer = layers[layerIndex].layer;
            RefreshLayerColors(layerIndex);
        }
        
        /// <summary>
        /// Prepares the grid layers.
        /// </summary>
        private void PrepareLayers()
        {
            foreach (LayerInfo info in layers)
            {
                info.layer.color = Color.white;
                info.layer.sprite = RedRatBuilder.GenerateSprite(EditorConstants.NoColor,
                                                                 EditorConstants.SpriteSize * gridSize.x,
                                                                 EditorConstants.SpriteSize * gridSize.y,
                                                                 EditorConstants.SpriteSize);
            }
        }
        
        /// <summary>
        /// Updates the currently selected grid position based on the pointer.
        /// </summary>
        private void RecalculateSelectedPosition(Vector2 pointerPos)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(ttransform, pointerPos, null, out Vector2 pos)) return;
            
            int x = (int)Mathf.Floor(pos.x / cellSize.x);
            int y = (int)Mathf.Floor(pos.y / cellSize.y);
            x = Mathf.Clamp(x, 0, gridSize.x - 1);
            y = Mathf.Clamp(y, 0, gridSize.y - 1);
            
            selectedPos = new Vector2Int(x, y);
        }

        /// <summary>
        /// Refreshes the color of all layers.
        /// </summary>
        /// <param name="layerIndex">The active layer index.</param>
        private void RefreshLayerColors(int layerIndex)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                layers[i].layer.color = (i == layerIndex) ? Color.white : layers[i].outOfFocusColor;
            }
        }
        
        public Vector2Int Size { get => gridSize; }
        public Vector2 CellSize { get => cellSize; }
        public Vector2Int SelectedPosition { get => selectedPos; }
    }
}