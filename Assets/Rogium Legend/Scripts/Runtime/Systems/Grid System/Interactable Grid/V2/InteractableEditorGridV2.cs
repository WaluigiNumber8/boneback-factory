using System;
using BoubakProductions.Core;
using BoubakProductions.Safety;
using Rogium.Editors.Core;
using Rogium.Editors.Core.Defaults;
using Rogium.Systems.Input;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Rogium.Systems.GridSystem
{
    /// <summary>
    /// An upgraded version of <see cref="InteractableEditorGrid"/>.
    /// </summary>
    [RequireComponent(typeof(RectTransform))]
    public class InteractableEditorGridV2 : InteractableEditorGridBase, IPointerClickHandler, IPointerMoveHandler
    {
        public override event Action<Vector2Int> OnClick;
        public event Action<Vector2Int> OnClickAlternative;
        
        [SerializeField] private Vector2Int gridSize;
        [SerializeField] private LayerInfo[] layers;
        
        private RectTransform ttransform;
        private SpriteDrawer drawer;
        
        private Image activeLayer;
        
        private Vector2Int cellSize;
        private Vector2Int selectedPos;
        
        private void Awake()
        {
            SafetyNet.EnsureIntIsBiggerThan(layers.Length, 0, "Layers amount");
            
            ttransform = GetComponent<RectTransform>();
            cellSize = new Vector2Int((int)ttransform.rect.width / gridSize.x, (int)ttransform.rect.height / gridSize.y);
            
            drawer = new SpriteDrawer(gridSize, new Vector2Int(EditorDefaults.SpriteSize, EditorDefaults.SpriteSize), EditorDefaults.SpriteSize);
            PrepareLayers();
            SwitchActiveLayer(0);
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            switch (eventData.button)
            {
                case PointerEventData.InputButton.Left:
                    RecalculateSelectedPosition(eventData.position);
                    OnClick?.Invoke(selectedPos);
                    return;
                case PointerEventData.InputButton.Right:
                    RecalculateSelectedPosition(eventData.position);
                    OnClickAlternative?.Invoke(selectedPos);
                    return;
            }
        }

        public void OnPointerMove(PointerEventData eventData)
        {
            if (InputSystem.Instance.UI.Click.IsHeld)
            {
                RecalculateSelectedPosition(eventData.position);
                OnClick?.Invoke(selectedPos);
                return;
            }
            
            if (InputSystem.Instance.UI.ClickAlternative.IsHeld)
            {
                RecalculateSelectedPosition(eventData.position);
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
        /// <typeparam name="T">Is a type of Asset.</typeparam>
        public void LoadWithSprites<T>(AssetList<T> assetList, ObjectGrid<string> IDGrid, int layerIndex) where T : AssetBase
        {
            SafetyNet.EnsureIntIsEqual(IDGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(IDGrid.Height, gridSize.y, "Grid Height");
            layers[layerIndex].layer.sprite = drawer.Build(IDGrid, assetList);
        }
        
        public override void LoadWithSprites<T>(AssetList<T> assetList, ObjectGrid<string> IDGrid)
        {
            SafetyNet.EnsureIntIsEqual(IDGrid.Width, gridSize.x, "Grid Width");
            SafetyNet.EnsureIntIsEqual(IDGrid.Height, gridSize.y, "Grid Height");
            activeLayer.sprite = drawer.Build(IDGrid, assetList);
        }
        
        public override void LoadWithColors(Color[] colorArray, ObjectGrid<int> indexGrid)
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
        /// Updates the currently selected grid position based on the pointer.
        /// </summary>
        private void RecalculateSelectedPosition(Vector2 pointerPos)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(ttransform, pointerPos, null, out Vector2 pos);
            
            int x = (int)Mathf.Abs(Mathf.Floor(pos.x / cellSize.x));
            int y = (int)Mathf.Abs(Mathf.Floor(pos.y / cellSize.y));

            x = Mathf.Max(0, Mathf.Min(x, gridSize.x-1));
            y = Mathf.Max(0, Mathf.Min(y, gridSize.y-1));
            
            selectedPos = new Vector2Int(x, y);
        }

        /// <summary>
        /// Prepares the grid layers.
        /// </summary>
        private void PrepareLayers()
        {
            foreach (LayerInfo info in layers)
            {
                info.layer.color = Color.white;
                info.layer.sprite = BoubakBuilder.GenerateSprite(EditorDefaults.NoColor,
                                                                 EditorDefaults.SpriteSize * gridSize.x,
                                                                 EditorDefaults.SpriteSize * gridSize.y,
                                                                 EditorDefaults.SpriteSize);
            }
        }

        /// <summary>
        /// Refreshes the color of all layers.
        /// </summary>
        /// <param name="layerIndex">The active layer index.</param>
        private void RefreshLayerColors(int layerIndex)
        {
            for (int i = 0; i < layers.Length; i++)
            {
                if (i == layerIndex)
                {
                    layers[i].layer.color = Color.white;
                    continue;
                }

                layers[i].layer.color = layers[i].outOfFocusColor;
            }
        }
    }
}